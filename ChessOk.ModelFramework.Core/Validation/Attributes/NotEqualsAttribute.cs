using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class NotEqualsAttribute : ValidateAttribute
    {
        public NotEqualsAttribute(object obj)
        {
            Object = obj;
        }

        public object Object { get; private set; }

        public string ErrorMessage { get; set; }

        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Get<DelegateValidator>();
            validator.Delegate = x =>
                {
                    var left = x != null ? x.ToString() : null;
                    var right = Object != null ? Object.ToString() : null;

                    return left != right;
                };
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
