namespace ChessOk.ModelFramework.Validation
{
    public interface IValidator
    {
        IValidationContext ValidationContext { get; set; }
        void Validate(object obj);
    }
}
