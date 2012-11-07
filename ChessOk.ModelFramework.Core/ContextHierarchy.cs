using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    internal static class ContextHierarchy
    {
        public static object ModelContext = typeof(ModelContext);
        public static object ApplicationBus = typeof(ApplicationBus);
        public static object ValidationContext = typeof(ValidationContext);
    }
}
