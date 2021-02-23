using AutoMapper;
using FluentValidation;
using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain;
using ISUCorp.Test.Api.Domain.ContactModel;
using ISUCorp.Test.Api.Dtos.Contact;
using ISUCorp.Test.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository _db;
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;
        private readonly IValidatorFactory _validatorFactory;

        public ReservationController(IRepository db, IMapper mapper, 
            IReservationService reservationService, IValidatorFactory validatorFactory)
        {
            _db = db;
            _mapper = mapper;
            _reservationService = reservationService;
            _validatorFactory = validatorFactory;
        }

        [HttpGet("get-reservation-pager")]
        public async Task<ActionResult<PagerBase<ReservationResult>>> GetReservationPager([FromQuery] int sortOption, [FromQuery] int pageNumber)
        {
            var reservations = await _db.GetReservationPager(sortOption, pageNumber);
            
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<ActionResult> SaveReservation([FromBody] ReservationSaveDto reservation)
        {
            var newReservation = _mapper.Map<Reservation>(reservation);
            newReservation.SetContact(new Contact(reservation.Contact.ContactId, reservation.Contact.Name.ReplaceNullByEmpty().DeleteWhiteSpaces(),
                                         reservation.Contact.BirthDate != null ? reservation.Contact.BirthDate.Value : DateTime.MinValue,
                                         reservation.Contact.PhoneNumber.ReplaceNullByEmpty().DeleteWhiteSpaces(),
                                         reservation.Contact.ContactTypeId));

            ModelState.AddValidationResult(await _validatorFactory.GetValidator<Contact>().ValidateAsync(newReservation.Contact));

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            await _reservationService.SaveReservation(newReservation);

            ModelState.AddValidationResult(_reservationService.ValidationResult());

            if (!ModelState.IsValid)
                return Conflict(ModelState.GetValidationProblemDetails());

            return Created(string.Empty, newReservation.ReservationId);
        }
    }
}
