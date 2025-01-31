using ReservationAPI.Model;

namespace ReservationAPI.Repository.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservations();
        Reservation? GetReservation(Guid id);
        void AddReservation(Reservation reservation);
        void DeleteReservation(Guid id);
    }
}
