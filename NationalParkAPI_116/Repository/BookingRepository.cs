using Microsoft.EntityFrameworkCore;
using NationalParkAPI_116.Data;
using NationalParkAPI_116.Models;
using NationalParkAPI_116.Repository.IRepository;
using System.Diagnostics;

namespace NationalParkAPI_116.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool BookingTicketExists(int bookingId)
        {
            return _context.BookingTickets.Any(b=>b.Id == bookingId);
        }

        public bool CreateBooking(BookingTicket bookingTicket)
        {
            _context.BookingTickets.Add(bookingTicket);
            return Save();
        }

        public BookingTicket GetBookingTicket(int bookingId)
        {
            return _context.BookingTickets.Find(bookingId);
        }

        public ICollection<BookingTicket> GetBookingTickets()
        {
            return _context.BookingTickets.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
