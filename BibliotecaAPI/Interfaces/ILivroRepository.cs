using BibliotecaAPI.Domains;

namespace BibliotecaAPI.Interfaces
{
    public interface ILivroRepository
    {
        List<Livro> Listar();

        // A interrogação (?) significa que o método pode retornar null
        // Esse método pode retornar um Livro ou NADA
        Livro? BuscarPorId(int id);

        Livro Adicionar(Livro livro);
    }
}
