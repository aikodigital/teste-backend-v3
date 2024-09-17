using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ValidacaoErrorException : Exception
    {
        public List<(string name, string ErrorMessage)> ErrorMessages { get; set; }
        public ValidacaoErrorException() : base() { }
        public ValidacaoErrorException(string name, string errorMessage) : base(errorMessage)
        {
            ErrorMessages = new List<(string name, string ErrorMessage)> { (name, errorMessage) };
        }
        // Construtor que aceita uma mensagem de erro única
        public ValidacaoErrorException(List<(string name, string ErrorMessage)> values, string erroValidacao) : base(erroValidacao)
        {
            ErrorMessages = new List<(string name, string ErrorMessage)>();
            ErrorMessages = values;
        }
        public void InsertExceptions(string key, string error)
        {
            if (ErrorMessages == null)
            {
                ErrorMessages = new List<(string name, string ErrorMessage)>();
            }
            ErrorMessages.Add((key, error));

        }
    }
}
