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
            var enumerable = obj as IEnumerable<object>;

            if (enumerable == null)
            {
                return;
            }

            var index = 0;
            foreach (var o in enumerable)
            {
                using (ValidationContext.ReplaceKeys("^$", string.Format("[{0}]", index)))
                using (ValidationContext.ReplaceKeys("^(.+)$", string.Format("[{0}].$1", index)))
                {
                    var validator = new ObjectValidator(ValidationContext);
                    validator.Validate(o);
                }

                index++;
            }
        }
    }
}
