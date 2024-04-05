using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalParkAPI_116.Models.DTOs
{
    public class BookingDTO
    {
        public BookingDTO()
        {
            AdultCount = 0;
            Age = 1;
            ChildrenTicketPrice = 250;
            AdultTicketPrice = 450;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public int ChildrenCount { get; set; }
        public int ChildrenTicketPrice { get; set; }
        public int AdultCount { get; set; }
        public int AdultTicketPrice { get; set; }
        public string BookingStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public int Amount { get; set; }
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }
    }
}

