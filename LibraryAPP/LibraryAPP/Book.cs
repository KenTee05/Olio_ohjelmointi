public class Book : IReadable // Book toteuttaa IReadable-rajapinnan
{
    public string Title { get; set; } // Kirjan nimi
    public string Author { get; set; } // Kirjan kirjoittaja
    public string Availability { get; set; } // Kirjan saatavuus

    public Book() { } // Tyhjä konstruktori

    public Book(string title, string author) // Konstruktori tavalliselle kirjalle
    {
        Title = title; // Tallennetaan nimi
        Author = author; // Tallennetaan kirjoittaja
        Availability = "Available"; // Tavallinen kirja on aluksi saatavilla
    }

    public virtual string GetDescription() // Rajapinnan toteutus, voidaan ylikirjoittaa aliluokassa
    {
        return $"{Title} by {Author}";
    }

    public override string ToString() // Palauttaa kirjan kuvauksen tekstinä
    {
        return GetDescription();
    }
}