using System.Collections.Generic; // List-kokoelmaa varten

public class Library // Kirjastoluokka kirjojen hallintaan
{
    private List<Book> books = new List<Book>(); // Lista kaikista kirjoista

    public void AddBook(Book book) // Lisää kirjan listaan
    {
        books.Add(book);
    }

    public List<Book> GetBooks() // Palauttaa kirjalistan
    {
        return books;
    }

    public void ClearBooks() // Tyhjentää kirjalistan
    {
        books.Clear();
    }
}