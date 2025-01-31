using ReservationAPI.Model;
using ReservationAPI.Repository.Interfaces;
using System.Text.Json;

namespace ReservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _reservations = LoadReservations();
        }

        public List<Reservation> GetReservations() => _reservations;

        public Reservation? GetReservation(Guid id) => _reservations.Find(r => r.Id == id);

        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
            SaveReservations();
        }

        public void DeleteReservation(Guid id)
        {
            _reservations.RemoveAll(r => r.Id == id);
            SaveReservations();
        }

        private static List<Reservation> LoadReservations()
        {
            if (File.Exists("reservations.json"))
            {
                var json = File.ReadAllText("reservations.json");
                return JsonSerializer.Deserialize<List<Reservation>>(json) ?? new List<Reservation>();
            }
            return new List<Reservation>();
        }

        private void SaveReservations()
        {
            var json = JsonSerializer.Serialize(_reservations);
            File.WriteAllText("reservations.json", json);
        }
    }
}
