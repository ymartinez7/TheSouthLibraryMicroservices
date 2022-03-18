using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Dto;
using TheSouthlLibrary.Identity.Api.Core.Entities;
using TheSouthlLibrary.Identity.Api.Core.JwtLogic;

namespace TheSouthlLibrary.Identity.Api.Core.Application
{
    public class UsuarioActual
    {
        public class UsuarioActualCommand : IRequest<UsuarioDto> { }

        public class UsuarioActualHandler : IRequestHandler<UsuarioActualCommand, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUsuarioSesion _usuarioSesion;

            public UsuarioActualHandler(UserManager<Usuario> userManager,
                IMapper mapper,
                IJwtGenerator jwtGenerator,
                IUsuarioSesion usuarioSesion)
            {
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<UsuarioDto> Handle(UsuarioActualCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.GetUsuarioSesion());

                if (usuario == null)
                {
                    throw new Exception("Ocurrió un error al buscar el usuario actual");
                }

                var usuarioDTO = _mapper.Map<Usuario, UsuarioDto>(usuario);

                usuarioDTO.Token = _jwtGenerator.CreateToken(usuario);

                return usuarioDTO;
            }
        }
    }
}
