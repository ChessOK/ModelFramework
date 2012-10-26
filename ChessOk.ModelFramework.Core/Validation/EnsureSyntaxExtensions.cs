using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public static class EnsureSyntaxExtensions
    {
        public static IEnsureSyntax<T> IsValid<T>(this IEnsureSyntax<T> syntax, bool doNotModifyKeys = false)
        {
            var validator = syntax.ValidationContext.Get<ObjectValidator>();
            validator.AttributesValidator.DoNotModifyErrorKeys = doNotModifyKeys;
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<string> HasMaxLength(this IEnsureSyntax<string> syntax, int maximumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Get<MaxLengthValidator>();
            validator.Length = maximumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T[]> HasMaxLength<T>(this IEnsureSyntax<T[]> syntax, int maximumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Get<MaxLengthValidator>();
            validator.Length = maximumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<string> HasMinLength(this IEnsureSyntax<string> syntax, int minimumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Get<MinLengthValidator>();
            validator.Length = minimumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T[]> HasMinLength<T>(this IEnsureSyntax<T[]> syntax, int minimumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Get<MinLengthValidator>();
            validator.Length = minimumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<DateTime> IsSqlDateTime(this IEnsureSyntax<DateTime> syntax, string message = null)
        {
            var validator = syntax.ValidationContext.Get<SqlDateTimeValidator>();
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsPresent<T>(this IEnsureSyntax<T> syntax, string message = null, bool allowEmptyStrings = false)
            where T : class
        {
            var validator = syntax.ValidationContext.Get<RequiredValidator>();
            validator.AllowEmptyStrings = allowEmptyStrings;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T?> IsPresent<T>(this IEnsureSyntax<T?> syntax)
            where T : struct
        {
            var validator = syntax.ValidationContext.Get<RequiredValidator>();

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsNull<T>(this IEnsureSyntax<T> syntax)
            where T : class
        {
            var validator = syntax.ValidationContext.Get<NullValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsTrue<T>(this IEnsureSyntax<T> syntax, Func<T, bool> @delegate,  string message)
        {
            var validator = syntax.ValidationContext.Get<DelegateValidator>();
            validator.Delegate = obj => @delegate((T)obj);
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<string> Matches(this IEnsureSyntax<string> syntax, string pattern, string message = null)
        {
            var validator = syntax.ValidationContext.Get<RegularExpressionValidator>();
            validator.Pattern = pattern;
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