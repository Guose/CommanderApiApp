using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsMapProfile : Profile
    {
        // Source to target
        public CommandsMapProfile()
        {            
            // used for GET method
            CreateMap<Command, CommandReadDto>();

            // used for POST method
            CreateMap<CommandCreateDto, Command>();


            CreateMap<CommandUpdateDto, Command>();


            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
