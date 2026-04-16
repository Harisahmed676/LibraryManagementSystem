using AutoMapper;
using LibraryManagement.Core.DTOs.Book;
using LibraryManagement.Core.DTOs.Library;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.API.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Library, LibraryDto>()
                .ForMember(dest => dest.TotalBooks, opt => opt.MapFrom(src => src.Books != null ? src.Books.Count : 0));

            CreateMap<CreateLibraryDto, Library>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateLibraryDto, Library>();
        }
    }
}
