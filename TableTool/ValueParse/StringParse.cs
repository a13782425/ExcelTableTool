using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class StringParse : IParseValue
    {
        public string Name => "string";

        public bool Parse(string value, out object res)
        {
            res = "";
            if (string.IsNullOrWhiteSpace(value))
            {
                res = "";
                return true;
            }
            res = value;
            return true;
        }
    }
}
