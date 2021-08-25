using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TableCore;

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
            Params.Add("namespace", null);
            Params.Add("encrypt", null);
            Params.Add("hump", null);
            Params.Add("exit", "0");
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
            Params.Add("namespace", null);
            Params.Add("encrypt", null);
            Params.Add("hump", null);
            Params.Add("exit", "0");
            Params.Add("allsheet", "1");
        }
        public static Dictionary<string, string> Params = new Dictionary<string, string>();

        /// <summary>
        /// 插件根目录
        /// </summary>
        public const string PLUGIN_PATH = "Plugin";

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
        /// 运行指令-是否驼峰
        /// </summary>
        public const string CONSOLE_HUMP = "hump";
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
        ///// <summary>
        ///// 生成数据模型DTO
        ///// </summary>
        //public static GenerateDto Generate;

        /// <summary>
        /// 插件缓存
        /// key:插件名 value:插件程序集
        /// </summary>
        public readonly static Dictionary<string, Assembly> PluginDic = new Dictionary<string, Assembly>();

        /// <summary>
        /// 格式化类缓存
        /// key:格式化名 value:格式化对象
        /// </summary>
        public readonly static Dictionary<string, IFormat> FormatDic = new Dictionary<string, IFormat>();

        public readonly static Type FormatType = typeof(IFormat);

        /// <summary>
        /// 代码生成类缓存
        /// key:代码生成名 value:代码生成对象
        /// </summary>
        public readonly static Dictionary<string, IGenerateCode> GenerateDic = new Dictionary<string, IGenerateCode>();
        public readonly static Type GenerateType = typeof(IGenerateCode);

        /// <summary>
        /// 数据解析
        /// key:name value:数据解析对象
        /// </summary>
        public readonly static Dictionary<string, IParseValue> ParseValueDic = new Dictionary<string, IParseValue>();

    }
}
