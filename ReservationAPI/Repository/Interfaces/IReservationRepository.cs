using ReservationAPI.Model;
using ReservationAPI.Models;

namespace ReservationAPI.Repository.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservations();
        Reservation? GetReservation(Guid id);
        void AddReservation(Reservation reservation);
        void DeleteReservation(Guid id);
        Reservation SaveReservation(Guid id, ReservationRequest updated);
    }
}
