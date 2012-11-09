using System;

using Autofac;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Logging;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// Обрабатывает команды, создавая для каждой отдельный ModelContext.
    /// </summary>
    public class SeparatedContextsHandler : IAsyncCommandHandler
    {
        private readonly ILifetimeScope _scope;

        /// <summary>
        /// Инициализировать экземпляр класса <see cref="SeparatedContextsHandler"/>,
        /// используя <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="logger"></param>
        public SeparatedContextsHandler(ILifetimeScope scope, ILogger logger)
        {
            _scope = scope;
            Logger = logger;
        }

        protected ILogger Logger;

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
                    using (var appBus = model.Get<IApplicationBus>())
                    {
                        appBus.Send(asyncCommand);
                        Logger.Debug(String.Format("Command has been handled successfully: {0}", asyncCommand));
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