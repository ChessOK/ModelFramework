using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class NullAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Model.Get<NullValidator>();
        }
    }
}
