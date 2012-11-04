using System;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Служит базовым классом для команд, не имеющих результата своего выполнения. 
    /// См. <see cref="CommandBase"/> для более подробного описания команд.
    /// </summary>
    /// 
    /// <example>
    /// <code lang="cs"><![CDATA[
    /// 
    /// public class RegisterUserCommand : Command
    /// {
    ///     [Required]
    ///     public string Login { get; set; }
    /// 
    ///     public string FirstName { get; set; }
    ///     public string LastName { get; set; }
    /// 
    ///     protected override void Execute()
    ///     {         
    ///         var user = new User { Login = Login, FirstName = FirstName, LastName = LastName };
    /// 
    ///         var dbContext = Context.Get<DbContext>();
    ///         dbContext.Set<User>().Insert(user);
    ///         dbContext.SaveChanges();
    /// 
    ///         Context.Get<EmailService>().Send(Templates.UserRegistered, user);
    ///     }
    /// }
    /// 
    /// // Somewhere in the code
    /// if (Bus.TrySend<RegisterUserCommand>(viewModel))
    /// {
    ///     FlashMessage = "User registered.";
    /// }
    /// 
    /// ]]></code>
    /// </example>
    [Serializable]
    public abstract class Command : CommandBase
    {
        public sealed override void Invoke()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}