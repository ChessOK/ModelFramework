using System;
using System.Threading;

using ChessOk.ModelFramework.AsyncCommands.Handlers;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.Logging;

namespace ChessOk.ModelFramework.AsyncCommands.Workers
{
    /// <summary>
    /// Создает отдельный поток для проверки очереди асинхронных
    /// команд <see cref="IAsyncCommandQueue"/> на наличие необработанных команд,
    /// и передает их на обработку, используя <see cref="IAsyncCommandHandler"/>.
    /// </summary>
    public class BackgroundThreadWorker : IDisposable
    {
        private static readonly TimeSpan ThreadJoinTimeout = TimeSpan.FromSeconds(5);

        private readonly object _lock = new object();

        private readonly IAsyncCommandQueue _queue;
        private readonly IAsyncCommandHandler _handler;

        private Thread _workerThread;
        private long _stopping;

        protected ILogger Logger;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="BackgroundThreadWorker"/>,
        /// используя <paramref name="queue"/> и <paramref name="handler"/>.
        /// </summary>
        /// <param name="queue">Наблюдаемая очередь.</param>
        /// <param name="handler">Обработчик команд.</param>
        /// /// <param name="logger"></param>
        public BackgroundThreadWorker(IAsyncCommandQueue queue, IAsyncCommandHandler handler, ILogger logger)
        {
            if (queue == null)
            {
                throw new ArgumentNullException("queue");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _queue = queue;
            _handler = handler;
            Logger = logger;
        } 

        /// <summary>
        /// Создает поток и начинает слежение за очередью
        /// асинхронных команд.
        /// </summary>
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

        /// <summary>
        /// Останавливает слежение за очередью асинхронных
        /// команд.
        /// </summary>
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

        /// <summary>
        /// Получает значение, поясняющее, запущено ли слежение 
        /// за очередью асинхронных команд.
        /// </summary>
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
            Logger.Debug("Background queue listener has been started");

            while (Interlocked.Read(ref _stopping) == 0)
            {
                try
                {
                    var message = _queue.Dequeue();
                    if (message != null)
                    {
                        Logger.Debug(String.Format("Handling message of type {0}", message.GetType().FullName));

                        _handler.Handle(message);
                    }
                    else
                    {
                        Thread.Sleep(40);
                    }
                }
                catch(AsyncCommandHandlingException ex)
                {
                    Logger.Error("Error during handling the message", ex);        
                }
                catch (ThreadAbortException)
                {
                    Logger.Warning("Listener Thread has been aborted. Some messages could be lost.");
                    Stop();
                }
                catch (Exception ex)
                {
                    // Не ломаем все приложение, но записываем
                    Logger.Error("Unrecognized exception has been thrown", ex);
                }
            }

            Logger.Debug("Background queue listener has been stopped");
        }

        public void Dispose()
        {
            _queue.Dispose();
        }
    }
}