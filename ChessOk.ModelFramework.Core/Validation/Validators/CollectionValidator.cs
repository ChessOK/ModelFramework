using System;
using System.Collections.Generic;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class CollectionValidator : Validator
    {
        public CollectionValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public override void Validate(object obj)
        {
            if (obj == null) { return; }

            var enumerable = obj as IEnumerable<object>;

            if (enumerable == null)
            {
                throw new InvalidOperationException(
                    String.Format(Resources.Strings.CollectionValidatorInvalidObject, GetType(), obj.GetType()));
            }

            var index = 0;
            foreach (var o in enumerable)
            {
                using (ValidationContext.ModifyKeys("^$", string.Format("[{0}]", index)))
                using (ValidationContext.ModifyKeys("^(.+)$", string.Format("[{0}].$1", index)))
                {
                    var validator = new ObjectValidator(ValidationContext);
                    validator.Validate(o);
                }

                index++;
            }
        }
    }
}
