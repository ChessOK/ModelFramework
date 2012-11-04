using System;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// ������������� ����������� ����������� ������ ��������
    /// � �������, ������� ��������� ����������. ��� � ���������� 
    /// ����� ������ ���������� � ������������ �������. � ��������
    /// ���������� ������� ������������ ��������� ���������� ��������.
    /// </summary>
    public class InlineCommand<T> : Command<T>
    {
        /// <summary>
        /// ���������������� ��������� ������ <see cref="InlineCommand"/>,
        /// ��������� ������� <paramref name="action"/>, ������� �����
        /// �������� ��� ���������� �������.
        /// </summary>
        /// <param name="action">�������.</param>
        public InlineCommand(Func<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Action = action;
        }

        /// <summary>
        /// �������� �������, ������� ����� �������� �� �����
        /// ���������� �������.
        /// </summary>
        public Func<T> Action { get; private set; } 

        protected override T Execute()
        {
            return Action();
        }
    }
}