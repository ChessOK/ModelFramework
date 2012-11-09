using System;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Предоставляет возможность превращения любого делегата
    /// в команду, не имеющую результата выполнения. При её выполнении 
    /// будет вызван переданный в конструкторе делегат.
    /// </summary>
    public class InlineCommand : Command
    {
        /// <summary>
        /// Инициализировать экземпляр класса <see cref="InlineCommand"/>,
        /// используя делегат <paramref name="action"/>, который будет
        /// выполнен при выполнении команды.
        /// </summary>
        /// <param name="action">Делегат.</param>
        public InlineCommand(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Action = action;
        }

        /// <summary>
        /// Получает делегат, который будет выполнен во время
        /// выполнения команды.
        /// </summary>
        public Action Action { get; private set; }

        protected override void Execute()
        {
            Action();
        }
    }
}
