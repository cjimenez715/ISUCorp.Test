using AutoMapper;
using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository _db;
        private readonly IMapper _mapper;

        public ReservationController(IRepository db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("get-reservation-by-filter")]
        public async Task<ActionResult<PagerBase<ReservationResult>>> GetByFilter([FromQuery] int sortOption, [FromQuery] int pageNumber)
        {
            var reservations = await _db.GetReservationList(sortOption, pageNumber);
            //var result = _mapper.Map<List<ReservationSearchDto>>(reservations);
            return Ok(reservations);
        }
    }
}
