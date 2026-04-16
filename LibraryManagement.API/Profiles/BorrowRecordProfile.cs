using AutoMapper;
using LibraryManagement.Core.DTOs.Book;
using LibraryManagement.Core.DTOs.BorrowRecord;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.API.Profiles
{
    public class BorrowRecordProfile : Profile
    {
        public BorrowRecordProfile()
        {
            CreateMap<BorrowRecord, BorrowRecordDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : string.Empty))
                .ForMember(dest => dest.BookISBN, opt => opt.MapFrom(src => src.Book != null ? src.Book.ISBN : string.Empty))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member != null ? $"{src.Member.FirstName} {src.Member.LastName}" : string.Empty))
                .ForMember(dest => dest.MembershipNumber, opt => opt.MapFrom(src => src.Member != null ? src.Member.MembershipNumber : string.Empty));

            CreateMap<CreateBorrowRecordDto, BorrowRecord>()
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false));
        }
    }
}
