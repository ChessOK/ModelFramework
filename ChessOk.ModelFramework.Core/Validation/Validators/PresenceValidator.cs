namespace ChessOk.ModelFramework.Validation.Validators
{
    public class PresenceValidator : IValidator
    {
        public bool AllowEmptyStrings { get; set; }

        public IValidationContext ValidationContext { get; set; }

        public void Validate(object obj)
        {
            var message = Resources.Strings.PresenceValidatorMessage;

            if (obj == null)
            {
                ValidationContext.AddError(message);
            }
            
            var str = obj as string;
            if (str != null)
            {
                if (!AllowEmptyStrings && str.Trim().Length == 0)
                {
                    ValidationContext.AddError(message);
                }
            }
        }
    }
}
