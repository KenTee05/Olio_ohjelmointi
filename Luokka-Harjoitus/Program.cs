using System;

namespace Luokka_Harjoitus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.AddBook(new Book("Clean Code", "Robert C. Martin"));
            library.AddBook(new Book("1984", "George Orwell"));

            library.SaveToFile("books.txt");
            library.LoadFromFile("books.txt");

            // Tulostetaan ladatut kirjat, jotta näet että toimii
            foreach (var book in library.GetBooks())
            {
                Console.WriteLine(book);
            }
        }
    }
}

