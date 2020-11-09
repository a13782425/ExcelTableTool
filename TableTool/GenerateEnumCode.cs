using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.CSharp;

namespace TableTool
{
    public class GenerateEnumCode
    {
        /// <summary>
        /// 枚举文件夹
        /// </summary>
        static string EnumFolder = "Enums";
        static string MatchWord = @"//(.*)\nenum (\w*){([\w\W]*)}";
        public static bool Generate()
        {
            //string[] files = Directory.GetFiles(EnumFolder, "*.txt", SearchOption.AllDirectories);
            //foreach (var item in files)
            //{
            //    string text = File.ReadAllText(item);
            //    MatchCollection mc = Regex.Matches(text, MatchWord);
            //    foreach (Match match in mc)
            //    {
            //        Console.WriteLine(match.Value);
            //    }
            //}
            return true;
        }

        //private string GetCode(string path)
        //{
        //    return File.ReadAllText(path);
        //}
    }
}
