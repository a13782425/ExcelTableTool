using System;
using System.Collections.Generic;

namespace TableCore
{
    /// <summary>
    /// 格式化基类
    /// </summary>
    public interface IFormat
    {
        /// <summary>
        /// 格式化名称,用于命令行(不要用中文,不区分大小写)
        /// 请确保名称唯一
        /// 如果名称重复则用最后读取的
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取自定义数值解析
        /// </summary>
        /// <returns></returns>
        List<IParseValue> GetCustomParse() => new List<IParseValue>();
        /// <summary>
        /// 格式化数据
        /// </summary>
        /// <param name="tableDto"></param>
        /// <param name="parse"></param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        string Format(TableDto tableDto, ValueParse parse, ref string fileName);
    }
}
