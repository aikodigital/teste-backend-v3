using System.Text;
using TheatricalPlayersRefactoringKata.Domain.Interface.UoW;

namespace TheatricalPlayersRefactoringKata.App.Model.Response
{
    public class Response
    {
        public readonly IUnitOfWork _uow;
        public bool HasErrors => !string.IsNullOrWhiteSpace(this.ErrorMessage);
        public string ErrorMessage { get; private set; }
        
        public Response()
        {

        }

        public Response(IUnitOfWork uow)
        {
            _uow = uow;
        }        

        public void AddErrorMessage(StringBuilder errors)
        {
            this.ErrorMessage = errors.ToString();

            _uow.HasErrors = this.HasErrors;
        }
        public void AddErrorMessage(string errors)
        {
            this.ErrorMessage = errors;

            _uow.HasErrors = this.HasErrors;
        }
    }

    public class Response<T> : Response
    {
        public T Value { get; set; }

        public Response(IUnitOfWork uow) :  base (uow) { }
    }
}