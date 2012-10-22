using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateCollectionItems : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return new CollectionValidator();
        }
    }
}
