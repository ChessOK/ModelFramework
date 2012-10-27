using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidItemsAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Model.Get<CollectionValidator>();
        }
    }
}
