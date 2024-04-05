using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_116.Models.DTOs;
using NationalParkAPI_116.Models;
using NationalParkAPI_116.Repository;
using NationalParkAPI_116.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using NationalParkAPI_116.Migrations;

namespace NationalParkAPI_116.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBookings()
        {
            var bookingList = _bookingRepository.GetBookingTickets().ToList().Select(_mapper.Map<BookingTicket, BookingDTO>);
            return Ok(_bookingRepository);
        }
        [HttpGet("{bookingId:int}", Name = "GetBooking")]
        public IActionResult GetBooking(int bookingId)
        {
            var booking = _bookingRepository.GetBookingTicket(bookingId);
            if (booking == null) return NotFound();
            var bookingDTOs = _mapper.Map<BookingDTO>(booking);
            return Ok(bookingDTOs);
        }
        [HttpPost]
        public IActionResult CreateBooking([FromBody] BookingDTO bookingDTOs)
        {
            if (bookingDTOs == null) return BadRequest(ModelState);
            if (_bookingRepository.BookingTicketExists(bookingDTOs.Id))
            {
                ModelState.AddModelError("", "BookinginDB!!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest();
            var booking=_mapper.Map<BookingDTO, BookingTicket>(bookingDTOs);
          //  var booking = _mapper.Map<BookingDTO, BookingTicket>(bookingDTOs);
            if (!_bookingRepository.CreateBooking(booking))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Save Data:{booking.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();            
        }
    }
}
