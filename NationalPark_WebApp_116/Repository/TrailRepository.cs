using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Repository.IRepository;

namespace NationalPark_WebApp_116.Repository
{
    public class TrailRepository:Repository<Trail>,ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
