namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Экземпляр класса, реализующего данный интерфейс, получает
    /// ссылку на экземпляр <see cref="IModelContext"/> во время собственного
    /// резолвинга.
    /// </summary>
    public interface IHaveModelContext
    {
        /// <summary>
        /// Получает экземпляр <see cref="IModelContext"/>, с помощью
        /// которого был получен экземпляр класса, реализующего данный интерфейс.
        /// </summary>
        IModelContext Context { get; set; }
    }
}