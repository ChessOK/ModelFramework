using System;

using ChessOk.ModelFramework.AsyncCommands.Handlers;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Logging;

namespace ChessOk.ModelFramework.AsyncCommads.Handlers
{
    /// <summary>
    /// Обрабатывает сообщения, создавая для каждого отдельный ModelContext
    /// </summary>
    public abstract class SeparatedContextsHandler : IAsyncCommandHandler
    {
        protected ILog Log = LogManager.Get();

        public void Handle(CommandBase asyncCommand)
        {
            if (asyncCommand == null)
            {
                throw new ArgumentNullException("asyncCommand");
            }

            try
            {
                using (var context = CreateContext())
                {
                    using (var appBus = new ApplicationBus(context))
                    {
                        appBus.Handle(asyncCommand);
                        Log.Debug(String.Format("Command has been handled successfully: {0}", asyncCommand));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AsyncCommandHandlingException(ex);
            }
        }

        protected abstract ModelContext CreateContext();
    }
}