﻿using System;
using FluentValidation;
using SlimFormaturas.Domain.Entities;

namespace SlimFormaturas.Domain.Validators {
    public class CourseValidator : AbstractValidator<Course>{
        public CourseValidator() {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x => throw new ArgumentNullException("O objeto não foi encontrado!"));
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(" É necessário informar o nome do Curso ")
                .NotNull()
                .Length(1, 50).WithMessage("O campo deve ter entre 1 e 50 caracteres ");
        }
    }
}
