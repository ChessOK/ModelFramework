﻿using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework
{
    public static class ValidationContextExtensions
    {
        public static IEnsureSyntax<T> Ensure<T>(this IValidationContext context, T obj)
        {
            return new EnsureEngine<T>(context, obj);
        }

        public static void Ensure<T, V>(this IValidationContext validationContext,
            T obj, Expression<Func<T, V>> propertyExpression, Action<IEnsureSyntax<V>> validation)
        {
            new EnsureEngine<T>(validationContext, obj).ItsProperty(propertyExpression, validation);
        }

        public static void AddError(this IValidationContext context, string message)
        {
            context.AddError(String.Empty, message);
        }

        public static IDisposable PrependKeysWithName(this IValidationContext context, string name)
        {
            var emptyReplace = context.ModifyKeys(@"^$", name);
            var indexReplace = context.ModifyKeys(@"^(\[[\d]+\])$", name + "$1");
            var nonEmptyReplace = context.ModifyKeys(@"^(?:(?!\[([\d+])\]))(.+)$", string.Format("{0}.$2", name));
            return new DisposableAction(() =>
                {
                    emptyReplace.Dispose();
                    indexReplace.Dispose();
                    nonEmptyReplace.Dispose();
                });
        }
    }
}
