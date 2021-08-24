using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class UIntParse : IParseValue
    {
        public string Name => "uint";

        public bool Parse(string value, out object res)
        {
            res = 0u;
            if (string.IsNullOrWhiteSpace(value))
            {
                res = 0u;
                return true;
            }
            if (uint.TryParse(value, out var result))
            {
                res = result;
                return true;
            }
            return false;
        }
    }
}
