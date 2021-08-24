using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class IntParse : IParseValue
    {
        public string Name => "int";

        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                res = 0;
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                res = result;
                return true;
            }
            return false;
        }
    }
}
