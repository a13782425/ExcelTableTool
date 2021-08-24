using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    /// <summary>
    /// 代码生成基类
    /// </summary>
    public interface IGenerateCode
    {
        /// <summary>
        /// 代码生成名称,用于命令行(不要用中文,不区分大小写)
        /// 请确保名称唯一
        /// 如果名称重复则用最后读取的
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="package">包名,命名空间名</param>
        /// <param name="tableDto">表格数据</param>
        /// <param name="fileName">需要返回的类名(含后缀)</param>
        /// <returns></returns>
        string Generate(string package, TableDto tableDto, ref string fileName);
    }
}
