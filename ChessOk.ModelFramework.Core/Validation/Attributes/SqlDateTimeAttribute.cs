using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class SqlDateTimeAttribute : ValidateAttribute
    {
        public string ErrorMessage { get; set; }

        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Model.Get<SqlDateTimeValidator>();
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
