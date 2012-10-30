using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Expressions;

namespace ChessOk.ModelFramework.Validation.Internals
{
    /// <summary>
    /// ѕредоставл€ет механизм дл€ осуществлени€ валидации на основе
    /// fluent-синтаксиса (<see cref="IEnsureSyntax{TObject}"/>) 
    /// и валидаторов (<see cref="IValidator"/>).
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class EnsureEngine<TObject> : IEnsureSyntax<TObject>
    {
        private readonly IValidationContext _validationContext;
        private readonly TObject _obj;

        /// <summary>
        /// »нициализирует экземпл€р класса <see cref="EnsureEngine{TObject}"/>,
        /// использу€ указанный <paramref name="validationContext"/> и валидируемый
        /// объект <paramref name="obj"/>.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <param name="obj"></param>
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
                        String.Format("Could not find property Ђ{0}ї in type Ђ{1}ї", propertyName, _obj.GetType().Name));
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

            using (_validationContext.PrefixErrorKeysWithName(propertyName))
            {
                validation(new EnsureEngine<TProperty>(_validationContext, propertyValue));
            }
        }
    }
}