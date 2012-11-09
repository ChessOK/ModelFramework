using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Предоставляет теги для <see cref="IModelContext"/> и его
    /// метода <see cref="IModelContext.CreateChildContext"/>. Полезен,
    /// если вы создаете собственные реализации указанных здесь
    /// интерфейсов.
    /// </summary>
    public static class ContextHierarchy
    {
        /// <summary>
        /// Тег LifetimeScope для класса, реализующего интерфейс
        /// <see cref="IModelContext"/>.
        /// </summary>
        public static object ModelContext = typeof(IModelContext);

        /// <summary>
        /// Тег LifetimeScope для класса, реализующего интерфейс
        /// <see cref="IApplicationBus"/>.
        /// </summary>
        public static object ApplicationBus = typeof(IApplicationBus);

        /// <summary>
        /// Тег LifetimeScope для класса, реализующего интерфейс
        /// <see cref="IValidationContext"/>.
        /// </summary>
        public static object ValidationContext = typeof(IValidationContext);
    }
}
