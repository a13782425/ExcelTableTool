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
        /// ����(sheet)
        /// </summary>
        public string TableSheetName { get; set; }

        //public string TableFileName { get; set; }
        public ExcelHelper Helper { get; set; }
        /// <summary>
        /// �����ļ���
        /// </summary>
        public string DataFileName { get; set; }

        public List<PropertyDto> PropertyDtoList { get; set; }

        public string DataPath { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public string ExcelFileName { get; set; }
    }
}
