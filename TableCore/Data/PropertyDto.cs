using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    public sealed class PropertyDto
    {

        public PropertyDto()
        {
            PropertyName = "";
            //HumpName = "";
            PropertyType = "";
            Des = "";
            //IsArray = false;
            //EnumType = "";
            //IsEnum = false;
        }
        /// <summary>
        /// 转化前名字
        /// </summary>
        public string PropertyName { get; set; }
        ///// <summary>
        ///// 转化后的名字
        ///// </summary>
        //public string HumpName { get; set; }
        /// <summary>
        /// 表各种的索引列(非连续的)
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 当前平台真实的索引
        /// </summary>
        public int RealIndex { get; set; }
        public string PropertyType { get; set; }

        //public string EnumType { get; set; }

        //public bool IsArray { get; set; }

        public string Des { get; set; }

        //public bool IsEnum { get; set; }
    }
}
