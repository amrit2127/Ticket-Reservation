using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Repository.IRepository;

namespace NationalPark_WebApp_116.Repository
{
    public class BookingRepository:Repository<BookingTicket>,IBookingRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BookingRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
