using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.Commands.Filters
{
    public class CommandFilterContext
    {
        public CommandFilterContext(IApplicationBus bus, CommandBase command)
        {
            Bus = bus;
            Command = command;
        }

        public IApplicationBus Bus { get; private set; }
        public CommandBase Command { get; private set; }
    }
}
