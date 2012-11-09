using System;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Выполняет указанный в конструкторе делегат
    /// во время вызова метода <see cref="Dispose"/>.
    /// </summary>
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// Инициализировать экземпляр класса, используя делегат <paramref name="action"/>,
        /// который будет вызван в метода <see cref="Dispose"/>.
        /// </summary>
        /// <param name="action"></param>
        public DisposableAction(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// Вызвать делегат, указанный при
        /// создании экземпляра.
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }
}