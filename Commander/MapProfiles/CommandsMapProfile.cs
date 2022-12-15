using AutoMapper;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Profiles
{
    public class CommandsMapProfile : Profile
    {
        public CommandsMapProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<ActionResult, Command>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
