using System.Text.Json;
using BibliotecaAPI.Domains;
using BibliotecaAPI.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BibliotecaAPI.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        // Caminho do arquivo onde os livros serão salvos
        private readonly string _caminhoArquivo = "Dados/livros.json";

        // Implementando método para listar todos os livros cadastrados lá da interface
        public List<Livro> Listar()
        {
            // Usamos try/catch nesse método pois estamos trabalhando com arquivos
            try
            {
                // Verificar se o arquivo ainda não existe
                if (!File.Exists(_caminhoArquivo))
                {
                    return new List<Livro>();
                }
                // Se o arquivo existir vamos ler o conteúdo do json
                string json = File.ReadAllText(_caminhoArquivo);

                // Converter o json para uma lista de livros
                // Caso a conversão der null, retorna uma lista vázia
                return JsonSerializer.Deserialize<List<Livro>>(json)
                    ?? new List<Livro>();
            }
            catch
            {
                // Se der qualquer outro erro na leitura, retorna lista vázia
                return new List<Livro>();
            }
        }

        public Livro? BuscarPorId(int id)
        {
            Livro livro = Listar().FirstOrDefault(livro => livro.id == id);

            return livro;
        }

        // Salva a lista de livros no arquivo JSON
        private void salvar(List<Livro> livros)
        {
            // Cria a pasta "Dados" caso ela não exista
            Directory.CreateDirectory("Dados");

            // Converte a lista de livros para JSON
            string json = JsonSerializer.Serialize(livros, new JsonSerializerOptions{
                // Deixa o JSON formatado e fácil de ler
                WriteIndented = true
            });

            // Escreve o JSON no arquivo
            File.WriteAllText(_caminhoArquivo, json);
        }

        public Livro Adicionar(Livro livro)
        {
            // Carrega a lista atual de livros
            List<Livro> livros = Listar();

            // Gera ids automaticamente
            // Se já houver livros, pega o maior id e soma 1
            // Se não houver livros, começa com id = 1
            // Função Any() -> valida se existe algo e retorna true/false
            livro.id = livros.Any() ? livros.Max(livro => livro.id) + 1 : 1;

            livros.Add(livro);
            salvar(livros);

            return livro;
        }

        public bool Atualizar(int id, Livro livroAtualizado) // Podemos passar o nome que quisermos no final "livroAtualizado"
        {
            // Carregar a lista atual de livros
            List<Livro> livros = Listar();

            // Busca o livro que possui id informado
            Livro? livro = livros.FirstOrDefault(l => l.id == id);

            // Se não encontrar o livro, retorna falso
            if (livro == null)
            {
                return false;
            }

            // Atualiza os dados do livro encontrado
            livro.titulo = livroAtualizado.titulo;
            livro.autor = livroAtualizado.autor;
            livro.anoPublicacao = livroAtualizado.anoPublicacao;
            livro.disponivel = livroAtualizado.disponivel;

            // Método de salvar as alterações
            salvar(livros);

            // Retorna true indicando que atualizou com sucesso
            return true;
        }

        public bool Remover(int id)
        {
            List<Livro> livros = Listar();

            Livro livro = livros.FirstOrDefault(l => l.id == id);

            if (livro == null)
            {
                return false;
            }

            // Se encontrar o livro remove o livro da lista
            livros.Remove(livro);

            // Salvar livros (após a ação)
            salvar(livros);

            return true;
        }
    }
}
