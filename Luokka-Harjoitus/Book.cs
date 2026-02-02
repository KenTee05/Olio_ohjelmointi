public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book() { }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        return $"{Title};{Author}";
    }
}
