using AutoMapper;
using ReservationAPI.Model;
using ReservationAPI.Models;
using ReservationAPI.Repository.Interfaces;
using System.Text.Json;

namespace ReservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private List<Reservation> _reservations;
        private readonly IMapper _mapper;

        public ReservationRepository(IMapper mapper)
        {
            _reservations = LoadReservations();
            _mapper = mapper;
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

        public Reservation SaveReservation(Guid id, ReservationRequest updated)
        {
            var existingReservation = GetReservation(id);
            if (existingReservation == null)
                throw new KeyNotFoundException();

            if (!DateTime.TryParse(updated.ArrivalTime, out var arrival) ||
                !DateTime.TryParse(updated.DepartureTime, out var departure))
            {
                throw new ArgumentException("Invalid date format");
            }

            if (arrival <= departure)
            {
                throw new ArgumentException("Arrival time cannot be before departure time");
            }

            _reservations.RemoveAll(r => r.Id == id);
            
            _mapper.Map(updated, existingReservation);

            _reservations.Add(existingReservation);

            SaveReservations();
            return existingReservation;
        }
    }
}
