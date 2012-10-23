using System;

namespace ChessOk.ModelFramework.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidateAttribute : Attribute
    {
        public IValidationContext ValidationContext { get; internal set; }

        public abstract IValidator GetValidator();
    }
}
