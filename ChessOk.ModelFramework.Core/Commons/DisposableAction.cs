using System;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// ��������� ��������� � ������������ �������
    /// �� ����� ������ ������ <see cref="Dispose"/>.
    /// </summary>
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// ���������������� ��������� ������, ��������� ������� <paramref name="action"/>,
        /// ������� ����� ������ � ������ <see cref="Dispose"/>.
        /// </summary>
        /// <param name="action"></param>
        public DisposableAction(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// ������� �������, ��������� ���
        /// �������� ����������.
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }
}