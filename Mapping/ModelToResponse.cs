using AutoMapper;
using LibraryApi.Contracts.Response;
using LibraryApi.Models;

namespace LibraryApi.Mapping
{
    public class ModelToResponse : Profile
    {
        public ModelToResponse()
        {
            CreateMap<Book, BookResponse>()
              .ForMember(dest => dest.Id, act => act
                .MapFrom(src => src.BookId));
        }
    }
}