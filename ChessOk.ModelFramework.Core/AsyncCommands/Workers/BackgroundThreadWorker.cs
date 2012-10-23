using System;
using System.Threading;

using ChessOk.ModelFramework.AsyncCommands.Handlers;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.Logging;

namespace ChessOk.ModelFramework.AsyncCommands.Workers
{
    public class BackgroundThreadWorker : IDisposable
    {
        private static readonly TimeSpan ThreadJoinTimeout = TimeSpan.FromSeconds(5);

        private readonly object _lock = new object();

        private readonly IAsyncCommandQueue _queue;
        private readonly IAsyncCommandHandler _handler;

        private Thread _workerThread;
        private long _stopping;

        protected ILog Log = LogManager.Get();

        public BackgroundThreadWorker(IAsyncCommandQueue queue, IAsyncCommandHandler handler)
        {
            if (queue == null)
            {
                throw new ArgumentNullException("queue");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            _queue = queue;
            _handler = handler;
        } 

        public void Start()
        {
            lock (_lock)
            {
                if (_workerThread == null)
                {
                    Interlocked.Exchange(ref _stopping, 0);

                    _workerThread = new Thread(Work);
                    _workerThread.Name = "BackgroundThreadMessageQueueListener";
                    _workerThread.Start();
                }
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (_workerThread != null)
                {
                    Interlocked.Exchange(ref _stopping, 1);
                    _workerThread.Join(ThreadJoinTimeout);
                    _workerThread.Abort();
                    _workerThread = null;
                }
            }
        }

        public bool IsActive
        {
            get
            {
                lock (_lock)
                {
                    return _workerThread != null;
                }
            }
        }

        protected void Work()
        {
            Log.Debug("Background queue listener has been started");

            while (Interlocked.Read(ref _stopping) == 0)
            {
                try
                {
                    var message = _queue.Dequeue();
                    if (message != null)
                    {
                        Log.Debug(String.Format("Handling message of type {0}", message.GetType().FullName));

                        _handler.Handle(message);
                    }
                    else
                    {
                        Thread.Sleep(40);
                    }
                }
                catch(AsyncCommandHandlingException ex)
                {
                    Log.Error("Error during handling the message", ex);        
                }
                catch (ThreadAbortException)
                {
                    Log.Warning("Listener Thread has been aborted. Some messages could be lost.");
                    Stop();
                }
                catch (Exception ex)
                {
                    // Не ломаем все приложение, но записываем
                    Log.Error("Unrecognized exception has been thrown", ex);
                }
            }

            Log.Debug("Background queue listener has been stopped");
        }

        public void Dispose()
        {
            _queue.Dispose();
        }
    }
}