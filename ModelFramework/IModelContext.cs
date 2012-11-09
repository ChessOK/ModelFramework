using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// ������ ��������� ��� <see cref="ILifetimeScope"/>,
    /// ������������ ���������� ����������� ��� ������� ������
    /// ����������. ��. <see cref="ModelContext"/>.
    /// </summary>
    public interface IModelContext : IDisposable
    {
        /// <summary>
        /// ���������� ��������� �������, �������������������
        /// � ���������� ��� ����� <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">��� ����������� �������.</typeparam>
        /// <returns>��������� �������.</returns>
        T Get<T>();

        /// <summary>
        /// ���������� ��������� �������, �������������������
        /// � ���������� ��� ����� <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">��� ����������� �������.</param>
        /// <returns>��������� �������.</returns>
        object Get(Type serviceType);

        /// <summary>
        /// ���������� ��������� ����������� ��������, ������������������
        /// � ���������� ��� ����� <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">��� ��������.</typeparam>
        /// <returns>��������� ����������� ��������.</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// ���������� ��������� ����������� ��������, ������������������
        /// � ���������� ��� ����� <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">��� ��������.</param>
        /// <returns>��������� ����������� ��������.</returns>
        IEnumerable<object> GetAll(Type serviceType);

        /// <summary>
        /// ������� �������� ��������� <see cref="IModelContext"/>, � ������� ����� ��������
        /// ��� ������������������ � ������� ���������� ������ ������,
        /// ������������ ����������� ��� ����������� �����.
        /// <para>� �������� ���������� ����� ������ �������� <see cref="ILifetimeScope"/>
        /// � ����� <paramref name="tag"/> � <paramref name="registrations"/>.</para>
        /// </summary>
        /// <param name="tag">��� ���������� <see cref="ILifetimeScope"/>.</param>
        /// <param name="registrations">�������� �� ����������� �������������� �������.</param>
        /// <returns></returns>
        IModelContext CreateChildContext(object tag, Action<ContainerBuilder> registrations);

        /// <summary>
        /// �������� <see cref="LifetimeScope"/>, ��������������� � ������ ����������� <see cref="IModelContext"/>.
        /// </summary>
        ILifetimeScope LifetimeScope { get; }
    }
}