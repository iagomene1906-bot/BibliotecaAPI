using BibliotecaAPI.Domains;

namespace BibliotecaAPI.Interfaces
{
    public interface ILivroRepository
    {
        // Interface cria o contrato, método que vamos chamar nas outras classes e vamos fazendo alterações do que ele precisa ter
        List<Livro> Listar();

        // A interrogação (?) significa que o método pode retornar null
        // Esse método pode retornar um Livro ou NADA
        Livro? BuscarPorId(int id);

        Livro Adicionar(Livro livro);

        bool Atualizar(int id, Livro livro);
        
        bool Remover(int id);
    }
}
