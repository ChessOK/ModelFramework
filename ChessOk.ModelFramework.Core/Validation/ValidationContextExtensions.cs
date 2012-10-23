using System;

using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework.Validation
{
    public static class ValidationContextExtensions
    {
        public static IEnsureSyntax<T> Ensure<T>(this IValidationContext context, T obj)
        {
            return new EnsureEngine<T>(context, obj);
        }

        public static void AddError(this IValidationContext context, string message)
        {
            context.AddError(String.Empty, message);
        }

        public static IDisposable PrependKeysWithName(this IValidationContext context, string name)
        {
            var emptyReplace = context.ReplaceKeys(@"^$", name);
            var indexReplace = context.ReplaceKeys(@"^(\[[\d]+\])$", name + "$1");
            var nonEmptyReplace = context.ReplaceKeys(@"^(?:(?!\[([\d+])\]))(.+)$", string.Format("{0}.$2", name));
            return new DisposableAction(() =>
                {
                    emptyReplace.Dispose();
                    indexReplace.Dispose();
                    nonEmptyReplace.Dispose();
                });
        }
    }
}
