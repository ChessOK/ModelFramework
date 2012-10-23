using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateSqlDateTimeAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Get<SqlDateTimeValidator>();
        }
    }
}
