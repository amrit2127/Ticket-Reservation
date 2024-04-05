namespace NationalPark_WebApp_116
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:7267/";
        public static string NationalParkAPIPath = APIBaseUrl + "api/NationalPark";
        public static string TrailAPIPath = APIBaseUrl + "api/Trail";
        public static string BookingAPIPath = "https://localhost:7267/api/Booking";

        //Booking Status
        public const string BookingStatusPending = "Pending";
        public const string BookingStatusApproved = "Approved";
        public const string BookingStatusCancelled = "Cancelled";

        //Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";

    }
}
