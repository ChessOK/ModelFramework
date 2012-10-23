using ChessOk.ModelFramework.Validation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Testing
{
    [TestClass]
    public abstract class ValidationTest : ApplicationBusTest
    {
        protected IValidationContext ValidationContext { get; private set; }

        [TestInitialize]
        public void InitializeValidation()
        {
            ValidationContext = Bus.ValidationContext;
        }
    }
}
