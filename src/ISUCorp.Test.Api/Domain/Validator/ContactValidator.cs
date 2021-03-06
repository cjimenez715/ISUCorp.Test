﻿using FluentValidation;
using ISUCorp.Test.Api.Domain.ContactModel;
using ISUCorp.Test.Api.Domain.Resources.DomainProperties;
using ISUCorp.Test.Api.Domain.Resources.DomainValidation;
using Microsoft.Extensions.Localization;

namespace ISUCorp.Test.Api.Domain.Validator
{
    //Contact Validation properties Rules and message concatenation
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator(IStringLocalizer<DomainPropertiesResource> localizer,
            IStringLocalizer<DomainValidationResource> localizerValidator)
        {
            RuleFor(c => c.Name).NotEmpty().WithName(localizer["Name"]).WithMessage(localizerValidator["IsRequired"]);
            RuleFor(c => c.ContactTypeId).NotNull().GreaterThan(0).WithName(localizer["ContactType"]).WithMessage(localizerValidator["IsRequired"]);
            RuleFor(c => c.BirthDate).NotEmpty().WithName(localizer["BirthDate"]).WithMessage(localizerValidator["IsRequired"]);
        }
    }
}
