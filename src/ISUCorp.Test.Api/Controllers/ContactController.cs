﻿using AutoMapper;
using FluentValidation;
using ISUCorp.Test.Api.Domain;
using ISUCorp.Test.Api.Domain.ContactAggregate;
using ISUCorp.Test.Api.Dtos.Contact;
using ISUCorp.Test.Api.Dtos.ContactType;
using ISUCorp.Test.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IRepository _db;
        private readonly IMapper _mapper;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IContactService _contactService;

        public ContactController(IRepository db, IMapper mapper, 
            IValidatorFactory validatorFactory, IContactService contactService)
        {
            _db = db;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
            _contactService = contactService;
        }

        [HttpGet("get-contact-type-by-filter")]
        public async Task<ActionResult<List<ContactTypeSearchDto>>> GetContactTypeByFilter([FromQuery] string filter = "")
        {
            var contactTypes = await _db.GetContactTypeByFilter(filter);
            var result = _mapper.Map<List<ContactTypeSearchDto>>(contactTypes);
            return Ok(result);
        }

        [HttpGet("get-contact-by-filter")]
        public async Task<ActionResult<List<ContactUpdateDto>>> GetcontactByFilter([FromQuery] string filter = "")
        {
            var contacts = await _db.GetContactByFilter(filter);
            var result = _mapper.Map<List<ContactUpdateDto>>(contacts);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SaveContact([FromBody] ContactSaveDto contact)
        {
            var newContact = _mapper.Map<Contact>(contact);
            ModelState.AddValidationResult(await _validatorFactory.GetValidator<Contact>().ValidateAsync(newContact));

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            await _contactService.SaveContact(newContact);
            return Created(string.Empty, newContact.ContactId);
        }

        [HttpPut("{contactId}")]
        public async Task<ActionResult> UpdateContact([FromRoute] int contactId, [FromBody] ContactUpdateDto contact)
        {
            var lastContact = await _db.GetContactById(contactId);
            if (lastContact == null) return NotFound();

            var contactMapped = _mapper.Map<Contact>(contact);
            ModelState.AddValidationResult(await _validatorFactory.GetValidator<Contact>().ValidateAsync(contactMapped));

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            await _contactService.UpdateContact(lastContact, contactMapped);
            return Created(string.Empty, lastContact.ContactId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePerson([FromRoute] int id)
        {
            var lastContact = await _db.GetContactById(id);
            if (lastContact == null) return NotFound();

            await _contactService.RemoveContact(lastContact);

            return Ok();
        }
    }
}
