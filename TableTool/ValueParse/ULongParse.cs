using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ULongParse : IParseValue
    {
        public string Name => "ulong";

        public bool Parse(string value, out object res)
        {
            res = 0UL;
            if (string.IsNullOrWhiteSpace(value))
            {
                res = 0UL;
                return true;
            }
            if (long.TryParse(value, out var result))
            {
                res = result;
                return true;
            }
            return false;
        }
    }
}
