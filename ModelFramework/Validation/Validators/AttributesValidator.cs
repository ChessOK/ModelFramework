﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Autofac;

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
    public class AttributesValidator : IValidator
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
                    var validator = context.ModelContext.LifetimeScope.Resolve<RequiredValidator>();
                    validator.AllowEmptyStrings = required.AllowEmptyStrings;
                    validator.Message = required.ErrorMessage;
                    return validator;
                });

            DataAnnotationsConverters.TryAdd(typeof(RegularExpressionAttribute), (context, attribute) =>
                {
                    var regular = (RegularExpressionAttribute)attribute;
                    var validator = context.ModelContext.LifetimeScope.Resolve<RegularExpressionValidator>();
                    validator.Message = regular.ErrorMessage;
                    validator.Pattern = regular.Pattern;
                    return validator;
                });
        }

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
        /// В качестве ключей ошибок выступают имея свойства объекта, если к нему не
        /// применен атрибут <see cref="FlatErrorKeysAttribute"/>.
        /// </remarks>
        /// 
        /// <param name="obj">Валидируемый объект.</param>
        /// <param name="context">Валидационный контекст.</param>
        public void Validate(IValidationContext context, object obj)
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

                var flatKeys = propertyInfo
                    .GetCustomAttributes(typeof(FlatErrorKeysAttribute), true)
                    .Any();

                var value = propertyInfo.GetValue(obj, null);
                
                if (flatKeys)
                {
                    ValidationAction(context, value, constraintAttributes);
                }
                else
                {
                    using (context.PrefixErrorKeysWithName(propertyInfo.Name))
                    {
                        ValidationAction(context, value, constraintAttributes);
                    }
                }
            }
        }

        private void ValidationAction(IValidationContext context, object value, IEnumerable<object> attributes)
        {
            foreach (var attribute in attributes)
            {
                var validator = GetValidatorForAttribute(context, attribute);

                if (validator != null)
                {
                    validator.Validate(context, value);
                }
            }
        }

        private IValidator GetValidatorForAttribute(IValidationContext context, object attribute)
        {
            IValidator validator = null;
            var validationAttribute = attribute as ValidateAttribute;
            if (validationAttribute != null)
            {
                validator = validationAttribute.GetValidator(context.ModelContext.LifetimeScope);
            }

            var annotationsAttribute = attribute as ValidationAttribute;
            if (annotationsAttribute != null)
            {
                var annotationType = annotationsAttribute.GetType();

                if (DataAnnotationsConverters.ContainsKey(annotationType))
                {
                    validator = DataAnnotationsConverters[annotationType](context, annotationsAttribute);
                }
                else
                {
                    var delegateValidator = context.ModelContext.LifetimeScope.Resolve<DelegateValidator>();
                    delegateValidator.Delegate = annotationsAttribute.IsValid;
                    delegateValidator.Message = annotationsAttribute.FormatErrorMessage("value");

                    validator = delegateValidator;
                }
            }

            return validator;
        }
    }
}
