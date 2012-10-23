using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    .Union(propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true))
                    .ToArray();

                if (!constraintAttributes.Any())
                {
                    continue;
                }

                var value = propertyInfo.GetValue(obj, null);
                foreach (var attribute in constraintAttributes)
                {
                    var validator = GetValidatorForAttribute(attribute);

                    if (validator != null)
                    {
                        using (ValidationContext.PrependKeysWithName(propertyInfo.Name))
                        {
                            validator.Validate(value);
                        }
                    }
                }
            }
        }

        private IValidator GetValidatorForAttribute(object attribute)
        {
            IValidator validator = null;
            var validationAttribute = attribute as ValidateAttribute;
            if (validationAttribute != null)
            {
                validationAttribute.ValidationContext = ValidationContext;
                validator = validationAttribute.GetValidator();
            }

            var dataAnnotationsAttribute = attribute as ValidationAttribute;
            if (dataAnnotationsAttribute != null)
            {
                validator = GetValidatorForDataAnnotations(dataAnnotationsAttribute);
            }
            return validator;
        }

        protected virtual IValidator GetValidatorForDataAnnotations(ValidationAttribute attribute)
        {
            var required = attribute as RequiredAttribute;
            if (required != null)
            {
                return new RequiredValidator(ValidationContext)
                    { AllowEmptyStrings = required.AllowEmptyStrings };
            }

            var range = attribute as RangeAttribute;
            if (range != null)
            {
                throw new NotImplementedException();
            }

            var regularExpression = attribute as RegularExpressionAttribute;
            if (regularExpression != null)
            {
                throw new NotImplementedException();
            }

            var stringLength = attribute as StringLengthAttribute;
            if (stringLength != null)
            {
                throw new NotImplementedException();
            }

            return null;
        }
    }
}
