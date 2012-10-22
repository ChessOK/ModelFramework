using System;

namespace ChessOk.ModelFramework.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidateAttribute : Attribute
    {
        public abstract IValidator GetValidator();
    }
}
