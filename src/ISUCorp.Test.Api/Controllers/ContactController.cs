﻿using AutoMapper;
using FluentValidation;
using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain;
using ISUCorp.Test.Api.Domain.ContactModel;
using ISUCorp.Test.Api.Dtos.Contact;
using ISUCorp.Test.Api.Dtos.ContactType;
using ISUCorp.Test.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Controllers
{
    //Controller created for Contact operations
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IRepository _db;
        private readonly IMapper _mapper;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IContactService _contactService;

        //Injecting dependencies
        public ContactController(IRepository db, IMapper mapper, 
            IValidatorFactory validatorFactory, IContactService contactService)
        {
            _db = db;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
            _contactService = contactService;
        }

        //Endpoint for retrieving ContactTypes by name Filter
        [HttpGet("get-contact-type-by-filter")]
        public async Task<ActionResult<List<ContactTypeSearchDto>>> GetContactTypeByFilter([FromQuery] string filter = "")
        {
            var contactTypes = await _db.GetContactTypeByFilter(filter);
            var result = _mapper.Map<List<ContactTypeSearchDto>>(contactTypes);
            return Ok(result);
        }

        //Endpoint for retrieving All ContactTypes
        [HttpGet("get-all-contact-type")]
        public async Task<ActionResult<List<ContactTypeSearchDto>>> GetAllContactType()
        {
            var contactTypes = await _db.GetAllContactType();
            var result = _mapper.Map<List<ContactTypeSearchDto>>(contactTypes);
            return Ok(result);
        }

        //Endpoint for retrieving Contacts by Name Filter
        [HttpGet("get-contact-by-filter")]
        public async Task<ActionResult<List<ContactUpdateDto>>> GetContactByFilter([FromQuery] string filter = "")
        {
            var contacts = await _db.GetContactByFilter(filter.ReplaceNullByEmpty());
            var result = _mapper.Map<List<ContactUpdateDto>>(contacts);
            return Ok(result);
        }

        //Endpoint for retrieving a Contact by Id 
        [HttpGet("get-contact-for-edit/{contactId}")]
        public async Task<ActionResult<List<ContactUpdateDto>>> GetContactById([FromRoute] int contactId)
        {
            var contact = await _db.GetContactById(contactId);
            if(contact == null)
                return NotFound();

            var result = _mapper.Map<ContactUpdateDto>(contact);
            return Ok(result);
        }

        //Endpoint for creating Contact
        [HttpPost]
        public async Task<ActionResult> SaveContact([FromBody] ContactSaveDto contact)
        {
            var newContact = new Contact(contact.Name.ReplaceNullByEmpty().DeleteWhiteSpaces(),
                                         contact.BirthDate != null ? contact.BirthDate.Value : DateTime.MinValue,
                                         contact.PhoneNumber.ReplaceNullByEmpty().DeleteWhiteSpaces(),
                                         contact.ContactTypeId);
            //Validating Contact data
            ModelState.AddValidationResult(await _validatorFactory.GetValidator<Contact>().ValidateAsync(newContact));

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            await _contactService.SaveContact(newContact);

            ModelState.AddValidationResult(_contactService.ValidationResult());

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            return Created(string.Empty, newContact.ContactId);
        }

        //Method for Updating Contact by Id
        [HttpPut("{contactId}")]
        public async Task<ActionResult> UpdateContact([FromRoute] int contactId, [FromBody] ContactUpdateDto contact)
        {
            var lastContact = await _db.GetContactById(contactId);
            if (lastContact == null) return NotFound();

            var contactMapped = new Contact(contact.Name.ReplaceNullByEmpty().DeleteWhiteSpaces(),
                                         contact.BirthDate != null ? contact.BirthDate.Value : DateTime.MinValue,
                                         contact.PhoneNumber.ReplaceNullByEmpty().DeleteWhiteSpaces(),
                                         contact.ContactTypeId);

            //Validating Contact data
            ModelState.AddValidationResult(await _validatorFactory.GetValidator<Contact>().ValidateAsync(contactMapped));

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            await _contactService.UpdateContact(lastContact, contactMapped);

            //Validate Service execution
            ModelState.AddValidationResult(_contactService.ValidationResult());

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            return Created(string.Empty, lastContact.ContactId);
        }

        //Endpoint for Deleting Contact and dependencis by Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePerson([FromRoute] int id)
        {
            var lastContact = await _db.GetContactById(id);
            if (lastContact == null) return NotFound();

            await _contactService.RemoveContact(lastContact);

            return Ok();
        }

        //Endpoint for retrieving Contacts paged
        [HttpGet("get-contact-pager")]
        public async Task<ActionResult<PagerBase<ContactResult>>> GetContactPager([FromQuery] int pageNumber)
        {
            var contacts = await _db.GetContactPager(pageNumber);

            return Ok(contacts);
        }
    }
}
