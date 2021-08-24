using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ByteParse : IParseValue
    {
        public string Name => "byte";

        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                res = 0;
                return true;
            }
            if (byte.TryParse(value, out var result))
            {
                res = result;
                return true;
            }
            return false;
        }
    }
}
