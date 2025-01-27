namespace CarRentalMVC.Data
{
    public class Rental
    {
        public int Id { get; set; } // Id wypożyczenia
        public required int CarId { get; set; } // Id samochodu (relacja do tabeli Cars)
        public Car? Car { get; set; } // Samochód, który został wypożyczony

        public required string RenterName { get; set; } // Imię najemcy
        public DateOnly RentalDate { get; set; } // Data wynajmu
        public DateOnly? ReturnDate { get; set; } // Data zwrotu (może być null, jeśli jeszcze nie zwrócony)
    }
}
