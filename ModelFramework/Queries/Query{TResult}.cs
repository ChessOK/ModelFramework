using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Queries.Internals;

namespace ChessOk.ModelFramework.Queries
{
    /// <summary>
    /// Предоставляет базовый класс для реализации операций, инкапсулирующих 
    /// запросы к данным приложения без раскрытия их внутренней реализации (будь то
    /// EntityFramework, NHibernate, ADO.NET, либо чтение из файла), без изменения
    /// состояния приложения, либо его окружения.
    /// </summary>
    /// 
    /// <remarks>
    /// Запросы, используемые при реализации слоя представления, не должны содержать
    /// в классе <typeparamref name="TResult"/> (и всех его свойствах) классы, относящиеся
    /// к деталям его реализации (например, сущности EntityFramework). Результатом
    /// такого запроса должны быть DTO (или их коллекция).
    /// 
    /// Запросы не должны изменять состояние приложения, либо его окружения. Для реализации
    /// операций, изменяющих такое состояние, пользуйтесь командами (<see cref="Command"/>, 
    /// <see cref="Command{TResult}"/>).
    /// </remarks>
    /// 
    /// <example>
    /// Предположим, что у нас есть очень сложный запрос, но мы пока не хотим
    /// заботиться о его производительности и выбрали EntityFramework для его написания,
    /// поскольку он предоставляет упрощенный синтаксис для операций подобного рода.
    /// 
    /// <code lang="cs"><![CDATA[
    /// 
    /// public class ComplexQuery : Query<IEnumerable<UserDto>
    /// {
    ///     public int[] UserIds { get; set; }
    ///     public string FirstName { get; set; }
    /// 
    ///     protected override IEnumerable<UserDto> Execute()
    ///     {
    ///         var dbContext = Context.Get<DbContext>();
    ///         // Perform some calculations using EntityFramework
    ///         return users;
    ///     }
    /// }
    /// 
    /// // Somewhere in the code
    /// var usersQuery = Context.Query<ComplexQuery>();
    /// foreach (var user in usersQuery.Result)
    /// {
    ///     // Send email, for example.
    /// }
    /// 
    /// ]]></code>
    /// 
    /// С течением времени, количество данных в приложении росло, и мы решили
    /// избавиться от накладных расходов по материализации объектов EntityFramework,
    /// переписав запрос, используя ADO.NET.
    /// 
    /// <code lang="cs"><![CDATA[
    /// 
    /// public class ComplexQuery : Query<IEnumerable<UserDto>>
    /// {
    ///     public int[] UserIds { get; set; }
    ///     public string FirstName { get; set; }
    /// 
    ///     protected override IEnumerable<UserDto> Execute()
    ///     {
    ///         using (var connection = Context.Get<SqlConnection>())
    ///         {
    ///             // Perform query using raw SQL and materialize objects by hands.
    ///             return users;
    ///         }
    ///     }
    /// } 
    /// ]]></code>
    /// 
    /// В итоге нам пришлось изменить только внутреннюю реализацию самого запроса,
    /// оставив код остального приложения нетронутым.
    /// 
    /// </example>
    /// 
    /// <typeparam name="TResult">Тип результата.</typeparam>
    public abstract class Query<TResult> : Query
    {
        /// <summary>
        /// Результат выполнения запроса.
        /// </summary>
        public TResult Result { get; private set; }

        internal override void Invoke()
        {
            Result = Execute();
        }

        /// <summary>
        /// Возвращает результат запроса, используя
        /// реализацию, приведенную в классе-наследнике.
        /// </summary>
        /// <returns>Результат запроса.</returns>
        protected abstract TResult Execute();
    }
}
