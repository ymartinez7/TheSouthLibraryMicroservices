using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.Entities;

namespace TheSouthlLibrary.Book.Api.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();

        Task<TDocument> GetById(string id);

        Task InsertDocument(TDocument document);

        Task UpdateDocument(TDocument document);

        Task DeleteById(string id);

        Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument, bool>> FilterExpression, 
            PaginationEntity<TDocument> pagination);

        Task<PaginationEntity<TDocument>> PaginationByFilter(PaginationEntity<TDocument> pagination);
    }
}
