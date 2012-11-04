namespace ChessOk.ModelFramework.Queries.Internals
{
    /// <summary>
    /// Предоставляет базовый класс-маркер для реализации классов, инкупсулирующих 
    /// запросы к данным приложения без раскрытия их внутренней реализации.
    /// <para>Класс является "синтетическим", в качестве базового класса для 
    /// запросов используйте <see cref="Query{T}"/>.</para>
    /// </summary>
    public abstract class Query
    {
        /// <summary>
        /// Получает текущий экземпляр <see cref="IModelContext"/> 
        /// для разрешения объектов модели.
        /// </summary>
        protected IModelContext Context { get; private set; }

        internal abstract void Invoke();

        internal void Bind(IModelContext model)
        {
            Context = model;
        }
    }
}