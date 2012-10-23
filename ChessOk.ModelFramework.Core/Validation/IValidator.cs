namespace ChessOk.ModelFramework.Validation
{
    public interface IValidator
    {
        IValidationContext ValidationContext { get; }
        void Validate(object obj);
    }
}
