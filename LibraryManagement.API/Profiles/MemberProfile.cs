using AutoMapper;
using LibraryManagement.Core.DTOs.Book;
using LibraryManagement.Core.DTOs.Member;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.API.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberDto, Member>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.MembershipNumber, opt => opt.Ignore());
            CreateMap<UpdateMemberDto, Member>();
        }
    }
}
