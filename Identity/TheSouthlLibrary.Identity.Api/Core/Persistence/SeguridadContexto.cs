using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Entities;

namespace TheSouthlLibrary.Identity.Api.Core.Persistence
{
    public class SeguridadContexto : IdentityDbContext<Usuario>
    {
        public SeguridadContexto(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
