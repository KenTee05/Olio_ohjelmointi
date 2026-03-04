using System;

namespace Luokka_Harjoitus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.AddBook(new Book("Clean Code", "Robert C. Martin"));
            library.AddBook(new EBook("Digital Fortress", "Dan Brown", 5.2));

            foreach (Book book in library.GetBooks())
            {
                Console.WriteLine(book.GetDescription());
            }
        }
    }
}

