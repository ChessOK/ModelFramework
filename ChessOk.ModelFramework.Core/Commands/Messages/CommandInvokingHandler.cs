using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ���������� ��������� <see cref="ICommandInvokingMessage{T}"/>, �������
    /// ���������� � ���� ����� ����������� ������� � ����� <typeparamref name="T"/>
    /// (��������� <see cref="CommandBase"/>).
    /// </summary>
    /// 
    /// <remarks>
    /// ��� ��� � ��������� <see cref="ICommandInvokingMessage{T}"/> �������� <typeparamref name="T"/>
    /// �������� ������������, �� ���������� ����� ������ ����� ����������� ���� ������, 
    /// ���������� ������������ ���� <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">��� ����������� � ������� �������, ��������� <see cref="CommandBase"/></typeparam>
    public abstract class CommandInvokingHandler<T> : ApplicationBusMessageHandler<ICommandInvokingMessage<T>>
        where T : CommandBase
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return CommandInvokingMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T command, out bool cancelInvocation);

        protected sealed override void Handle(ICommandInvokingMessage<T> message)
        {
            bool cancelInvocation;
            Handle(message.Command, out cancelInvocation);

            if (cancelInvocation)
            {
                message.CancelInvocation();
            }
        }
    }
}