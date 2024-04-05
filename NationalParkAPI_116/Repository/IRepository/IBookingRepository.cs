using NationalParkAPI_116.Models;

namespace NationalParkAPI_116.Repository.IRepository
{
    public interface IBookingRepository
    {
        ICollection<BookingTicket> GetBookingTickets();
        BookingTicket GetBookingTicket(int bookingId);
        bool BookingTicketExists(int bookingId);
        //bool BookingTicketExists(string bookingName);
        bool CreateBooking(BookingTicket bookingTicket);
        //bool UpdateBookingTicket(BookingTicket bookingTicket);
        //bool DeleteBookingTicket(BookingTicket bookingTicket);
        bool Save();
    }
}
