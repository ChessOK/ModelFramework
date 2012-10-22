using System;
using System.Linq.Expressions;

namespace ChessOk.ModelFramework.Validation
{
    public interface IAssertionSyntax<TObject>
    {
        IAssertionSyntax<TObject> IsValid(IValidator validator);

        IAssertionSyntax<TObject> AssertProperty<TProperty>(
            Expression<Func<TObject, TProperty>> propertyExpression,
            Action<IAssertionSyntax<TProperty>> validation);

        IAssertionSyntax<TObject> AssertProperty<TProperty>(
            string propertyName,
            Action<IAssertionSyntax<TProperty>> validation);
    }
}