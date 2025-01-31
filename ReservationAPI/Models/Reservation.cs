namespace ReservationAPI.Model
{
    public class Reservation()
    {
        public Guid Id { get; set; }
        public string PassengerName { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Class { get; set; }
    }

}
