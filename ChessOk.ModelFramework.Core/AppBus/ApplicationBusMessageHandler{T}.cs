using System;

namespace ChessOk.ModelFramework.Messages
{
    /// <summary>
    /// Служит базовым классом для реализации обработчиков сообщений
    /// типа <typeparamref name="T"/>.
    /// </summary>
    /// 
    /// <remarks>
    /// В отличие от <see cref="ApplicationBusMessageHandler"/>, он обрабатывает
    /// только сообщения типа <typeparamref name="T"/>, игнорируя все другие типы.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип обрабатываемых сообщений.</typeparam>
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