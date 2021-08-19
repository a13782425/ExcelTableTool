using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static TableTool.GlobalConst;

namespace TableTool
{
    public class ExcelHelper
    {
        private ISheet _excelSheet;
        private string _sheetName;

        public string TableName => _sheetName;
        private int _index = 0;

        public int Index
        {
            get
            {
                return _index;
            }
            private set
            {
                _index = value;
            }
        }

        public static string GetCellValue(ICell cell)
        {
            if (cell != null)
            {
                if (cell.CellType == CellType.Formula)
                {
                    cell.SetCellType(CellType.String);
                    return cell.StringCellValue;
                }
                else
                {
                    return cell.ToString();
                }
            }
            return "";
        }

        public static List<ExcelHelper> GetAllHelper(string path)
        {
            if (File.Exists(path))
            {
                List<ExcelHelper> list = new List<ExcelHelper>();
                using (FileStream @is = File.OpenRead(path))
                {
                    XSSFWorkbook xSSFWorkbook = new XSSFWorkbook((Stream)@is);

                    if (Params[CONSOLE_ALL_SHEET] == "1")
                    {
                        for (int i = 0; i < xSSFWorkbook.Count; i++)
                        {
                            ISheet sheet = xSSFWorkbook.GetSheetAt(i);
                            if (int.TryParse(sheet.SheetName, out int num))
                            {
                                continue;
                            }
                            ExcelHelper exHelper = new ExcelHelper(sheet, sheet.SheetName);
                            list.Add(exHelper);
                        }
                    }
                    else
                    {
                        ISheet sheet = xSSFWorkbook.GetSheet("data");
                        if (sheet == null)
                        {
                            sheet = xSSFWorkbook.GetSheetAt(0);
                        }
                        ExcelHelper exHelper = new ExcelHelper(sheet, "");
                        list.Add(exHelper);
                    }
                }
                return list;
            }
            throw new Exception(path + "  不存在");
        }

        private ExcelHelper(ISheet sheet, string sheetName)
        {
            _excelSheet = sheet;
            _sheetName = sheetName;
            Index = 0;
        }
        public ExcelHelper(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream @is = File.OpenRead(path))
                {
                    XSSFWorkbook xSSFWorkbook = new XSSFWorkbook((Stream)@is);
                    _excelSheet = xSSFWorkbook.GetSheet("data");
                    if (_excelSheet == null)
                    {
                        _excelSheet = xSSFWorkbook.GetSheetAt(0);
                    }
                    Index = 0;
                }
                return;
            }
            throw new Exception(path + "  不存在");
        }

        public bool HaveData()
        {
            return Index <= _excelSheet.LastRowNum;
        }

        public IRow GetNextRow()
        {
            if (Index <= _excelSheet.LastRowNum)
            {
                IRow row = _excelSheet.GetRow(Index);
                Index++;
                return row;
            }
            return null;
        }
    }
}
