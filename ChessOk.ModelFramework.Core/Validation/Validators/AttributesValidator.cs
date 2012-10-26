using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class AttributesValidator : Validator
    {
        public static ConcurrentDictionary<Type, Func<IValidationContext, object, IValidator>> DataAnnotationsConverters =
            new ConcurrentDictionary<Type, Func<IValidationContext, object, IValidator>>();

        static AttributesValidator()
        {
            DataAnnotationsConverters.TryAdd(typeof(RequiredAttribute), (context, attribute) =>
                {
                    var required = (RequiredAttribute)attribute;
                    return new RequiredValidator(context)
                        {
                            AllowEmptyStrings = required.AllowEmptyStrings,
                            Message = required.ErrorMessage
                        };
                });

            DataAnnotationsConverters.TryAdd(typeof(RegularExpressionAttribute), (context, attribute) =>
                {
                    var regular = (RegularExpressionAttribute)attribute;
                    return new RegularExpressionValidator(context)
                        {
                            Message = regular.ErrorMessage,
                            Pattern = regular.Pattern
                        };
                });
        }

        public AttributesValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public bool DoNotModifyErrorKeys { get; set; }

        public override void Validate(object obj)
        {
            if (obj == null)
            {
                return;
            }

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
                        if (DoNotModifyErrorKeys)
                        {
                            validator.Validate(value);
                        }
                        else
                        {
                            using (ValidationContext.PrependKeysWithName(propertyInfo.Name))
                            {
                                validator.Validate(value);
                            }
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

            var annotationsAttribute = attribute as ValidationAttribute;
            if (annotationsAttribute != null)
            {
                var annotationType = annotationsAttribute.GetType();

                if (DataAnnotationsConverters.ContainsKey(annotationType))
                {
                    validator = DataAnnotationsConverters[annotationType](
                        ValidationContext, annotationsAttribute);
                }
                else
                {
                    validator = new DelegateValidator(ValidationContext)
                    {
                        Delegate = annotationsAttribute.IsValid,
                        Message = annotationsAttribute.FormatErrorMessage("value")
                    };
                }
            }
            return validator;
        }
    }
}
