using ChessOk.ModelFramework.AsyncCommands.Workers;
using ChessOk.ModelFramework.Commands;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    /// <summary>
    /// ������������� ��������� ��� ������������ ������ <see cref="CommandBase"/>,
    /// ����������� ����������.
    /// <para>
    /// ���������� ���������� ����� ��������� ������� �� ������� (��.
    /// ������ � ������ <see cref="BackgroundThreadWorker"/>).
    /// </para>
    /// </summary>
    public interface IAsyncCommandHandler
    {
        /// <summary>
        /// ���������� � ��������� ������� <paramref name="asyncCommand"/>.
        /// </summary>
        void Handle(CommandBase asyncCommand);
    }
}