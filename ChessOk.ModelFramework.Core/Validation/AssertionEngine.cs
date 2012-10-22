using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Expressions;

namespace ChessOk.ModelFramework.Validation.Internals
{
    public class AssertionEngine<TObject> : IAssertionSyntax<TObject>
    {
        private readonly IValidationContext _context;
        private readonly TObject _obj;

        public AssertionEngine(IValidationContext context, TObject obj)
        {
            _context = context;
            _obj = obj;
        }

        public IAssertionSyntax<TObject> IsValid(IValidator validator)
        {
            validator.ValidationContext = _context;
            validator.Validate(_obj);
            
            return this;
        }

        public IAssertionSyntax<TObject> AssertProperty<TProperty>(
            Expression<Func<TObject, TProperty>> propertyExpression, 
            Action<IAssertionSyntax<TProperty>> validation)
        {
            if (_obj != null)
            {
                var propertyName = ExpressionTextHelper.GetExpressionText(propertyExpression);
                var propertyValue = CachedExpressionCompiler.CompileGetter(propertyExpression)(_obj);

                AssertProperty(validation, propertyName, propertyValue);
            }

            return this;
        }

        public IAssertionSyntax<TObject> AssertProperty<TProperty>(string propertyName, 
                                                              Action<IAssertionSyntax<TProperty>> validation)
        {
            if (_obj != null)
            {
                var propertyInfo = _obj.GetType().GetProperty(propertyName);

                if (propertyInfo == null)
                {
                    throw new InvalidOperationException(
                        String.Format("Could not find property «{0}» in type «»", propertyName, _obj.GetType().Name));
                }

                var propertyValue = (TProperty)propertyInfo.GetValue(_obj, null);

                AssertProperty(validation, propertyName, propertyValue);
            }

            return this;
        }

        private void AssertProperty<TProperty>(
            Action<IAssertionSyntax<TProperty>> validation, 
            string propertyName, 
            TProperty propertyValue)
        {
            using (_context.PrependKeysWithName(propertyName))
            {
                validation(new AssertionEngine<TProperty>(_context, propertyValue));
            }
        }
    }
}