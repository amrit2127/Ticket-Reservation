using Microsoft.EntityFrameworkCore;
using NationalPark_WebApp_116.Models;

namespace NationalPark_WebApp_116.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<BookingTicket> BookingTickets { get; set; }

    }
}
