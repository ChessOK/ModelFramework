using System;

namespace ChessOk.ModelFramework.Commands.Filters
{
    /// <summary>
    /// Служит базовым классом для реализации фильтров команд.
    /// </summary>
    /// 
    /// <remarks>
    /// Фильтр применяется перед выполнением команды. Во время применения
    /// фильтра, в его метод <see cref="Apply"/> передается делегат выполнения команды
    /// и примененных к ней фильтров (фильтров, имеющих значение свойства <see cref="Order"/>
    /// меньше текущего).
    /// 
    /// Порядок выполнения фильтров команд определяется с помощью свойства <see cref="Order"/>.
    /// Причем фильтр с меньшим значением <see cref="Order"/> будет применен до текущего,
    /// и параметр commandInvocation метода <see cref="Apply"/>, будет содержать инструкции
    /// всех фильтров, примененных к данной команде, с меньшим порядком.
    /// 
    /// Фильтры служат для добавления дополнительной инфраструктурной функциональности к командам. 
    /// Например, возможности повторного выполнения в некоторых случаях. 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class CommandFilterAttribute : Attribute
    {
        /// <summary>
        /// Определяет порядок выполнения фильтра.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Применяет фильтр к выполнению команды.
        /// </summary>
        /// <param name="filterContext">Контекст выполнения команды.</param>
        /// <param name="commandInvocation">Делегат вызова команды и её примененных фильтров.</param>
        public abstract void Apply(CommandFilterContext filterContext, Action commandInvocation);
    }
}
