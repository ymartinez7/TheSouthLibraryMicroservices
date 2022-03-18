using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.Entities;

namespace TheSouthlLibrary.Book.Api.Core.ContextMongoDB
{
    public interface IAutorContext
    {
        IMongoCollection<Autor> Autores { get; }
    }
}
