namespace ChessOk.ModelFramework.Validation.Validators
{
    public class ObjectValidator : CompositeValidator
    {
        public ObjectValidator()
            : base(new AttributesValidator(), new ValidatableObjectValidator())
        {
        }
    }
}
