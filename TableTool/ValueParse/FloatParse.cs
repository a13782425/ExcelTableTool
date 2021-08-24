using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class FloatParse : IParseValue
    {
        public string Name => "float";

        public bool Parse(string value, out object res)
        {
            res = 0f;
            if (string.IsNullOrWhiteSpace(value))
            {
                res = 0f;
                return true;
            }
            if (float.TryParse(value, out var result))
            {
                res = result;
                return true;
            }
            return false;
        }
    }
}
