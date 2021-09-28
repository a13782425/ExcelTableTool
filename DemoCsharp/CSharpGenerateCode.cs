using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace DemoCsharp
{
    public sealed class CSharpGenerateCode : IGenerateCode
    {
        public string Name => "csharp";

        public string Generate(string package, TableDto tableDto, ref string fileName)
        {


            return "";
        }
    }
}
