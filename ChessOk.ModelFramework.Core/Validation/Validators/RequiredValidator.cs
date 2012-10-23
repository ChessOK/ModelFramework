namespace ChessOk.ModelFramework.Validation.Validators
{
    public class RequiredValidator : Validator
    {
        public RequiredValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public bool AllowEmptyStrings { get; set; }

        public override void Validate(object obj)
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
