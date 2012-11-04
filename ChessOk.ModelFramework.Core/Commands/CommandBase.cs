using System;

using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Queries.Internals;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// ������ ������� ������� ��� ���������� ��������, ��������������� ������
    /// ���������� (EntityFramework, ��������� ����� � ��.) � ���������� ���������
    /// ����������, ���� ��� ���������.
    /// 
    /// <para>�������� ��������� �������. ��� ���������� ������ �����������
    /// �������� <see cref="Command"/> � <see cref="Command{TResult}"/>.</para>
    /// </summary>
    /// 
    /// <remarks>
    /// ������� �������� ��������������� ��������� � ������ ���� ����������� ���,
    /// ����� ����� � ���������� �� ����������� ������� �������������� ��������
    /// (�������� ����������, ������ ������ DbContext.SaveChanges, �������� ����� � ��.).
    /// 
    /// ���� �������� �� �������� ��������� ����������, �� ��������������
    /// ��������� <see cref="Query"/>.
    /// </remarks>
    [Serializable]
    public abstract class CommandBase : IApplicationBusMessage
    {
        /// <summary>
        /// ��������� ����� ����� ���������� ������� � ���� � ��������.
        /// </summary>
        public event Action Invoked;

        /// <summary>
        /// ��������� �������, ��������� ����������, ������������� � �������-�����������.
        /// </summary>
        public abstract void Invoke();

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        /// <summary>
        /// ���������� ��� ��������� <see cref="IApplicationBusMessage"/>,
        /// ������������ ���� �������� <see cref="CommandBase"/>.
        /// </summary>
        /// <returns></returns>
        public static string GetMessageName()
        {
            return typeof(CommandBase).Name;
        }

        /// <summary>
        /// ������� ��������� ���� ����������.
        /// </summary>
        protected IApplicationBus Bus { get; private set; }

        /// <summary>
        /// ������� ��������� <see cref="IModelContext"/>.
        /// </summary>
        protected IModelContext Context { get { return Bus.Context; } }

        /// <summary>
        /// ������� ��������� <see cref="IValidationContext"/>.
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