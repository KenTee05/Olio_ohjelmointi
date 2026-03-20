public class Book : IReadable // Book-luokka toteuttaa IReadable-rajapinnan
{
    public string Title { get; set; } // Kirjan nimi
    public string Author { get; set; } // Kirjan kirjoittaja
    public string Availability { get; set; } // Kirjan saatavuustieto

    public Book() { } // Tyhjä konstruktori

    public Book(string title, string author) // Konstruktori, joka saa nimen ja kirjoittajan
    {
        Title = title; // Tallennetaan annettu nimi Title-ominaisuuteen
        Author = author; // Tallennetaan annettu kirjoittaja Author-ominaisuuteen
        Availability = "Available"; // Oletuksena kirja on saatavilla
    }

    public virtual string GetDescription() // Metodi, joka palauttaa kirjan kuvauksen
    {
        return $"{Title} - {Author}"; // Palauttaa muodon: Nimi - Kirjoittaja
    }

    public override string ToString() // Ylikirjoitetaan ToString-metodi
    {
        return $"{Title};{Author}"; // Palautetaan kirjan tiedot puolipisteellä eroteltuna
    }
}