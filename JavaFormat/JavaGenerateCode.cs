using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace JavaFormat
{
    internal sealed class JavaGenerateCode : IGenerateCode
    {
        public string Name => "java";

        public string Generate(string packageName, TableDto tableDto, ref string fileName)
        {
            return "";
        }
    }
}
