using System;

namespace ChessOk.ModelFramework.Commands.Filters
{
    /// <summary>
    /// ��������� ��������� ������� ��������, ���� ��� � ����������
    /// �������� �������� ���������� �� ������� MS SQL Server. 
    /// </summary>
    public class RetryOnDeadlockAttribute : CommandFilterAttribute
    {
        /// <summary>
        /// ���������������� ��������� ������ <see cref="RetryOnDeadlockAttribute"/>
        /// � ����������� ��������� ������� ������ 5.
        /// </summary>
        public RetryOnDeadlockAttribute()
        {
            RetryAttemptsCount = 5;
            Order = 1000;
        }

        /// <summary>
        /// ���������� ���������� ��������� ������� ���������� �������
        /// � ������ ��������� �������� ����������.
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