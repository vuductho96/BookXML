using System;
using System.Xml;
using System.Xml.Linq;

namespace CodeGym
{
    class Book
    {
        public string Title { get; set; }
        public float Price { get; set; }
    }

    class ReadWriteFileXML
    {
        static void Main(string[] args)
        {
            Book book = new Book();
            book.Title = "Đắc Nhân Tâm";
            book.Price = 123.5f;

            WriteToFile(book);

            Book readBook = ReadFromFile();
            Console.WriteLine($"Title: {readBook.Title} - Price: {readBook.Price}");
        }

        public static void WriteToFile(Book book)
        {
            try
            {
                XDocument xDoc;

                // Check if file exists
                if (System.IO.File.Exists("books.xml"))
                {
                    // Load existing XML file
                    xDoc = XDocument.Load("books.xml");
                }
                else
                {
                    // Create a new XML file
                    xDoc = new XDocument(new XElement("bookstore"));
                }

                // Create a new book element
                XElement newBook = new XElement("book",
                    new XElement("title", book.Title),
                    new XElement("price", book.Price.ToString()));

                // Add the new book element to the XML file
                xDoc.Element("bookstore").Add(newBook);

                // Save the changes to the XML file
                xDoc.Save("books.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static Book ReadFromFile()
        {
            Book book = new Book();

            try
            {
                XmlTextReader reader = new XmlTextReader("books.xml");

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "title")
                    {
                        // Read the title element and set the book's title property
                        reader.Read();
                        book.Title = reader.Value;
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "price")
                    {
                        // Read the price element and set the book's price property
                        reader.Read();
                        book.Price = float.Parse(reader.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return book;
        }
    }
}

