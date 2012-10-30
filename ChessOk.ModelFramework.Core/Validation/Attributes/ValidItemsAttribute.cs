using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Указывает, что объект нужно проверить с помощью <see cref="CollectionValidator"/>.
    /// </summary>
    public class ValidItemsAttribute : ValidateAttribute
    {
        /// <summary>
        /// Получить экземпляр типа <see cref="CollectionValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            return ValidationContext.Model.Get<CollectionValidator>();
        }
    }
}
