using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.Entities;
using TheSouthlLibrary.Book.Api.Repository;

namespace TheSouthlLibrary.Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMongoRepository<LibroEntity> _libroGenericoRepository;

        public LibroController(IMongoRepository<LibroEntity> libroGenericoRepository)
        {
            _libroGenericoRepository = libroGenericoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroEntity>>> Get()
        {
            var autores = await _libroGenericoRepository.GetAll();

            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroEntity>> GetById(string id)
        {
            var autor = await _libroGenericoRepository.GetById(id);

            return Ok(autor);
        }

        [HttpPost]
        public async Task Post(LibroEntity libro)
        {
            await _libroGenericoRepository.InsertDocument(libro);
        }

        [HttpPut("{id}")]
        public async Task Put(string id, LibroEntity autor)
        {
            autor.Id = id;

            await _libroGenericoRepository.UpdateDocument(autor);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _libroGenericoRepository.DeleteById(id);
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<LibroEntity>>> PostPagination(PaginationEntity<LibroEntity> pagination)
        {
            var result = await _libroGenericoRepository.PaginationByFilter(pagination);

            return Ok(result);
        }
    }
}
