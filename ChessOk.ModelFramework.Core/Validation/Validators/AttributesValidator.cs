using System.Linq;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class AttributesValidator : IValidator
    {
        public IValidationContext ValidationContext { get; set; }

        public void Validate(object obj)
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
                    var validator = attribute.GetValidator();
                    validator.ValidationContext = ValidationContext;

                    using (ValidationContext.PrependKeysWithName(propertyInfo.Name))
                    {
                        validator.Validate(value);
                    }
                }
            }

        }
    }
}
