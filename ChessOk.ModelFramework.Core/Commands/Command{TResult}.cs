using System;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Служит базовым классом для команд, имеющих результат
    /// своего выполнения. См. <see cref="CommandBase"/> для более подробного
    /// описания команд.
    /// </summary>
    /// 
    /// <remarks>
    /// Команды, используемые при реализации слоя представления, не должны содержать
    /// в классе <typeparamref name="TResult"/> (и всех его свойствах) классы, относящиеся
    /// к деталям его реализации (например, сущности EntityFramework). Результатом
    /// такого запроса должны быть DTO (или их коллекция).
    /// </remarks>
    /// 
    /// <example>
    /// В качестве примера рассмотрим команду регистрации пользователя, использующую
    /// EntityFramework и возвращающую идентификатор созданного пользователя.
    /// 
    /// <code><![CDATA[
    /// 
    /// public class RegisterUserCommand : Command<int>
    /// {
    ///     [Required]
    ///     public string Login { get; set; }
    /// 
    ///     public string FirstName { get; set; }
    ///     public string LastName { get; set; }
    /// 
    ///     protected override int Execute()
    ///     {         
    ///         var user = new User { Login = Login, FirstName = FirstName, LastName = LastName };
    /// 
    ///         var dbContext = Context.Get<DbContext>();
    ///         dbContext.Set<User>().Insert(user);
    ///         dbContext.SaveChanges();
    /// 
    ///         Context.Get<EmailService>().Send(Templates.UserRegistered, user);
    ///         return user.Id;
    ///     }
    /// }
    /// 
    /// // Somewhere in the code
    /// var command = Bus.Create<RegisterUserCommand>(viewModel);
    /// if (Bus.TrySend(command))
    /// {
    ///     return RedirectToAction("Details", "User", new { id = command.Result }
    /// }
    /// 
    /// ]]></code>
    /// </example>
    /// 
    /// <typeparam name="TResult">Тип результата команды.</typeparam>
    [Serializable]
    public abstract class Command<TResult> : CommandBase
    {
        /// <summary>
        /// Результат выполнения команды.
        /// </summary>
        public TResult Result { get; private set; }

        public sealed override void Invoke()
        {
            Result = Execute();
        }

        protected abstract TResult Execute();
    }
}
