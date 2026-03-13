public class EBook : Book
{
    public double FileSizeMB { get; set; }

    public EBook(string title, string author, double size)
        : base(title, author)
    {
        FileSizeMB = size;
        Availability = "Available (E-Book)";
    }

    public override string GetDescription()
    {
        return $"{Title} - {Author}";
    }
}