namespace ChessOk.ModelFramework.Commands.Filters
{
    /// <summary>
    /// Служит оболочкой для контекста применения фильтров команд.
    /// </summary>
    public class CommandFilterContext
    {
        internal CommandFilterContext(IApplicationBus bus, CommandBase command)
        {
            Bus = bus;
            Command = command;
        }

        /// <summary>
        /// Экземпляр текущей шины приложения.
        /// </summary>
        public IApplicationBus Bus { get; private set; }

        /// <summary>
        /// Экземпляр выполняемой команды.
        /// </summary>
        public CommandBase Command { get; private set; }
    }
}
