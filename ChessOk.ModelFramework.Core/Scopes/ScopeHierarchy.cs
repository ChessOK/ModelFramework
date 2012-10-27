using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework.Scopes
{
    internal static class ScopeHierarchy
    {
        public static object ModelContext = typeof(ModelContext);
        public static object ApplicationBus = typeof(ApplicationBus);
        public static object ValidationContext = typeof(ValidationContext);
    }
}
