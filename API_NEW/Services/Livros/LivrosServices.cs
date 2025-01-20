using API_NEW.Data;
using API_NEW.Dto.Livro;
using API_NEW.Models;
using Microsoft.EntityFrameworkCore;

namespace API_NEW.Services.Livros
{
    public class LivroServices : ILivrosinterface
    {
        private readonly AppDbContext _context;

        public LivroServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livros = await _context.Livros.Include(l => l.Autor).ToListAsync();

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram coletados!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao listar livros: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            var resposta = new ResponseModel<LivroModel>();

            try
            {
                var livro = await _context.Livros
                    .Include(l => l.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro localizado!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao buscar livro: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorIdAutor(int idAutor)
        {
            var resposta = new ResponseModel<LivroModel>();

            try
            {
                var livro = await _context.Livros
                    .Include(l => l.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Autor.Id == idAutor);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado para esse autor!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro do autor localizado!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao buscar livro por autor: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livroCriacaoDto.AutorId);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                var novoLivro = new LivroModel
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Livros.Add(novoLivro);
                await _context.SaveChangesAsync();

                var livrosAtualizados = await _context.Livros.Include(l => l.Autor).ToListAsync();

                resposta.Dados = livrosAtualizados;
                resposta.Mensagem = "Livro criado com sucesso!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao criar livro: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros
                    .Include(l => l.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livroEdicaoDto.AutorId);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                var livrosAtualizados = await _context.Livros.Include(l => l.Autor).ToListAsync();

                resposta.Dados = livrosAtualizados;
                resposta.Mensagem = "Livro editado com sucesso!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao editar livro: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();

                var livrosAtualizados = await _context.Livros.Include(l => l.Autor).ToListAsync();

                resposta.Dados = livrosAtualizados;
                resposta.Mensagem = "Livro excluído com sucesso!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao excluir livro: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }
    }
}
