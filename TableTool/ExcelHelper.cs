using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static TableCore.GlobalConst;

namespace TableTool
{
    public class ExcelHelper
    {
        private ISheet _excelSheet;
        private string _sheetName;

        /// <summary>
        /// 注释
        /// </summary>
        public string Comments { get; private set; }

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
                            string sheetName = "";
                            string comment = "";
                            bool isComment = false;
                            foreach (var item in sheet.SheetName)
                            {
                                if (!isComment)
                                {
                                    if (item == '#')
                                    {
                                        isComment = true;
                                    }
                                    else
                                    {
                                        sheetName += item;
                                    }
                                }
                                else
                                {
                                    comment += item;
                                }
                            }
                            if (isRemark(sheetName))
                            {
                                continue;
                            }
                            ExcelHelper exHelper = new ExcelHelper(sheet, sheetName);
                            exHelper.Comments = comment;
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
        /// <summary>
        /// 判断一个sheet名是否是注释 
        /// </summary>
        /// <returns></returns>
        private static bool isRemark(string sheetName)
        {
            if (string.IsNullOrWhiteSpace(sheetName))
            {
                //如果sheet名是空则是注释
                return true;
            }
            if (int.TryParse(sheetName, out int num1))
            {
                //如果全是数字则是注释
                return true;
            }
            if (int.TryParse(sheetName[0].ToString(), out int num2))
            {
                //如果首字母是数字则是注释
                return true;
            }

            foreach (var item in sheetName)
            {
                if (isChinese(item))
                {
                    return true;
                }
            }

            return false;

            bool isChinese(char ch) => ch >= 127;
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
