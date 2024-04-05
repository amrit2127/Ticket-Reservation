using Microsoft.EntityFrameworkCore;
using NationalParkAPI_116.Models;

namespace NationalParkAPI_116.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
                
        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookingTicket> BookingTickets { get; set; }
    }
}
