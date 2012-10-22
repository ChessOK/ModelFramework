namespace ChessOk.ModelFramework.Validation.Validators
{
    public class CompositeValidator : IValidator
    {
        private readonly IValidator[] _validators;

        public CompositeValidator(params IValidator[] validators)
        {
            _validators = validators;
        }

        public IValidationContext ValidationContext { get; set; }

        public void Validate(object obj)
        {
            foreach (var validator in _validators)
            {
                validator.ValidationContext = ValidationContext;
                validator.Validate(obj);
            }
        }
    }
}
