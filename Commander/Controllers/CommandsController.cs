using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        private bool? _isAscending;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET http://localhost:5000/api/commmands
        [HttpGet]
        public async Task<ActionResult<List<CommandReadDto>>> GetCommandsListAsync() 
        {
            var commandItems = await _repository.GetAllCommandsAsync((bool)(_isAscending = true));

            if (commandItems != null)
            {
                return await Task.Run(() => _mapper.Map<List<CommandReadDto>>(commandItems));                
            }

            return NotFound();
        }

        // GET http://localhost:5000/api/commmands/{id}
        [HttpGet("{id}", Name = "GetCommandAsync")]
        public async Task<ActionResult<CommandReadDto>> GetCommandAsync(int id)
        {
            var commandItem = await _repository.GetCommandByIdAsync(id);

            if (commandItem != null)
            {
                return await Task.Run(() => _mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        // POST http://localhost:5000/api/commmands
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateNewCommandAsync(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            var commandModelId = await _repository.CreateNewCommandAsync(commandModel);

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModelId);

            return CreatedAtRoute(nameof(GetCommandAsync), new { id = commandReadDto.Id }, commandReadDto);
        }

        // PUT http://localhost:5000/api/commmands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommandAsync(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = await _repository.GetCommandByIdAsync(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            await _repository.UpdateCommandAsync(commandModelFromRepo);

            return NoContent();
        }

        // PATCH http://localhost:5000/api/commmands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCommandPartialAsync(int id, JsonPatchDocument<CommandUpdateDto> jsonPatch)
        {
            var commandFromRepo = await _repository.GetCommandByIdAsync(id);

            if (commandFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandFromRepo);
            jsonPatch.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandFromRepo);
            await _repository.UpdateCommandAsync(commandFromRepo);

            return NoContent();
        }

        // DELETE http://localhost:5000/api/commmands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommandAsync(int id)
        {
            var commandModelFromRepo = await _repository.GetCommandByIdAsync(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            await _repository.DeleteCommandAsync(commandModelFromRepo);

            return NoContent();
        }
    }
}
