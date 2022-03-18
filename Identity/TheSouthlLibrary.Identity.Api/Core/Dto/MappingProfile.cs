using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Entities;

namespace TheSouthlLibrary.Identity.Api.Core.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
