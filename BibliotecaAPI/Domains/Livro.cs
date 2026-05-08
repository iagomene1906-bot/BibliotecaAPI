namespace BibliotecaAPI.Domains
{
    public class Livro
    {
        public int id { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string autor {  get; set; } = string.Empty;
        public int anoPublicacao { get; set; }
        public bool disponivel { get; set; } = true;
    }
}
