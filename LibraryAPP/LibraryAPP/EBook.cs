public class EBook : Book // EBook perii Book-luokan
{
    public double FileSizeMB { get; set; } // E-kirjan tiedostokoko megatavuina

    public EBook(string title, string author, double size) // Konstruktori e-kirjalle
        : base(title, author) // Kutsuu Book-luokan konstruktoria ja asettaa title + author
    {
        FileSizeMB = size; // Tallennetaan tiedostokoko
        Availability = "Available (E-Book)"; // E-kirjan oletussaatavuus
    }

    public override string GetDescription() // Ylikirjoitetaan Book-luokan GetDescription-metodi
    {
        return $"{Title} - {Author}"; // Palautetaan e-kirjan nimi ja kirjoittaja
    }
}