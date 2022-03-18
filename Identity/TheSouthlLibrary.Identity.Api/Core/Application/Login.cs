using AutoMapper;
using FluentValidation;
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
using TheSouthlLibrary.Identity.Api.Core.Persistence;

namespace TheSouthlLibrary.Identity.Api.Core.Application
{
    public class Login
    {
        public class UsuarioLoginComand : IRequest<UsuarioDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioLoginValidation : AbstractValidator<UsuarioLoginComand>
        {
            public UsuarioLoginValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginComand, UsuarioDto>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly SignInManager<Usuario> _signinManager;

            public UsuarioLoginHandler(SeguridadContexto context,
                UserManager<Usuario> userManager,
                IMapper mapper,
                IJwtGenerator jwtGenerator,
                SignInManager<Usuario> signinManager)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
                _signinManager = signinManager;
            }

            public async Task<UsuarioDto> Handle(UsuarioLoginComand request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }

                var resultado = await _signinManager.CheckPasswordSignInAsync(usuario, request.Password, true);

                if (resultado.Succeeded)
                {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDto>(usuario);

                    usuarioDTO.Token = _jwtGenerator.CreateToken(usuario);

                    return usuarioDTO;
                }

                throw new Exception("Login incorrecto");
            }
        }
    }
}
