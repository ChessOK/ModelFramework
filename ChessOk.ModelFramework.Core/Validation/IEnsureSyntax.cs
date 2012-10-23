using System;
using System.Linq.Expressions;

namespace ChessOk.ModelFramework.Validation
{
    public interface IEnsureSyntax<TObject>
    {
        IValidationContext ValidationContext { get; }

        IEnsureSyntax<TObject> IsValid(IValidator validator);

        IEnsureSyntax<TObject> Property<TProperty>(
            Expression<Func<TObject, TProperty>> propertyExpression,
            Action<IEnsureSyntax<TProperty>> validation);

        IEnsureSyntax<TObject> Property<TProperty>(
            string propertyName,
            Action<IEnsureSyntax<TProperty>> validation);
    }
}