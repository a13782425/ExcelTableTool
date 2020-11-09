using System;
using System.Collections.Generic;
using System.Text;
using TableTool.GenerateCode;

namespace TableTool.Format
{
    public interface IFormat
    {
        void GenerateData();
        void GenerateCode();
    }

    public abstract class BaseFormat : IFormat
    {
        public virtual void GenerateCode()
        {
        }

        public virtual void GenerateData()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"改类型无法生成代码");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
