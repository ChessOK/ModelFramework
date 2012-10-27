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
            var validator = syntax.ValidationContext.Model.Get<ObjectValidator>();
            validator.AttributesValidator.DoNotModifyErrorKeys = doNotModifyKeys;
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<string> HasMaxLength(this IEnsureSyntax<string> syntax, int maximumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MaxLengthValidator>();
            validator.Length = maximumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T[]> HasMaxLength<T>(this IEnsureSyntax<T[]> syntax, int maximumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MaxLengthValidator>();
            validator.Length = maximumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<string> HasMinLength(this IEnsureSyntax<string> syntax, int minimumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MinLengthValidator>();
            validator.Length = minimumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T[]> HasMinLength<T>(this IEnsureSyntax<T[]> syntax, int minimumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MinLengthValidator>();
            validator.Length = minimumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<DateTime> IsSqlDateTime(this IEnsureSyntax<DateTime> syntax, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<SqlDateTimeValidator>();
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsPresent<T>(this IEnsureSyntax<T> syntax, string message = null, bool allowEmptyStrings = false)
            where T : class
        {
            var validator = syntax.ValidationContext.Model.Get<RequiredValidator>();
            validator.AllowEmptyStrings = allowEmptyStrings;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T?> IsPresent<T>(this IEnsureSyntax<T?> syntax, string message = null)
            where T : struct
        {
            var validator = syntax.ValidationContext.Model.Get<RequiredValidator>();
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsNull<T>(this IEnsureSyntax<T> syntax)
            where T : class
        {
            var validator = syntax.ValidationContext.Model.Get<NullValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T> IsTrue<T>(this IEnsureSyntax<T> syntax, Func<T, bool> @delegate,  string message)
        {
            var validator = syntax.ValidationContext.Model.Get<DelegateValidator>();
            validator.Delegate = obj => @delegate((T)obj);
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<string> Matches(this IEnsureSyntax<string> syntax, string pattern, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<RegularExpressionValidator>();
            validator.Pattern = pattern;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        #region CollectionValidator

        public static IEnsureSyntax<IEnumerable<T>> HasValidItems<T>(this IEnsureSyntax<IEnumerable<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<T[]> HasValidItems<T>(this IEnsureSyntax<T[]> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<List<T>> HasValidItems<T>(this IEnsureSyntax<List<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<IList<T>> HasValidItems<T>(this IEnsureSyntax<IList<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<Collection<T>> HasValidItems<T>(this IEnsureSyntax<Collection<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        public static IEnsureSyntax<ICollection<T>> HasValidItems<T>(this IEnsureSyntax<ICollection<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        #endregion

        public static IEnsureSyntax<T> IsNotValid<T>(this IEnsureSyntax<T> syntax, string message)
        {
            return syntax.IsTrue(x => false, message);
        }

        public static IEnsureSyntax<T> IsNotValidIf<T>(this IEnsureSyntax<T> syntax, bool condition, string message)
        {
            return syntax.IsTrue(x => condition == false, message);
        }

        public static IEnsureSyntax<T> IsNotValidIfNull<T>(this IEnsureSyntax<T> syntax, object obj, string message)
        {
            return syntax.IsNotValidIf(obj == null, message);
        }

        public static IEnsureSyntax<T> IsValidIf<T>(this IEnsureSyntax<T> syntax, bool condition, string message)
        {
            return syntax.IsTrue(x => condition, message);
        }

        public static IEnsureSyntax<T> IsGreaterThan<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) < 0, message);
        }

        public static IEnsureSyntax<T> IsGreaterThanOrEqual<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) <= 0, message);
        }

        public static IEnsureSyntax<T> IsLessThan<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) > 0, message);
        }

        public static IEnsureSyntax<T> IsLessThanOrEqual<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) >= 0, message);
        }
    }
}