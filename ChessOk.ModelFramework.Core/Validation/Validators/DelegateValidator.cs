using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class DelegateValidator : Validator
    {
        public DelegateValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public Func<object, bool> Delegate { get; set; }
        public string Message { get; set; }

        public override void Validate(object obj)
        {
            if (!Delegate(obj))
            {
                ValidationContext.AddError(Message);
            }
        }
    }
}
