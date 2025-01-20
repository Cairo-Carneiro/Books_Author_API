using System.Linq.Expressions;
using API_NEW.Data;
using API_NEW.Dto.Autor;
using API_NEW.Models;
using Microsoft.EntityFrameworkCore;

namespace API_NEW.Services.Autor
{
    public class AutorServices : IAutorinterface
    {
        private readonly AppDbContext _context;

        public AutorServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            var resposta = new ResponseModel<List<AutorModel>>();

            try
            {
                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Todos os autores foram coletados!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao listar autores: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            var resposta = new ResponseModel<AutorModel>();

            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor localizado!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao buscar autor: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            var resposta = new ResponseModel<AutorModel>();

            try
            {
                // Busca o livro e inclui informações do autor relacionado
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor localizado!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao buscar autor por livro: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            var resposta = new ResponseModel<List<AutorModel>>();

            try
            {
  
                var novoAutor = new AutorModel
                {
                    Name = autorCriacaoDto.Name,
                    Sobrenome = autorCriacaoDto.Sobrenome
                };


                _context.Autores.Add(novoAutor);
                await _context.SaveChangesAsync();

                var autoresAtualizados = await _context.Autores.ToListAsync();

                resposta.Dados = autoresAtualizados;
                resposta.Mensagem = "Autor criado com sucesso!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao criar autor: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            var resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor localizado!";
                    return resposta;
                }

                autor.Name = autorEdicaoDto.Name; 
                autor.Sobrenome = autorEdicaoDto.Sobrenome;
                _context.Update(autor);

                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();

                resposta.Mensagem = "Autor Edidado com Sucesso!";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao excluir o autor: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }

        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            var resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                // Corrigido: agora aguardando o resultado assíncrono
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor); // Await foi adicionado

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor localizado!";
                    return resposta;
                }

                _context.Remove(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor Removido com Sucesso!";
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Erro ao excluir o autor: {ex.Message}";
                resposta.Status = false;

                return resposta;
            }
        }

    }
}


    

    

