using BibliotecaAPI.Domains;
using BibliotecaAPI.Interfaces;

namespace BibliotecaAPI.Services
{
    public class LivroService
    {
        private readonly ILivroRepository _repository;

        public LivroService(ILivroRepository repository)
        {
            _repository = repository;
        }

        public List<Livro> Listar()
        {
            return _repository.Listar();
        }

        public Livro? BuscarPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public Livro Adicionar(Livro livro)
        {
            if(string.IsNullOrWhiteSpace(livro.titulo))
            {
                throw new Exception("O título é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(livro.autor))
            {
                throw new Exception("O autor é obrigatório");
            }

            if (livro.anoPublicacao <= 0)
            {
                throw new Exception("O ano de publicação é inválido");
            }

            return _repository.Adicionar(livro);
        }
    }
}
