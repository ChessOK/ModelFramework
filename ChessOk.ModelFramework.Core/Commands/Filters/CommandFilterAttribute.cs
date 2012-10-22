using System;

namespace ChessOk.ModelFramework.Commands.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class CommandFilterAttribute : Attribute
    {
        public int Order { get; set; }

        public abstract void OnInvoke(CommandFilterContext filterContext, Action commandAction);
    }
}
