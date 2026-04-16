using AutoMapper;
using LibraryManagement.Core.DTOs.Library;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.API.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public LibraryService(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LibraryDto>> GetAllLibrariesAsync()
        {
            var libraries = await _libraryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LibraryDto>>(libraries);
        }

        public async Task<LibraryDto?> GetLibraryByIdAsync(int id)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
            return library == null ? null : _mapper.Map<LibraryDto>(library);
        }

        public async Task<LibraryDto?> GetLibraryWithBooksAsync(int id)
        {
            var library = await _libraryRepository.GetLibraryWithBooksAsync(id);
            return library == null ? null : _mapper.Map<LibraryDto>(library);
        }

        public async Task<LibraryDto> CreateLibraryAsync(CreateLibraryDto dto)
        {
            var library = _mapper.Map<Library>(dto);
            var created = await _libraryRepository.AddAsync(library);
            return _mapper.Map<LibraryDto>(created);
        }

        public async Task UpdateLibraryAsync(int id, UpdateLibraryDto dto)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
            if (library == null) throw new KeyNotFoundException($"Library with id {id} not found");

            _mapper.Map(dto, library);
            await _libraryRepository.UpdateAsync(library);
        }

        public async Task DeleteLibraryAsync(int id)
        {
            var library = await _libraryRepository.GetByIdAsync(id);
            if (library == null) throw new KeyNotFoundException($"Library with id {id} not found");

            library.IsActive = false;
            await _libraryRepository.UpdateAsync(library);
        }
    }
}