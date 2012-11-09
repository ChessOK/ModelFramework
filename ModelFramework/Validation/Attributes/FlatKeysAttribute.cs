using System;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Имя свойства, помеченное данным атрибутом, не будет включено
    /// в ключи валидационных ошибок.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FlatErrorKeysAttribute : Attribute
    {
    }
}
