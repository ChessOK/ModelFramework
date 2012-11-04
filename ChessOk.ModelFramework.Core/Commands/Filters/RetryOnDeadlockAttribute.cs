using System;

namespace ChessOk.ModelFramework.Commands.Filters
{
    /// <summary>
    /// Позволяет выполнить команду повторно, если при её выполнении
    /// возникла взаимная блокировка на стороне MS SQL Server. 
    /// </summary>
    public class RetryOnDeadlockAttribute : CommandFilterAttribute
    {
        /// <summary>
        /// Инициализировать экземпляр класса <see cref="RetryOnDeadlockAttribute"/>
        /// с количеством повторных попыток равным 5.
        /// </summary>
        public RetryOnDeadlockAttribute()
        {
            RetryAttemptsCount = 5;
            Order = 1000;
        }

        /// <summary>
        /// Определяет количество повторных попыток выполнения команды
        /// в случае возникшей взаимной блокировки.
        /// </summary>
        public int RetryAttemptsCount { get; set; }

        public override void Apply(CommandFilterContext filterContext, Action commandInvocation)
        {
            var attempt = 0;

            while (true)
            {
                attempt++;
                try
                {
                    commandInvocation();
                    return;
                }
                catch (Exception ex)
                {
                    if (!SqlExceptionHelper.IsDeadlock(ex))
                    {
                        throw;
                    }

                    if (attempt >= RetryAttemptsCount)
                    {
                        throw;
                    }
                }
            }
        }
    }
}