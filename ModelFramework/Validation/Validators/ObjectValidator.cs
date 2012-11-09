using System;

using Autofac;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Составной валидатор, производит валидацию объекта путем 
    /// вызова <see cref="AttributesValidator"/> и <see cref="ValidatableObjectValidator"/>.
    /// </summary>
    public class ObjectValidator : IValidator
    {
        private readonly IValidator _attributesValidator;
        private readonly IValidator _validatableObjectValidator;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ObjectValidator"/>,
        /// используя <paramref name="lifetimeScope"/>.
        /// </summary>
        /// <param name="lifetimeScope"></param>
        public ObjectValidator(ILifetimeScope lifetimeScope)
        {
            _attributesValidator = lifetimeScope.Resolve<AttributesValidator>();
            _validatableObjectValidator = lifetimeScope.Resolve<ValidatableObjectValidator>();   
        }

        public void Validate(IValidationContext context, object obj)
        {
            _attributesValidator.Validate(context, obj);
            _validatableObjectValidator.Validate(context, obj);
        }
    }
}
