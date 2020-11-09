using System;
using System.Collections.Generic;
using System.Text;

namespace TableTool.GenerateCode
{
    public interface IGenerateCode
    {
        void GenerateCode();
    }

    public abstract class BaseGenerateCode : IGenerateCode
    {
        public virtual void GenerateCode()
        {

        }
    }
}
