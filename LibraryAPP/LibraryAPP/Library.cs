using System.Collections.Generic;

public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public List<Book> GetBooks()
    {
        return books;
    }
}