using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Entities;

namespace TheSouthlLibrary.Identity.Api.Core.JwtLogic
{
    public interface IJwtGenerator
    {
        string CreateToken(Usuario usuario);
    }
}
