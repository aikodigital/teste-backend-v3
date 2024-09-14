namespace TheatricalPlayersRefactoringKata.Application.Response
{
    public class RespostaPadrao
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public RespostaPadrao(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
