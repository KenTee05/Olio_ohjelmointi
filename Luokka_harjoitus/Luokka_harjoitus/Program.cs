using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Program
{
    static void Main()
    {
        Library library = new Library();

        library.AddBook(new Book("Clean Code", "Robert C. Martin"));
        library.AddBook(new Book("1984", "George Orwell"));

        library.SaveToFile("books.txt");
        library.LoadFromFile("books.txt");
    }
}
