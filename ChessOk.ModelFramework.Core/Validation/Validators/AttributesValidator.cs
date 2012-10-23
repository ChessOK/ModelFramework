using System.Linq;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class AttributesValidator : Validator
    {
        public AttributesValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public override void Validate(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var constraintAttributes = propertyInfo
                    .GetCustomAttributes(typeof(ValidateAttribute), true)
                    .Cast<ValidateAttribute>().ToList();

                if (!constraintAttributes.Any())
                {
                    continue;
                }

                var value = propertyInfo.GetValue(obj, null);
                foreach (var attribute in constraintAttributes)
                {
                    attribute.ValidationContext = ValidationContext;

                    var validator = attribute.GetValidator();

                    using (ValidationContext.PrependKeysWithName(propertyInfo.Name))
                    {
                        validator.Validate(value);
                    }
                }
            }

        }
    }
}
