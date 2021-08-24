using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    /// <summary>
    /// 格式化数值
    /// </summary>
    public interface IParseValue
    {

        /// <summary>
        /// 表格中的类型(不区分大小写)
        /// 如果重复则取最新的
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        bool Parse(string value, out object res);
    }
}
