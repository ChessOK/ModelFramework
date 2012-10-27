﻿using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Internals
{
    public class ValidateAttributesAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Model.Get<AttributesValidator>();
        }
    }
}
