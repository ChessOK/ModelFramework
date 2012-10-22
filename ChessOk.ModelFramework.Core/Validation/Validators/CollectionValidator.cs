using System.Collections.Generic;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class CollectionValidator : IValidator
    {
        public IValidationContext ValidationContext { get; set; }

        public void Validate(object obj)
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
                    var validator = new ObjectValidator { ValidationContext = ValidationContext };
                    validator.Validate(o);
                }

                index++;
            }
        }
    }
}
