using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Repository.IRepository;

namespace NationalPark_WebApp_116.Repository
{
    public class NationalParkRepository:Repository<NationalPark>,INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NationalParkRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
