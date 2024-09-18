using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ValueObjects
{
    public class Error
    {
        public int Code { get; set; }
        public string Atributo { get; set; }
        public string Message { get; set; }

        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(int code, string atributo, string message)
        {
            Code = code;
            Atributo = atributo;
            Message = message;
        }
    }
}

