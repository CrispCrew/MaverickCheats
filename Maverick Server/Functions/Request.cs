using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Functions
{
    public class Request
    {
        public string Data;

        public Request(string Data)
        {
            this.Data = Data.Substring(1, Data.Length - 1);
        }

        public bool Contains(string Variable)
        {
            if (!Data.Contains(Variable + "="))
                return false;

            return true;
        }

        public string Get(string Variable)
        {
            if (!Data.Contains(Variable + "="))
                return null;

            string[] posted = Data.Split('&');

            foreach (string post in posted)
            {
                if (post.Contains(Variable + "="))
                {
                    return post.Replace(Variable + "=", "");
                }
            }

            return Data;
        }
    }
}
