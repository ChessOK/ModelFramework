using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Compatibility
{
    /// <summary>
    /// ���������, ��� ����������� ����� ������� (������, ���� �������),
    /// �� ������ ���� ������ ��������� � ������������ �����.
    /// </summary>
    /// <remarks>
    /// ��� �������� ������������ ��������� <see cref="MinLengthValidator"/>,
    /// ��� ����� ��������� �������� ��. ��� ������������.
    /// </remarks>
    public class MinLengthAttribute : ValidateAttribute
    {
        private readonly int _minimumLength;

        /// <summary>
        /// ������� ��������� �������� <see cref="MinLengthAttribute"/>
        /// �� ������ ��������� <paramref name="minimumLength"/>.
        /// </summary>
        /// <param name="minimumLength">����������� ����� ������, ���� �������.</param>
        public MinLengthAttribute(int minimumLength)
        {
            _minimumLength = minimumLength;
        }

        /// <summary>
        /// �������� ��� ������ ���������, ������������ ��� ������. ����� ���� ������.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// �������� ��������� ���� <see cref="MinLengthValidator"/>.
        /// </summary>
        /// <returns>��������� ����������.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Model.Get<MinLengthValidator>();
            validator.Length = _minimumLength;
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}