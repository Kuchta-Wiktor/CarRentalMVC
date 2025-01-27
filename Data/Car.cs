namespace CarRentalMVC.Data
{
    public class Car
    {
        public int Id { get; set; } // Id samochodu
        public string Make { get; set; } // Marka samochodu
        public string Model { get; set; } // Model samochodu
        public int Year { get; set; } // Rok produkcji
        public string LicensePlate { get; set; } // Numer rejestracyjny
        public bool IsAvailable { get; set; } // Status dostępności
    }
}
