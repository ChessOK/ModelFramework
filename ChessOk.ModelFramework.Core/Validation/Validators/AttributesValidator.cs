using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Валидатор сканирует все свойства проверяемого объекта на наличие
    /// специализированных атрибутов, и, на их основе, запускает валидацию свойств.
    /// </summary>
    /// <remarks>
    /// Проверка осуществляется на основе атрибутов двух типов:
    /// 
    /// <list type="table">
    ///   <listheader>
    ///     <term>Тип атрибута</term>      
    ///     <description>Метод проверки</description>
    ///   </listheader>
    ///   <item>
    ///     <term><see cref="ValidateAttribute"/> из ModelFramework.</term>
    ///     <description>
    ///       Для проверки используется валидатор, полученный с помощью метода <c>GetValidator</c>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="ValidationAttribute"/> из System.ComponentModel.DataAnnotations</term>
    ///     <description>
    ///       <para>Для валидации используется метод IsValid,
    ///       однако для атрибутов <see cref="RequiredAttribute"/> и <see cref="RegularExpressionAttribute"/>
    ///       используются валидаторы <see cref="RequiredValidator"/> и <see cref="RegularExpressionValidator"/> 
    ///       соответственно.</para>
    ///       <para>Также можно добавить свои переопределения, используя поле <see cref="DataAnnotationsConverters"/>.</para>
    ///     </description>
    ///   </item>
    /// </list>
    /// 
    /// Для одного свойства можно указать несколько атрибутов разных типов.
    /// </remarks>
    public class AttributesValidator : Validator
    {
        /// <summary>
        /// Словарь содержит список типов валидационных атрибутов-исключений из DataAnnotations, на которые
        /// требуется создание собственного валидатора из ModelFramework, нежели использование собственного метода IsValid.
        /// </summary>
        /// 
        /// <remarks>
        /// В качестве ключа выступает тип, унаследованный от <seealso cref="ValidationAttribute"/>,
        /// в качестве значения — функция, принимающая экземпляр атрибута и валидационный контекст и 
        /// возвращающая нужный <seealso cref="IValidator"/>.
        /// </remarks>
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

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AttributesValidator"/>,
        /// используя заданный валидационный контекст.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        public AttributesValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            UsePropertyNamesAsErrorKeys = true;
        }

        /// <summary>
        /// Получает или задает значение, указывающее, выставлять или
        /// нет в качестве ключей ошибок имена свойств (по-умолчанию: true). 
        /// </summary>
        public bool UsePropertyNamesAsErrorKeys { get; set; }

        /// <summary>
        /// Проверяет свойства указанного объекта на основе их атрибутов. Свойства, не
        /// помеченные одним из валидационных атрибутов, пропускаются.
        /// </summary>
        /// 
        /// <remarks>
        /// Все валидационные ошибки записываются в экземпляр <see cref="ValidationContext"/>,
        /// указанный при создании экземпляра данного класса.
        /// 
        /// Если валидируемый объект имеет значение <c>null</c>, то валидация завершается 
        /// успешно. Используйте <see cref="RequiredValidator"/>,
        /// если хотите проверить, имеет ли объект значение.
        /// 
        /// По-умолчанию в качестве ключей ошибок выступают имена свойств объекта, 
        /// это поведение можно изменить в параметре <see cref="UsePropertyNamesAsErrorKeys"/>.
        /// </remarks>
        /// 
        /// <param name="obj">Валидируемый объект.</param>
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
                        if (UsePropertyNamesAsErrorKeys)
                        {
                            using (ValidationContext.PrefixErrorKeysWithName(propertyInfo.Name))
                            {
                                validator.Validate(value);
                            }
                        }
                        else
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
