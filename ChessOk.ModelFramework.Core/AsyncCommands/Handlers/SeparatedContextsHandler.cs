using System;

using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Logging;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// ������������ ���������, �������� ��� ������� ��������� ModelContext
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

        protected abstract ModelContext CreateContext();
    }
}