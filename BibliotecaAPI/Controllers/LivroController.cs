using BibliotecaAPI.Domains;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _service;

        public LivroController(LivroService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            List<Livro> livros = _service.Listar();

            return Ok(livros);
            // return StatusCode(200, livros);
        }

        [HttpGet("{id}")]
        public ActionResult BuscarPorId(int id)
        {
            Livro livro = _service.BuscarPorId(id);

            if(livro == null)
            {
                return NotFound("Livro não encontrado.");
                // return StatusCode(404, "Livro não encontrado.");
            }

            return Ok(livro);

        }

        [HttpPost]
        public ActionResult Adicionar(Livro livro)
        {
            try
            {
                Livro novoLivro = _service.Adicionar(livro);
                return Created();
                // return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
                // return StatusCode(400);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, Livro livro)
        {
            bool atualizado = _service.Atualizar(id, livro);

            try
            {
                if (!atualizado)
                {
                    return NotFound("Livro não encontrado.");
                }
                return Ok("Livro atualizado com sucesso!");
                }

            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            bool removido = _service.Remover(id);

            if (!removido)
            {
                return NotFound("Livro não encontrado");
            }

            return NoContent();
        }
    }
}
