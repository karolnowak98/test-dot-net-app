using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetFirstCharacter(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
        { 
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(UpdateCharacterRequestDto newCharacter)
        {
            var response = await _characterService.UpdateCharacter(newCharacter);
            
            if (response.Data is null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            
            if (response.Data is null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }
    }
}