using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Dto;
using TheSouthlLibrary.Identity.Api.Core.Entities;
using TheSouthlLibrary.Identity.Api.Core.JwtLogic;
using TheSouthlLibrary.Identity.Api.Core.Persistence;

namespace TheSouthlLibrary.Identity.Api.Core.Application
{
    public class Register
    {
        public class UsuarioRegisterCommand : IRequest<UsuarioDto>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioRegisterValidation : AbstractValidator<UsuarioRegisterCommand>
        {
            public UsuarioRegisterValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioRegisterHandler : IRequestHandler<UsuarioRegisterCommand, UsuarioDto>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;

            public UsuarioRegisterHandler(SeguridadContexto contexto, 
                UserManager<Usuario> userManager, 
                IMapper mapper,
                IJwtGenerator jwtGenerator)
            {
                _context = contexto;
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UsuarioDto> Handle(UsuarioRegisterCommand request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();

                if (existe)
                {
                    throw new Exception("El email del usuario ya existe en la base de datos");
                }

                existe = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();

                if (existe)
                {
                    throw new Exception("El nombre de usuario ya existe en la base de datos");
                }

                var usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    Apellidos = request.Apellido,
                    Email = request.Email,
                    UserName = request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDto>(usuario);

                    usuarioDTO.Token = _jwtGenerator.CreateToken(usuario);

                    return usuarioDTO;
                }

                throw new Exception("No se pudo registrar el usuario");
            }
        }
    }
}
