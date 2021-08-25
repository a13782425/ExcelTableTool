using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Transactions;
using TableCore;
using static TableTool.GlobalConst;

namespace TableTool
{
    class Program
    {
        //platform=client excelPath=Table
        static void Main(string[] args)
        {
            Console.WriteLine($"插件放置路径:{PLUGIN_PATH}");
            Tool.Init();

            if (!GenerateEnumCode.Generate())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"枚举关联生成失败");
                Console.ForegroundColor = ConsoleColor.White;
            }
            bool result = ResolveArgs(args);
            string exit = "0";
            if (!result)
            {
                ResetParams();
            }
            else
            {
                exit = Params["exit"];
                GenerateTable();
                ResetParams();
            }
            if (exit == "0")
            {
                while (true)
                {
                    string consoleText = Console.ReadLine();
                    string str = consoleText.ToLower();
                    switch (str)
                    {
                        case CONSOLE_EXIT:
                            goto Exit;
                        case CONSOLE_HELP:
                        case CONSOLE_HELP2:
                            ShowHelp();
                            break;
                        case CONSOLE_CLEAR:
                        case CONSOLE_CLEAN:
                            Console.Clear();
                            break;
                        default:
                            ResolveConsoleText(consoleText);
                            break;
                    }
                }
            }
        Exit: Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("关闭中。。。");
            Thread.Sleep(1000);
        }
        /// <summary>
        /// 解析控制台输入
        /// </summary>
        /// <param name="consoleText"></param>
        private static void ResolveConsoleText(string consoleText)
        {
            string[] args = consoleText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            bool result = ResolveArgs(args);
            if (!result)
            {
                ResetParams();
            }
            else
            {
                GenerateTable();
                ResetParams();
            }
        }

        /// <summary>
        /// 解析参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool ResolveArgs(string[] args)
        {
            foreach (var item in args)
            {
                string[] strs = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (strs.Length != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{item}参数错误,请使用?或者help确定参数");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                string str = strs[0].ToLower();
                if (Params.ContainsKey(str))
                {
                    Params[str] = strs[1];
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{item}参数不存在,已跳过,请使用?或者help确定参数");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return true;
        }

        private static void GenerateTable()
        {
            GenerateDto generate = new GenerateDto();
            if (!Tool.CheckExcel(generate))
            {
                return;
            }
            if (!Tool.ResolveExcel(generate))
            {
                return;
            }
            //如果有导出则生成数据
            if (Tool.CheckOutPath())
            {
                if (!Tool.GenerateData(generate))
                {
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(Params[CONSOLE_CODE_TYPE]))
            {
                if (Tool.CheckCodeOutPath())
                {
                    if (!Tool.GenerateCode(generate))
                    {
                        return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"未注明代码生成位置,请使用?或者help确定参数");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"生成成功,如需要关闭窗口,请输入exit或者关闭窗口");
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("?              -帮助");
            Console.WriteLine("help           -帮助");
            Console.WriteLine("platform       -平台(client,server),例platform=client");
            Console.WriteLine("excelpath      -表格文件夹(表格必须是.xlsx后缀)");
            Console.WriteLine("outpath        -数据输出文件夹");
            Console.WriteLine("format         -数据输出文件格式(csv,json,byte,luatable,luajson)");
            Console.WriteLine("hump           -驼峰命名");
            Console.WriteLine("allsheet       -是否是全部sheet,1代表全部,其他代表名为data的Sheet或第一个Sheet");
            Console.WriteLine("codetype       -代码类型(java,csharp)");
            Console.WriteLine("codepath       -代码文件夹");
            Console.WriteLine("namespace      -代码命名空间或包名");
            Console.WriteLine("encrypt        -简单加密字段(特殊需求)");
            Console.WriteLine("clear          -清理控制台");
            Console.WriteLine("clean          -清理控制台");
            Console.WriteLine("exit           -退出1=自动退出,0=手动推出");
        }
    }
}
