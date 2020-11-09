using System.Collections.Generic;

namespace TableTool
{
    public class TableDto
    {
        public TableDto()
        {
            PropertyDtoList = new List<PropertyDto>();
        }
        /// <summary>
        /// 表名(sheet)
        /// </summary>
        public string TableSheetName { get; set; }

        //public string TableFileName { get; set; }
        public ExcelHelper Helper { get; set; }
        /// <summary>
        /// 数据文件名
        /// </summary>
        public string DataFileName { get; set; }

        public List<PropertyDto> PropertyDtoList { get; set; }

        public string DataPath { get; set; }

        /// <summary>
        /// 表格名称
        /// </summary>
        public string ExcelFileName { get; set; }
    }
}
