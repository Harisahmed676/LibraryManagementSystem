using AutoMapper;
using LibraryManagement.Core.DTOs.Member;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            return member == null ? null : _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto?> GetMemberByEmailAsync(string email)
        {
            var member = await _memberRepository.GetMemberByEmailAsync(email);
            return member == null ? null : _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateMemberAsync(CreateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);

            // Generate unique membership number
            member.MembershipNumber = $"MEM-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

            var created = await _memberRepository.AddAsync(member);
            return _mapper.Map<MemberDto>(created);
        }

        public async Task UpdateMemberAsync(int id, UpdateMemberDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) throw new KeyNotFoundException($"Member with id {id} not found");

            _mapper.Map(dto, member);
            await _memberRepository.UpdateAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) throw new KeyNotFoundException($"Member with id {id} not found");

            member.IsActive = false;
            await _memberRepository.UpdateAsync(member);
        }
    }
}