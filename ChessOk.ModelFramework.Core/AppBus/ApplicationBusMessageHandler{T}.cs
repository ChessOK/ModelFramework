using System;

namespace ChessOk.ModelFramework.Messages
{
    /// <summary>
    /// ������ ������� ������� ��� ���������� ������������ ���������
    /// ���� <typeparamref name="T"/>.
    /// </summary>
    /// 
    /// <remarks>
    /// � ������� �� <see cref="ApplicationBusMessageHandler"/>, �� ������������
    /// ������ ��������� ���� <typeparamref name="T"/>, ��������� ��� ������ ����.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� �������������� ���������.</typeparam>
    public abstract class ApplicationBusMessageHandler<T> : ApplicationBusMessageHandler
        where T : class, IApplicationBusMessage
    {
        protected abstract void Handle(T message);

        public sealed override void Handle(IApplicationBusMessage ev)
        {
            var casted = ev as T;
            if (casted == null)
            {
                return;
            }

            Handle(casted);
        }
    }
}