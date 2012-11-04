namespace ChessOk.ModelFramework.Queries.Internals
{
    /// <summary>
    /// ������������� ������� �����-������ ��� ���������� �������, ��������������� 
    /// ������� � ������ ���������� ��� ��������� �� ���������� ����������.
    /// <para>����� �������� "�������������", � �������� �������� ������ ��� 
    /// �������� ����������� <see cref="Query{T}"/>.</para>
    /// </summary>
    public abstract class Query
    {
        /// <summary>
        /// �������� ������� ��������� <see cref="IModelContext"/> 
        /// ��� ���������� �������� ������.
        /// </summary>
        protected IModelContext Context { get; private set; }

        internal abstract void Invoke();

        internal void Bind(IModelContext model)
        {
            Context = model;
        }
    }
}