using System;
using System.Collections.Generic;
using System.Text;

namespace TableTool.GenerateCode
{
    public abstract class JavaGenerateCode : BaseGenerateCode
    {
        protected string GetTypeName(string typeName)
        {
            string text = typeName;
            if (text.StartsWith("enum_"))
            {
                text = "enum";
            }
            switch (text)
            {
                case "bool":
                    return "boolean";
                case "int":
                case "byte":
                case "short":
                case "long":
                case "float":
                    return typeName;
                case "uint":
                    return "int";
                case "ulong":
                    return "long";
                case "string":
                    return "String";
                case "enum":
                    {
                        return "int";
                    }
                case "int[]":
                    return "int[]";
                case "uint[]":
                    return "int[]";
                case "byte[]":
                    return "byte[]";
                case "short[]":
                    return "short[]";
                case "long[]":
                    return "long[]";
                case "ulong[]":
                    return "long[]";
                case "float[]":
                    return "float[]";
                case "string[]":
                    return "String[]";
                default:
                    return "int";
            }
        }
    }
}
