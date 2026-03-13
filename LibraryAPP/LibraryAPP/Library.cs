using System.Collections.Generic;
using System.IO;
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

    // TALLENNUS TIEDOSTOON
    public void SaveToFile(string filePath)
    {
        List<string> lines = new List<string>();

        foreach (Book book in books)
        {
            lines.Add(book.ToString());
        }

        File.WriteAllLines(filePath, lines);
    }

    // LUKU TIEDOSTOSTA
    public void LoadFromFile(string filePath)
    {
        books.Clear();

        if (!File.Exists(filePath))
            return;

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            books.Add(new Book(parts[0], parts[1]));
        }
    }
}

