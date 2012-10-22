using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public static class AssertionSyntaxExtensions
    {
        public static IAssertionSyntax<T> IsValid<T>(this IAssertionSyntax<T> syntax)
        {
            return syntax.IsValid(new ObjectValidator());
        }

        public static IAssertionSyntax<string> HasMaximumLength(this IAssertionSyntax<string> syntax, int maximumLength)
        {
            return syntax.IsValid(new MaximumLengthValidator(maximumLength));
        }

        public static IAssertionSyntax<DateTime> IsSqlDateTime(this IAssertionSyntax<DateTime> syntax)
        {
            return syntax.IsValid(new SqlDateTimeValidator());
        }

        public static IAssertionSyntax<T> IsPresent<T>(this IAssertionSyntax<T> syntax, bool allowEmptyStrings = false)
            where T : class
        {
            return syntax.IsValid(new PresenceValidator { AllowEmptyStrings = allowEmptyStrings });
        }

        public static IAssertionSyntax<T> IsNull<T>(this IAssertionSyntax<T> syntax)
            where T : class
        {
            return syntax.IsValid(new NullValidator());
        }

        public static IAssertionSyntax<T> IsNotNull<T>(this IAssertionSyntax<T> syntax)
        {
            return syntax.IsValid(new NotNullValidator());
        }

        public static IAssertionSyntax<T> IsTrue<T>(this IAssertionSyntax<T> syntax, Func<T, bool> @delegate,  string message)
        {
            return syntax.IsValid(new DelegateValidator<T>(@delegate, message));
        }

        #region CollectionValidator

        public static IAssertionSyntax<T[]> HasValidItems<T>(this IAssertionSyntax<T[]> syntax)
        {
            return syntax.IsValid(new CollectionValidator());
        }

        public static IAssertionSyntax<IEnumerable<T>> HasValidItems<T>(this IAssertionSyntax<IEnumerable<T>> syntax)
        {
            return syntax.IsValid(new CollectionValidator());
        }

        public static IAssertionSyntax<List<T>> HasValidItems<T>(this IAssertionSyntax<List<T>> syntax)
        {
            return syntax.IsValid(new CollectionValidator());
        }

        public static IAssertionSyntax<IList<T>> HasValidItems<T>(this IAssertionSyntax<IList<T>> syntax)
        {
            return syntax.IsValid(new CollectionValidator());
        }

        public static IAssertionSyntax<Collection<T>> HasValidItems<T>(this IAssertionSyntax<Collection<T>> syntax)
        {
            return syntax.IsValid(new CollectionValidator());
        }

        public static IAssertionSyntax<ICollection<T>> HasValidItems<T>(this IAssertionSyntax<ICollection<T>> syntax)
        {
            return syntax.IsValid(new CollectionValidator());
        }

        #endregion
    }
}