using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.Entities;

namespace TheSouthlLibrary.Book.Api.Repository
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutores();
    }
}
