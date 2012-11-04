using System;

using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Queries.Internals;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Служит базовым классом для реализации операций, инкапсулирующих детали
    /// реализации (EntityFramework, изменение файла и пр.) и изменяющих состояние
    /// приложения, либо его окружения.
    /// 
    /// <para>Является служебным классом. Для реализации команд пользуйтесь
    /// классами <see cref="Command"/> и <see cref="Command{TResult}"/>.</para>
    /// </summary>
    /// 
    /// <remarks>
    /// Команда является самодостаточной операцией и должна быть реализована так,
    /// чтобы после её выполнения не требовалось никаких дополнительных операций
    /// (фиксации транзакции, вызова метода DbContext.SaveChanges, закрытия файла и пр.).
    /// 
    /// Если операция не изменяет состояние приложения, то воспользуйтесь
    /// запросами <see cref="Query"/>.
    /// </remarks>
    [Serializable]
    public abstract class CommandBase : IApplicationBusMessage
    {
        /// <summary>
        /// Возникает сразу после выполнения команды и всех её фильтров.
        /// </summary>
        public event Action Invoked;

        /// <summary>
        /// Выполняет команду, используя инструкции, реализованные в классах-наследниках.
        /// </summary>
        public abstract void Invoke();

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        /// <summary>
        /// Возвращает имя сообщения <see cref="IApplicationBusMessage"/>,
        /// присвоенного всем командам <see cref="CommandBase"/>.
        /// </summary>
        /// <returns></returns>
        public static string GetMessageName()
        {
            return typeof(CommandBase).Name;
        }

        /// <summary>
        /// Текущий экземпляр шины приложения.
        /// </summary>
        protected IApplicationBus Bus { get; private set; }

        /// <summary>
        /// Текущий экземпляр <see cref="IModelContext"/>.
        /// </summary>
        protected IModelContext Context { get { return Bus.Context; } }

        /// <summary>
        /// Текущий экземпляр <see cref="IValidationContext"/>.
        /// </summary>
        protected IValidationContext Validation { get { return Bus.ValidationContext; } }

        internal void RaiseInvoked()
        {
            if (Invoked != null)
            {
                Invoked();
            }
        }

        internal void Bind(IApplicationBus bus)
        {
            if (Bus != null)
            {
                throw new InvalidOperationException("Command is already bound to a bus");
            }

            if (bus == null)
            {
                throw new ArgumentNullException("bus");
            }

            Bus = bus;
        }
    }
}