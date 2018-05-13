using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_ConsumingData
{
    public class BookHelper
    {
        List<Book> books;

        public BookHelper()
        {
            books = new List<Book>();
        }

        public void AddBookToList(string name, string authorName, string genre, double price)
        {
            books.Add(new Book
            {
                Name = name,
                AuthorName = authorName,
                Genre = genre,
                Price = price
            });
        }

        public void RemoveBookFromList(Book book)
        {
            books.Remove(book);
        }
    }
}
