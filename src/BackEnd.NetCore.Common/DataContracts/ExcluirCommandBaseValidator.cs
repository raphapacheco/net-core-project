﻿using BackEnd.NetCore.Common.Utils;
using FluentValidation;

namespace BackEnd.NetCore.Common.DataContracts
{
    internal class ExcluirCommandBaseValidator : AbstractValidator<ExcluirCommandBase>
    {
        public ExcluirCommandBaseValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.NOT_EMPTY);

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage(ValidationMessage.NOT_NULL);
        }

    }
}
