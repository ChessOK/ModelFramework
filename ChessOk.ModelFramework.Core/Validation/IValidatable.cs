namespace ChessOk.ModelFramework.Validation
{
    public interface IValidatable
    {
        void Validate(IValidationContext context);
    }
}
