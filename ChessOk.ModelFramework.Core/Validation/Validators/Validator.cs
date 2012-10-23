using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public abstract class Validator : IValidator
    {
        protected Validator(IValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            ValidationContext = validationContext;
        }

        public IValidationContext ValidationContext { get; private set; }

        public abstract void Validate(object obj);
    }
}
