using System.Collections.Generic;
using System.Linq;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class CompositeValidator : Validator
    {
        public CompositeValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            Validators = Enumerable.Empty<IValidator>();
        }

        public IEnumerable<IValidator> Validators { get; set; }

        public override void Validate(object obj)
        {
            foreach (var validator in Validators)
            {
                validator.Validate(obj);
            }
        }
    }
}
