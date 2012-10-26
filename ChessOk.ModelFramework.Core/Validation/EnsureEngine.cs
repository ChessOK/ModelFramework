using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Expressions;

namespace ChessOk.ModelFramework.Validation.Internals
{
    public class EnsureEngine<TObject> : IEnsureSyntax<TObject>
    {
        private readonly IValidationContext _validationContext;
        private readonly TObject _obj;

        public EnsureEngine(IValidationContext validationContext, TObject obj)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            _validationContext = validationContext;
            _obj = obj;
        }

        public IValidationContext ValidationContext
        {
            get { return _validationContext; }
        }

        public IEnsureSyntax<TObject> IsValid(IValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            validator.Validate(_obj);
            
            return this;
        }

        public IEnsureSyntax<TObject> ItsProperty<TProperty>(
            Expression<Func<TObject, TProperty>> propertyExpression, 
            Action<IEnsureSyntax<TProperty>> validation)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            if (_obj != null)
            {
                var propertyName = ExpressionTextHelper.GetExpressionText(propertyExpression);
                var propertyValue = CachedExpressionCompiler.CompileGetter(propertyExpression)(_obj);

                AssertProperty(validation, propertyName, propertyValue);
            }

            return this;
        }

        public IEnsureSyntax<TObject> ItsProperty<TProperty>(string propertyName, Action<IEnsureSyntax<TProperty>> validation)
        {
            if (_obj != null)
            {
                var propertyInfo = _obj.GetType().GetProperty(propertyName);

                if (propertyInfo == null)
                {
                    throw new InvalidOperationException(
                        String.Format("Could not find property «{0}» in type «{1}»", propertyName, _obj.GetType().Name));
                }

                var propertyValue = (TProperty)propertyInfo.GetValue(_obj, null);

                AssertProperty(validation, propertyName, propertyValue);
            }

            return this;
        }

        private void AssertProperty<TProperty>(
            Action<IEnsureSyntax<TProperty>> validation, 
            string propertyName, 
            TProperty propertyValue)
        {
            if (validation == null)
            {
                throw new ArgumentNullException("validation");
            }

            using (_validationContext.PrependKeysWithName(propertyName))
            {
                validation(new EnsureEngine<TProperty>(_validationContext, propertyValue));
            }
        }
    }
}