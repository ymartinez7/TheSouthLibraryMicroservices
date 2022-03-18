using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.Entities;

namespace TheSouthlLibrary.Book.Api.Core.ContextMongoDB
{
    public class AutorContext : IAutorContext
    {
        private readonly IMongoDatabase _db;

        public AutorContext(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Autor> Autores => _db.GetCollection<Autor>("Autor");
    }
}
