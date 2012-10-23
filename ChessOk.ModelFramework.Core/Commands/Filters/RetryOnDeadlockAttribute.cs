using System;

namespace ChessOk.ModelFramework.Commands.Filters
{
    public class RetryOnDeadlockAttribute : CommandFilterAttribute
    {
        public RetryOnDeadlockAttribute()
        {
            RetryAttemptsCount = 5;
            Order = 1000;
        }

        public int RetryAttemptsCount { get; set; }

        public override void OnInvoke(CommandFilterContext filterContext, Action commandAction)
        {
            var attempt = 0;

            while (true)
            {
                attempt++;
                try
                {
                    commandAction();
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