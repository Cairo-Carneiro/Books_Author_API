using API_NEW.Dto.Livro;
using API_NEW.Models;

namespace API_NEW.Services.Livros
{
    public interface ILivrosinterface
    {
            Task<ResponseModel<List<LivroModel>>> ListarLivros();
            Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
            Task<ResponseModel<LivroModel>> BuscarLivroPorIdAutor(int idAutor);
            Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);

            Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
            Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
    }
}

