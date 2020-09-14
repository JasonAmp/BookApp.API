using BookAPP.Core.Services;

namespace BookAPP.API.Services
{
    public class BookService : IBookService
    {
        public string GenerateISBN(int length)
        {
            return "111";
        }
    }
}