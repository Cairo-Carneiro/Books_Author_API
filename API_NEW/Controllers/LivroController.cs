using API_NEW.Dto.Livro;
using API_NEW.Models;
using API_NEW.Services.Livros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_NEW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivrosinterface _livrosInterface;

        public LivroController(ILivrosinterface livrosInterface)
        {
            _livrosInterface = livrosInterface;
        }

        // Listar todos os livros
        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livrosInterface.ListarLivros();
            return Ok(livros);
        }

        // Buscar livro por ID
        [HttpGet("BuscarLivroPorID/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livrosInterface.BuscarLivroPorId(idLivro);
            return Ok(livro);
        }

        // Buscar livro por autor
        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            var livro = await _livrosInterface.BuscarLivroPorIdAutor(idAutor);
            return Ok(livro);
        }

        // Criar um novo livro
        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livros = await _livrosInterface.CriarLivro(livroCriacaoDto);
            return Ok(livros);
        }

        // Editar um livro existente
        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livros = await _livrosInterface.EditarLivro(livroEdicaoDto);
            return Ok(livros);
        }

        // Excluir um livro
        [HttpDelete("ExcluirLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ExcluirLivro(int idLivro)
        {
            var livros = await _livrosInterface.ExcluirLivro(idLivro);
            return Ok(livros);
        }
    }
}
