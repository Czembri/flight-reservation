using Moq;
using ReservationAPI.Model;
using ReservationAPI.Repository.Interfaces;

namespace ReservationAPI.Tests.UnitTests
{
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _mockRepo;

        public ReservationServiceTests()
        {
            _mockRepo = new Mock<IReservationRepository>();

            // Mock danych
            var mockReservations = new List<Reservation>
        {
            new Reservation()
            {
                Id = Guid.NewGuid(),
                PassengerName = "John Doe",
                FlightNumber = "AB123",
                DepartureTime = DateTime.Parse("2025-02-20T09:00:00Z"),
                ArrivalTime = DateTime.Parse("2025-02-20T12:00:00Z"),
                Class = "Business"
            },
            new Reservation()
            {
                Id = Guid.NewGuid(),
                PassengerName = "Jane Smith",
                FlightNumber = "CD456",
                DepartureTime = DateTime.Parse("2025-03-15T14:00:00Z"),
                ArrivalTime = DateTime.Parse("2025-03-15T17:00:00Z"),
                Class = "Economy"
            }
        };

            _mockRepo.Setup(repo => repo.GetReservations()).Returns(mockReservations);
            _mockRepo.Setup(repo => repo.GetReservation(It.IsAny<Guid>())).Returns((Guid id) => mockReservations.Find(r => r.Id == id));
            _mockRepo.Setup(repo => repo.AddReservation(It.IsAny<Reservation>())).Callback((Reservation res) => mockReservations.Add(res));
            _mockRepo.Setup(repo => repo.DeleteReservation(It.IsAny<Guid>())).Callback((Guid id) => mockReservations.RemoveAll(r => r.Id == id));
        }

        [Fact]
        public void GetReservations_ReturnsList()
        {
            var result = _mockRepo.Object.GetReservations();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetReservation_ValidId_ReturnsReservation()
        {
            var testId = _mockRepo.Object.GetReservations()[0].Id;
            var result = _mockRepo.Object.GetReservation(testId);

            Assert.NotNull(result);
            Assert.Equal("John Doe", result.PassengerName);
        }

        [Fact]
        public void GetReservation_InvalidId_ReturnsNull()
        {
            var result = _mockRepo.Object.GetReservation(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact]
        public void AddReservation_AddsSuccessfully()
        {
            var newReservation = new Reservation()
            {
                PassengerName = "Alice Johnson",
                FlightNumber = "CD456",
                DepartureTime = DateTime.Parse("2025-04-10T10:00:00Z"),
                ArrivalTime = DateTime.Parse("2025-04-10T13:00:00Z"),
                Class = "First"
            };
            _mockRepo.Object.AddReservation(newReservation);

            var result = _mockRepo.Object.GetReservations();
            Assert.Equal(3, result.Count);
            Assert.Contains(result, r => r.PassengerName == "Alice Johnson");
        }

        [Fact]
        public void DeleteReservation_RemovesSuccessfully()
        {
            var testId = _mockRepo.Object.GetReservations()[0].Id;
            _mockRepo.Object.DeleteReservation(testId);

            var result = _mockRepo.Object.GetReservations();
            Assert.Equal(1, result.Count);
            Assert.DoesNotContain(result, r => r.Id == testId);
        }
    }
}
