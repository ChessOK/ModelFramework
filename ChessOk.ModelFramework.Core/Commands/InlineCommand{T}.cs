using System;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Предоставляет возможность превращения любого делегата
    /// в команду, имеющую результат выполнения. При её выполнении 
    /// будет вызван переданный в конструкторе делегат. В качестве
    /// результата команды используется результат выполнения делегата.
    /// </summary>
    public class InlineCommand<T> : Command<T>
    {
        /// <summary>
        /// Инициализировать экземпляр класса <see cref="InlineCommand"/>,
        /// используя делегат <paramref name="action"/>, который будет
        /// выполнен при выполнении команды.
        /// </summary>
        /// <param name="action">Делегат.</param>
        public InlineCommand(Func<T> action)
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
        public Func<T> Action { get; private set; } 

        protected override T Execute()
        {
            return Action();
        }
    }
}