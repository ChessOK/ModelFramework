using System;

using Autofac;

using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Logging;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// Обрабатывает сообщения, создавая для каждого отдельный ModelContext
    /// </summary>
    public class SeparatedContextsHandler : IAsyncCommandHandler
    {
        private readonly ILifetimeScope _scope;

        public SeparatedContextsHandler(ILifetimeScope scope)
        {
            _scope = scope;
        }

        protected ILog Log = LogManager.Get();

        public void Handle(CommandBase asyncCommand)
        {
            if (asyncCommand == null)
            {
                throw new ArgumentNullException("asyncCommand");
            }

            try
            {
                using (var model = CreateModel())
                {
                    using (var appBus = new ApplicationBus(model))
                    {
                        appBus.Send(asyncCommand);
                        Log.Debug(String.Format("Command has been handled successfully: {0}", asyncCommand));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AsyncCommandHandlingException(ex);
            }
        }

        protected virtual ModelContext CreateModel()
        {
            return new ModelContext(_scope);
        }
    }
}