using System.Collections.Generic; // Tarvitaan List-kokoelman käyttöön

public class Library // Luokka, joka edustaa kirjastoa
{
    private List<Book> books = new List<Book>(); // Yksityinen lista, johon tallennetaan kirjat

    public void AddBook(Book book) // Metodi kirjan lisäämiseen listaan
    {
        books.Add(book); // Lisätään annettu kirja listaan
    }

    public List<Book> GetBooks() // Metodi, joka palauttaa kaikki kirjat
    {
        return books; // Palautetaan kirjalista
    }
}