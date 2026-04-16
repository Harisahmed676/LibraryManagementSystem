using AutoMapper;
using LibraryManagement.Core.DTOs.Book;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.LibraryName, opt => opt.MapFrom(src => src.Library != null ? src.Library.Name : string.Empty));

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.TotalCopies))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateBookDto, Book>();
        }
    }
}
