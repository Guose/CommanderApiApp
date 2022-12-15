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
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsListAsync() 
        {
            var commandItems = _repository.GetAllCommands((bool)(_isAscending = true));

            return await Task.Run(() => _mapper.Map<ActionResult<IEnumerable<CommandReadDto>>>(commandItems));
        }

        // GET http://localhost:5000/api/commmands/{id}
        [HttpGet("{id}")]
        public async Task <ActionResult<CommandReadDto>> GetCommandAsync(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
            {
                return await Task.Run(() => _mapper.Map<ActionResult<CommandReadDto>>(commandItem));                
            }

            return NotFound();
        }

        // POST http://localhost:5000/api/commmands
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateNewCommandAsync(CommandCreateDto commandCreateDto)
        {
            var commandModel = await Task.Run(() => _mapper.Map<Command>(commandCreateDto));
            await Task.Run(() => _repository.CreateNewCommand(commandModel));

            var commandReadDto =  await Task.Run(() => _mapper.Map<CommandReadDto>(commandModel));

            return await Task.Run(() => CreatedAtRoute(nameof(GetCommandAsync), new { commandReadDto.Id }, commandReadDto));
        }

        // PUT http://localhost:5000/api/commmands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommandAsync(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = await Task.Run(() => _repository.GetCommandById(id));

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            await Task.Run(() => _mapper.Map(commandUpdateDto, commandModelFromRepo));
            await Task.Run(() => _repository.UpdateCommand(commandModelFromRepo));

            return NoContent();
        }

        // PATCH http://localhost:5000/api/commmands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCommandPartialAsync(int id, JsonPatchDocument<CommandUpdateDto> jsonPatch)
        {
            var commandFromRepo = await Task.Run(() => _repository.GetCommandById(id));

            if (commandFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = await Task.Run(() => _mapper.Map<CommandUpdateDto>(commandFromRepo));
            jsonPatch.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            await Task.Run(() => _mapper.Map(commandToPatch, commandFromRepo));
            await Task.Run(() => _repository.UpdateCommand(commandFromRepo));

            return NoContent();
        }

        // DELETE http://localhost:5000/api/commmands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommandAsync(int id)
        {
            var commandModelFromRepo = await Task.Run(() => _repository.GetCommandById(id));

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            await Task.Run(() => _repository.DeleteCommand(commandModelFromRepo));

            return NoContent();
        }
    }
}
