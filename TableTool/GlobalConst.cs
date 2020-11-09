using System;
using System.Collections.Generic;
using System.Text;

namespace TableTool
{
    public static class GlobalConst
    {
        static GlobalConst()
        {
            Params.Add("platform", "client");
            Params.Add("excelpath", null);
            Params.Add("outpath", null);
            Params.Add("codetype", null);
            Params.Add("codepath", null);
            Params.Add("format", null);
            Params.Add("extension", null);
            Params.Add("namespace", null);
            Params.Add("encrypt", null);
            Params.Add("allsheet", "1");
        }
        public static void ResetParams()
        {
            Params.Clear();
            Params.Add("platform", "client");
            Params.Add("excelpath", null);
            Params.Add("outpath", null);
            Params.Add("codetype", null);
            Params.Add("codepath", null);
            Params.Add("format", null);
            Params.Add("extension", null);
            Params.Add("namespace", null);
            Params.Add("encrypt", null);
            Params.Add("allsheet", "1");
        }
        public static Dictionary<string, string> Params = new Dictionary<string, string>();

        public const string GAME_ENUM_PATH = "GameEnum";
        /// <summary>
        /// 运行指令-平台
        /// </summary>
        public const string CONSOLE_PLATFORM = "platform";
        /// <summary>
        /// 运行指令-表格(文件夹)
        /// </summary>
        public const string CONSOLE_EXCEL_PATH = "excelpath";
        /// <summary>
        /// 运行指令-输出路径(文件夹)
        /// </summary>
        public const string CONSOLE_OUT_PATH = "outpath";
        /// <summary>
        /// 运行指令-代码类型
        /// </summary>
        public const string CONSOLE_CODE_TYPE = "codetype";
        /// <summary>
        /// 运行指令-代码路径(文件夹)
        /// </summary>
        public const string CONSOLE_CODE_PATH = "codepath";
        /// <summary>
        /// 运行指令-文件格式(csv?json?byte?luaTable)
        /// </summary>
        public const string CONSOLE_FORMAT = "format";
        /// <summary>
        /// 运行指令-文件后缀(.csv?.json,.byte?.luatable)
        /// </summary>
        public const string CONSOLE_EXTENSION = "extension";
        /// <summary>
        /// 运行指令-命名空间(包名)
        /// </summary>
        public const string CONSOLE_NAMESPACE = "namespace";
        /// <summary>
        /// 运行指令-加密密钥
        /// </summary>
        public const string CONSOLE_ENCRYPT = "encrypt";
        /// <summary>
        /// 运行指令-全部sheet
        /// </summary>
        public const string CONSOLE_ALL_SHEET = "allsheet";
        /// <summary>
        /// 运行指令-帮助
        /// </summary>
        public const string CONSOLE_HELP = "?";
        /// <summary>
        /// 运行指令-帮助2
        /// </summary>
        public const string CONSOLE_HELP2 = "help";
        /// <summary>
        /// 运行指令-清理控制台
        /// </summary>
        public const string CONSOLE_CLEAR = "clear";
        /// <summary>
        /// 运行指令-清理控制台
        /// </summary>
        public const string CONSOLE_CLEAN = "clean";
        /// <summary>
        /// 运行指令-退出
        /// </summary>
        public const string CONSOLE_EXIT = "exit";
        /// <summary>
        /// 客户端
        /// </summary>
        public const string PLATFORM_CLIENT = "client";
        /// <summary>
        /// 客户端
        /// </summary>
        public const string PLATFORM_SERVER = "server";

        /// <summary>
        /// 生成数据模型DTO
        /// </summary>
        public static GenerateDto Generate;
    }
}
