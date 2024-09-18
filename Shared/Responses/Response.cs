using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response
{
    public class Response
    {
        public string status { get; set; }
        public dynamic? data { get; set; }
        public int code { get; set; }

        public IList<Dictionary<string, string>> errmsg { get; set; } = new List<Dictionary<string, string>>();

        public void setErrmsg(string key, string value, Response response)
        {
            response.errmsg.Add(new Dictionary<string, string> { { key, value } });
        }

        public void insert(string status, int code, dynamic data)
        {
            this.status = status;
            this.data = data;
            this.code = code;

        }
        public void insertCommand(string status, int code, dynamic data)
        {

            this.status = status;
            this.data = data;
            this.code = code;


        }
    }
}
