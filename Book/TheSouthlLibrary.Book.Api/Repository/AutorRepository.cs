using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.ContextMongoDB;
using TheSouthlLibrary.Book.Api.Core.Entities;

namespace TheSouthlLibrary.Book.Api.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IAutorContext _autorContext;

        public AutorRepository(IAutorContext autorContext)
        {
            _autorContext = autorContext;
        }
       
        public async Task<IEnumerable<Autor>> GetAutores()
        {
            return await _autorContext.Autores.Find(p => true).ToListAsync();
        }
    }
}
