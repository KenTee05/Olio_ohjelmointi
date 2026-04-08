public class EBook : Book // EBook perii Book-luokan
{
    public double FileSizeMB { get; set; } // E-kirjan tiedostokoko megatavuina

    public EBook(string title, string author, double size) // EBook-konstruktori
        : base(title, author) // Kutsutaan Book-luokan konstruktoria
    {
        FileSizeMB = size; // Tallennetaan tiedostokoko
        Availability = "Available (E-Book)"; // E-kirja on aluksi saatavilla
    }

    public override string GetDescription() // Ylikirjoitettu kuvaus e-kirjalle
    {
        return $"{Title} by {Author} (E-Book, {FileSizeMB} MB)";
    }
}