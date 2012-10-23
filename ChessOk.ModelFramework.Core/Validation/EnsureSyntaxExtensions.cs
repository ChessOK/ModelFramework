using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public static class EnsureSyntaxExtensions
    {
        public static IEnsureSyntax<T> IsValid<T>(this IEnsureSyntax<T> syntax)
        {
            return syntax.IsValid(syntax.ValidationContext.Get<ObjectValidator>());
        }

        public static IEnsureSyntax<string> HasMaximumLength(this IEnsureSyntax<string> syntax, int maximumLength)
        {
            var validator = syntax.ValidationContext.Get<MaximumLengthValidator>();
            validator.MaximumLength = maximumLength;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<DateTime> IsSqlDateTime(this IEnsureSyntax<DateTime> syntax)
        {
            var validator = syntax.ValidationContext.Get<SqlDateTimeValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsPresent<T>(this IEnsureSyntax<T> syntax, bool allowEmptyStrings = false)
            where T : class
        {
            var validator = syntax.ValidationContext.Get<RequiredValidator>();
            validator.AllowEmptyStrings = allowEmptyStrings;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsNull<T>(this IEnsureSyntax<T> syntax)
            where T : class
        {
            var validator = syntax.ValidationContext.Get<NullValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsNotNull<T>(this IEnsureSyntax<T> syntax)
        {
            var validator = syntax.ValidationContext.Get<NotNullValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsTrue<T>(this IEnsureSyntax<T> syntax, Func<object, bool> @delegate,  string message)
        {
            var validator = syntax.ValidationContext.Get<DelegateValidator>();
            validator.Delegate = @delegate;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        #region CollectionValidator

        public static IEnsureSyntax<IEnumerable<T>> HasValidItems<T>(this IEnsureSyntax<IEnumerable<T>> syntax)
        {
            var validator = syntax.ValidationContext.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T[]> HasValidItems<T>(this IEnsureSyntax<T[]> syntax)
        {
            var validator = syntax.ValidationContext.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<List<T>> HasValidItems<T>(this IEnsureSyntax<List<T>> syntax)
        {
            var validator = syntax.ValidationContext.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<IList<T>> HasValidItems<T>(this IEnsureSyntax<IList<T>> syntax)
        {
            var validator = syntax.ValidationContext.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<Collection<T>> HasValidItems<T>(this IEnsureSyntax<Collection<T>> syntax)
        {
            var validator = syntax.ValidationContext.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<ICollection<T>> HasValidItems<T>(this IEnsureSyntax<ICollection<T>> syntax)
        {
            var validator = syntax.ValidationContext.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        #endregion
    }
}