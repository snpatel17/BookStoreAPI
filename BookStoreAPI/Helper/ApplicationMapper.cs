using AutoMapper;
using BookStoreAPI.Data;

namespace BookStoreAPI.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BooksModel>().ReverseMap();
        }
    }
}
