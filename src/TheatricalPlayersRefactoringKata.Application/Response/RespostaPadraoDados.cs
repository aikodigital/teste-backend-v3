namespace TheatricalPlayersRefactoringKata.Application.Response
{
    public class RespostaPadraoDados<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Data { get; set; }

        public RespostaPadraoDados(bool sucesso, string mensagem, T data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }
    }
}
