using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    public class TableDto
    {
        public TableDto()
        {
            PropertyDic = new Dictionary<string, PropertyDto>();
            Rows = new List<RowDataDto>();
        }
        /// <summary>
        /// 表原名
        /// </summary>
        public string OriginSheetName { get; set; }
        /// <summary>
        /// 表名(sheet)处理后的
        /// </summary>
        public string TableSheetName { get; set; }
        /// <summary>
        /// 数据文件名
        /// </summary>
        public string DataFileName { get; set; }

        public Dictionary<string, PropertyDto> PropertyDic { get; private set; }

        public List<RowDataDto> Rows { get; private set; }


        public string DataPath { get; set; }

        /// <summary>
        /// 表格名称
        /// </summary>
        public string ExcelFileName { get; set; }
    }
}
