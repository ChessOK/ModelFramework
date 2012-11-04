using System;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.AsyncCommands
{
    /// <summary>
    /// Предоставляет базовые возможности асинхронного выполнения 
    /// команд <see cref="CommandBase"/>
    /// <para>См. описание <see cref="AsyncCommandDispatcher"/> для более
    /// подробных сведений.</para> 
    /// </summary>
    public class AsyncCommand : IApplicationBusMessage
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AsyncCommand"/> и
        /// задает команду <paramref name="command"/>, которая будет провалидирована и 
        /// выполнена асинхронно.
        /// </summary>
        /// <param name="command"></param>
        public AsyncCommand(CommandBase command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            Command = command;
        }

        /// <summary>
        /// Получает команду, которая будет выполнена асинхронно.
        /// </summary>
        [Valid]
        public CommandBase Command { get; private set; }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        /// <summary>
        /// Возвращает имя сообщения <see cref="IApplicationBusMessage"/>,
        /// которое используется классом <see cref="AsyncCommand"/>.
        /// <para>Может быть использовано для написания собственных 
        /// обработчиков асинхронных команд.</para>
        /// </summary>
        /// <returns></returns>
        public static string GetMessageName()
        {
            return typeof(AsyncCommand).Name;
        }
    }
}
