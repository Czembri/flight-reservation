using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Enums;
using ReservationAPI.Model;
using ReservationAPI.Models;
using ReservationAPI.Repository.Interfaces;
using ReservationAPI.Utils;

namespace ReservationAPI.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _repository;
        private readonly IMapper mapper;

        public ReservationsController(IReservationRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetReservations());

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var reservation = _repository.GetReservation(id);
            return reservation != null ? Ok(reservation) : NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReservationRequest reservation)
        {
            if (reservation != null)
            {
                try
                {
                    if (!Enum.IsDefined(typeof(TicketClass), reservation.Class))
                    {
                        return BadRequest(new { message = $"Invalid class type: {reservation.Class}. Allowed values: Economy=0, Business=1, FirstClass=2" });
                    }

                    var departureTime = DateTimeParser.Parse2(reservation.DepartureTime);
                    var arrivalTime = DateTimeParser.Parse2(reservation.ArrivalTime);

                    if (arrivalTime < departureTime)
                    {
                        return BadRequest(new
                        {
                            message = "Arrival time cannot be before departure time"
                        });
                    }

                    var newReservation = mapper.Map<Reservation>(reservation);
                    newReservation.Id = Guid.NewGuid();
                    _repository.AddReservation(newReservation);
                    return Created($"/reservations/{newReservation.Id}", newReservation);

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Reservation not provided");
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] ReservationRequest updatedReservation)
        {
            if (updatedReservation == null)
                return BadRequest(new { message = "Invalid reservation data" });

            try
            {
                if (!Enum.IsDefined(typeof(TicketClass), updatedReservation.Class))
                {
                    return BadRequest(new { message = $"Invalid class type: {updatedReservation.Class}. Allowed values: Economy=0, Business=1, FirstClass=2" });
                }

                var departureTime = DateTimeParser.Parse(updatedReservation.DepartureTime);
                var arrivalTime = DateTimeParser.Parse(updatedReservation.ArrivalTime);

                if (arrivalTime < departureTime)
                {
                    return BadRequest(new
                    {
                        message = "Arrival time cannot be before departure time"
                    });
                }

                var existing = _repository.SaveReservation(id, updatedReservation);
                return Ok(existing);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existing = _repository.GetReservation(id);
            if (existing == null) return NotFound();

            _repository.DeleteReservation(id);
            return NoContent();
        }
    }
}
