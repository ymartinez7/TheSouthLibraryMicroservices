using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Entities;

namespace TheSouthlLibrary.Identity.Api.Core.Persistence
{
    public class SeguridadData
    {
        public static async Task InsertarUsuario(SeguridadContexto contexto, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nombre = "Yansel",
                    Apellidos = "Martinez Rodriguez",
                    Direccion = "Av de las suertes 68",
                    UserName = "ymartinez7",
                    Email = "ymartinez7@gmail.com"
                };

                await usuarioManager.CreateAsync(usuario, "Madrid_2020");
            }
        }
    }
}
