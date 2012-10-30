using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// ������������� ��������� ��� ��������� ������� ���� <typeparamref name="TObject"/>,
    /// ��������� ����������� Fluent-���������.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public interface IEnsureSyntax<TObject>
    {
        /// <summary>
        /// �������� ��������������� � ����������� ���������
        /// <see cref="IValidationContext"/>.
        /// </summary>
        IValidationContext ValidationContext { get; }

        /// <summary>
        /// ���������� ��������� ��������� ������� � ������� ����������, ���������� � ���������
        /// <paramref name="validator"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// ��� ���� ��������� <paramref name="validator"/> ������������� � �������
        /// <see cref="ValidationContext"/> � �������� ��� ����� <see cref="IValidator.Validate"/>.
        /// </remarks>
        /// 
        /// <param name="validator">��������� ����������.</param>
        /// <returns><see cref="IEnsureSyntax{TObject}"/>, ��������������� � �������� ��������.</returns>
        /// <exception cref="ArgumentNullException">�������� <paramref name="validator"/> �� �����.</exception>
        IEnsureSyntax<TObject> IsValid(IValidator validator);

        /// <summary>
        /// ���������� ��������� ���������� ����� ������-��������� <paramref name="propertyExpression"/>
        /// ��������. ���������� �� ��������� ����������� � ��������� <paramref name="validation"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// ��� ���� ��������� ����� ��������� <see cref="EnsureEngine{TProperty}"/>, ���������������
        /// �� ��������� ���������� ��������.
        /// 
        /// ���� �������� ������ ��� ����� <c>null</c>, �� ������� <paramref name="validation"/>
        /// �� ����������.
        /// 
        /// ��� ���� ������ ������, ��������������� � �������� <paramref name="validation"/> �����������
        /// ������� � ������ ��������, ��������� <see cref="ValidationContextExtensions.PrefixErrorKeysWithName"/>.
        /// </remarks>
        /// 
        /// <typeparam name="TProperty">��� ��������.</typeparam>
        /// <param name="propertyExpression">������-��������� ��������.</param>
        /// <param name="validation">������� � ������������ �� ��������� ��������.</param>
        /// <returns><see cref="IEnsureSyntax{TObject}"/>, ��������������� � �������� �������� (�� ���������).</returns>
        /// <exception cref="ArgumentNullException">�������� <paramref name="propertyExpression"/> �� �����.</exception>
        /// <exception cref="ArgumentNullException">������� <paramref name="validation"/> �� �����.</exception>
        IEnsureSyntax<TObject> ItsProperty<TProperty>(
            Expression<Func<TObject, TProperty>> propertyExpression,
            Action<IEnsureSyntax<TProperty>> validation);

        /// <summary>
        /// ���������� ��������� �������� � ������ <paramref name="propertyName"/>
        /// ���������� �� ��������� ����������� � ��������� <paramref name="validation"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// ��� ���� ��������� ����� ��������� <see cref="EnsureEngine{TProperty}"/>, ���������������
        /// �� ��������� ���������� ��������.
        /// 
        /// ���� �������� ������ ��� ����� <c>null</c>, �� ������� <paramref name="validation"/>
        /// �� ����������.
        /// 
        /// ��� ���� ������ ������, ��������������� � �������� <paramref name="validation"/> �����������
        /// ������� � ������ ��������, ��������� <see cref="ValidationContextExtensions.PrefixErrorKeysWithName"/>.
        /// </remarks>
        /// 
        /// <typeparam name="TProperty">��� ��������.</typeparam>
        /// <param name="propertyName">��� ������������� ��������.</param>
        /// <param name="validation">������� � ������������ �� ��������� ��������.</param>
        /// <returns><see cref="IEnsureSyntax{TObject}"/>, ��������������� � �������� �������� (�� ���������).</returns>
        /// <exception cref="InvalidOperationException">�������� � ������ <paramref name="propertyName"/> �� �������.</exception>
        /// <exception cref="ArgumentNullException">������� <paramref name="validation"/> �� �����.</exception>
        IEnsureSyntax<TObject> ItsProperty<TProperty>(
            string propertyName,
            Action<IEnsureSyntax<TProperty>> validation);
    }
}