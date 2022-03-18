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
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;

        public AutorController(IAutorRepository autorRepository,
            IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            _autorRepository = autorRepository;
            _autorGenericoRepository = autorGenericoRepository;
        }

        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutor()
        {
            var autores = await _autorRepository.GetAutores();

            return Ok(autores);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> Get()
        {
            var autores = await _autorGenericoRepository.GetAll();

            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> GetById(string id)
        {
            var autor = await _autorGenericoRepository.GetById(id);

            return Ok(autor);
        }

        [HttpPost]
        public async Task Post(AutorEntity autor)
        {
            await _autorGenericoRepository.InsertDocument(autor);
        }

        [HttpPut("{id}")]
        public async Task Put(string id, AutorEntity autor)
        {
            autor.Id = id;

            await _autorGenericoRepository.UpdateDocument(autor);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _autorGenericoRepository.DeleteById(id);
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination(PaginationEntity<AutorEntity> pagination)
        {
            var result = await _autorGenericoRepository.PaginationByFilter(pagination);

            return Ok(result);
        }
    }
}
