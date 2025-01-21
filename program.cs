using System;
using System.Collections.Generic;

// Book Class
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool Available { get; set; }

    public Book(string title, string author, string isbn, bool available)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Available = available;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Available: {Available}";
    }
}

// EBook Subclass
public class EBook : Book
{
    public int FileSize { get; set; } // in MB

    public EBook(string title, string author, string isbn, bool available, int fileSize)
        : base(title, author, isbn, available)
    {
        FileSize = fileSize;
    }

    public override string ToString()
    {
        return base.ToString() + $", File Size: {FileSize} MB";
    }
}

// Library Class
public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(string isbn)
    {
        books.RemoveAll(b => b.ISBN == isbn);
    }

    public Book SearchByTitle(string title)
    {
        return books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public void ListBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}

// Singleton LibraryManager Class
public class LibraryManager
{
    private static LibraryManager instance;
    private Library library = new Library();

    private LibraryManager() { }

    public static LibraryManager Instance => instance ??= new LibraryManager();

    public Library Library => library;

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nLibrary Menu:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Search Book");
            Console.WriteLine("4. List Books");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBookMenu();
                    break;
                case "2":
                    RemoveBookMenu();
                    break;
                case "3":
                    SearchBookMenu();
                    break;
                case "4":
                    library.ListBooks();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid selection. Try again.");
                    break;
            }
        }
    }

    private void AddBookMenu()
    {
        Console.Write("Enter book type (1 for Book, 2 for EBook): ");
        string type = Console.ReadLine();
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();
        Console.Write("Available (true/false): ");
        bool available = bool.Parse(Console.ReadLine());

        if (type == "2")
        {
            Console.Write("File Size (MB): ");
            int fileSize = int.Parse(Console.ReadLine());
            library.AddBook(new EBook(title, author, isbn, available, fileSize));
        }
        else
        {
            library.AddBook(new Book(title, author, isbn, available));
        }
        Console.WriteLine("Book added successfully.");
    }

    private void RemoveBookMenu()
    {
        Console.Write("Enter ISBN of the book to remove: ");
        string isbn = Console.ReadLine();
        library.RemoveBook(isbn);
        Console.WriteLine("Book removed successfully.");
    }

    private void SearchBookMenu()
    {
        Console.Write("Enter the title of the book to search: ");
        string title = Console.ReadLine();
        var book = library.SearchByTitle(title);
        if (book != null)
        {
            Console.WriteLine("Book found: " + book);
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }
}

// Program Entry Point
class Program
{
    static void Main(string[] args)
    {
        LibraryManager.Instance.ShowMenu();
    }
}
