using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class DelegateValidator<T> : IValidator
    {
        private readonly Func<T, bool> _delegate;
        private readonly string _message;

        public DelegateValidator(Func<T, bool> @delegate, string message)
        {
            _delegate = @delegate;
            _message = message;
        }

        public IValidationContext ValidationContext { get; set; }

        public void Validate(object obj)
        {
            try
            {
                var casted = (T)obj;

                if (!_delegate(casted))
                {
                    ValidationContext.AddError(_message);
                }
            }
            catch (InvalidCastException)
            {
            }
        }
    }
}
