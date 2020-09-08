using LibraryApi.Contracts.Request;
using LibraryApi.Models;
using AutoMapper;
using System;

namespace LibraryApi.Mapping
{
    public class RequestToModel : Profile
    {
        public RequestToModel()
        {
            CreateMap<UpdateBookRequest, Book>();
            CreateMap<CreateBookRequest, Book>()
              .ForMember(dest => dest.BookId, act => act
                .MapFrom(_ => Guid.NewGuid()));
        }
    }
}