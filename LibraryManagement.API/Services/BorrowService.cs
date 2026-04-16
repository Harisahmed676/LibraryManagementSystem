using AutoMapper;
using LibraryManagement.Core.DTOs.BorrowRecord;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.API.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRecordRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public BorrowService(
            IBorrowRecordRepository borrowRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            IMapper mapper)
        {
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BorrowRecordDto>> GetAllBorrowsAsync()
        {
            var borrows = await _borrowRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BorrowRecordDto>>(borrows);
        }

        public async Task<IEnumerable<BorrowRecordDto>> GetBorrowsByMemberAsync(int memberId)
        {
            var borrows = await _borrowRepository.GetBorrowsByMemberAsync(memberId);
            return _mapper.Map<IEnumerable<BorrowRecordDto>>(borrows);
        }

        public async Task<IEnumerable<BorrowRecordDto>> GetActiveBorrowsAsync()
        {
            var borrows = await _borrowRepository.GetActiveBorrowsAsync();
            return _mapper.Map<IEnumerable<BorrowRecordDto>>(borrows);
        }

        public async Task<IEnumerable<BorrowRecordDto>> GetOverdueBorrowsAsync()
        {
            var borrows = await _borrowRepository.GetOverdueBorrowsAsync();
            return _mapper.Map<IEnumerable<BorrowRecordDto>>(borrows);
        }

        public async Task<BorrowRecordDto> BorrowBookAsync(CreateBorrowRecordDto dto)
        {
            // Check book exists and is available
            var book = await _bookRepository.GetByIdAsync(dto.BookId);
            if (book == null) throw new KeyNotFoundException($"Book with id {dto.BookId} not found");
            if (book.AvailableCopies <= 0) throw new InvalidOperationException("No available copies of this book");

            // Check member exists and is active
            var member = await _memberRepository.GetByIdAsync(dto.MemberId);
            if (member == null) throw new KeyNotFoundException($"Member with id {dto.MemberId} not found");
            if (!member.IsActive) throw new InvalidOperationException("Member is not active");
            if (member.MembershipEndDate < DateTime.UtcNow) throw new InvalidOperationException("Member's membership has expired");

            // Create borrow record
            var borrowRecord = _mapper.Map<BorrowRecord>(dto);

            // Decrease available copies
            book.AvailableCopies--;
            await _bookRepository.UpdateAsync(book);

            var created = await _borrowRepository.AddAsync(borrowRecord);

            // Reload with includes
            var result = await _borrowRepository.GetBorrowsByMemberAsync(dto.MemberId);
            var latest = result.FirstOrDefault(x => x.Id == created.Id);

            return _mapper.Map<BorrowRecordDto>(latest ?? created);
        }

        public async Task ReturnBookAsync(ReturnBookDto dto)
        {
            var borrowRecord = await _borrowRepository.GetByIdAsync(dto.BorrowRecordId);
            if (borrowRecord == null) throw new KeyNotFoundException($"Borrow record with id {dto.BorrowRecordId} not found");
            if (borrowRecord.IsReturned) throw new InvalidOperationException("This book has already been returned");

            // Update borrow record
            borrowRecord.IsReturned = true;
            borrowRecord.ReturnDate = dto.ReturnDate;
            await _borrowRepository.UpdateAsync(borrowRecord);

            // Increase available copies
            var book = await _bookRepository.GetByIdAsync(borrowRecord.BookId);
            if (book != null)
            {
                book.AvailableCopies++;
                await _bookRepository.UpdateAsync(book);
            }
        }
    }
}