using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    /// <summary>
    /// 一行数据
    /// </summary>
    public sealed class RowDataDto
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNum { get; private set; }

        public string  Id { get; set; }


        /// <summary>
        /// 当前一行的数据
        /// key:字段名,value:数据
        /// </summary>
        public Dictionary<string, string> Values { get; }

        public RowDataDto(int rowNum)
        {
            RowNum = rowNum;
            Values = new Dictionary<string, string>();
        }

        /// <summary>
        /// 获取对应字段名的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (Values.ContainsKey(key))
                {
                    return Values[key];
                }
                return null;
            }
            set
            {
                if (Values.ContainsKey(key))
                {
                    Values[key] = value;
                }
                else
                {
                    Values.Add(key, value);
                }
            }
        }

    }
}
