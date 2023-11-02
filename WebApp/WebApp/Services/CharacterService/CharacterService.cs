namespace WebApp.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        private static readonly List<Character> Characters = new()
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var dbCharacters = await _context.Characters.ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>
            {
                Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList()
            };
                
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>
            {
                Data = _mapper.Map<GetCharacterResponseDto>(character)
            };

            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found!!";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            var dbCharacters = await _context.Characters.ToListAsync();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = dbCharacters.Max(c => c.Id) + 1;
            dbCharacters.Add(character);
            //_mapper.Map<List<GetCharacterResponseDto>>(characters);
            
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>
            {
                Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList()
            };
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            
            try
            {
                var characterToUpdate = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if (characterToUpdate == null)
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                }

                _mapper.Map(updatedCharacter, characterToUpdate);
                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(characterToUpdate);
            }
            
            catch(Exception exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exception.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var dbCharacters = await _context.Characters.ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            
            try
            {
                var characterToRemove = await _context.Characters.SingleOrDefaultAsync(c => c.Id == id);

                if (characterToRemove == null)
                {
                    throw new Exception($"Character with Id '{id}' not found.");
                }

                dbCharacters.Remove(characterToRemove);
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            }
            
            catch(Exception exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exception.Message;
            }
            
            return serviceResponse;
        }
    }
}