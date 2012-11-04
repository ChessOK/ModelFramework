using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ���������� ��������� <see cref="ICommandInvokedMessage{T}"/>, �������
    /// ���������� � ���� ����� ���������� ������� � ����� <typeparamref name="T"/> 
    /// (��������� <see cref="CommandBase"/>).
    /// </summary>
    /// 
    /// <remarks>
    /// ��� ��� � ��������� <see cref="ICommandInvokedMessage{T}"/> �������� <typeparamref name="T"/>
    /// �������� ������������, �� ���������� ����� ������ ����� ���������� ���� ������, 
    /// ���������� ������������ ���� <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� ����������� �������, ��������� <see cref="CommandBase"/></typeparam>
    public abstract class CommandInvokedHandler<T> : ApplicationBusMessageHandler<ICommandInvokedMessage<T>>
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return CommandInvokedMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T command);

        protected sealed override void Handle(ICommandInvokedMessage<T> message)
        {
            Handle(message.Command);
        }
    }
}