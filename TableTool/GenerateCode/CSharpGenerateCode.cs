//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace TableTool.GenerateCode
//{
//    public abstract class CSharpGenerateCode : BaseGenerateCode
//    {
//        protected string GetTypeName(string typeName)
//        {
//            string text = typeName;
//            if (text.StartsWith("enum_"))
//            {
//                text = "enum";
//            }
//            switch (text)
//            {
//                case "bool":
//                case "int":
//                case "byte":
//                case "short":
//                case "long":
//                case "float":
//                case "uint":
//                case "ulong":
//                case "string":
//                    return typeName;
//                case "enum":
//                    {
//                        return "int";
//                    }
//                case "int[]":
                  
//                case "uint[]":
//                case "byte[]":
//                case "short[]":
//                case "long[]":
//                case "ulong[]":
//                case "float[]":
//                case "string[]":
//                    return typeName;
//                default:
//                    return "int";
//            }
//        }
//    }
//}
