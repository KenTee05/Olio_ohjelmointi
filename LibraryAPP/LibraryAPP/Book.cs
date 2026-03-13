public class Book : IReadable
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Availability { get; set; }

    public Book() { }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        Availability = "Available";
    }

    public virtual string GetDescription()
    {
        return $"{Title} - {Author}";
    }

    public override string ToString()
    {
        return $"{Title};{Author}";
    }
}