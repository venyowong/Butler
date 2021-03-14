using Butler.Daos;
using Butler.Entities;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Butler
{
    public static class Data
    {
        public static void Init(bool async = false)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (async)
            {
                Task.Run(LoadFiles);
            }
            else
            {
                LoadFiles();
            }
        }

        private static void LoadFiles()
        {
            var files = Directory.GetFiles("data");
            if (!files.Any())
            {
                return;
            }

            foreach (var f in files)
            {
                if (!f.EndsWith(".xls") && !f.EndsWith(".xlsx") && !f.EndsWith(".xlsb"))
                {
                    continue;
                }

                var file = new FileInfo(f);
                var version = DataDao.GetFileVersion(file.FullName);
                if (version != null && file.LastWriteTime <= version.LastWriteTime)
                {
                    continue;
                }

                using (var stream = File.OpenRead(f))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var rows = result?.Tables?[0]?.Rows;
                    if (rows == null || rows.Count <= 0)
                    {
                        continue;
                    }
                    if (!IsQuotation(rows[0]))
                    {
                        continue;
                    }

                    var quotations = new List<IndexQuotation>();
                    for (int i = 1; i < rows.Count; i ++)
                    {
                        if (string.IsNullOrWhiteSpace(rows[i][0]?.ToString()))
                        {
                            break;
                        }

                        var quotation = new IndexQuotation
                        {
                            IndexCode = rows[i][0].ToString(),
                            IndexName = rows[i][1].ToString()
                        };
                        DateTime.TryParse(rows[i][2].ToString(), out DateTime date);
                        decimal.TryParse(rows[i][3].ToString().Replace(",", ""), out decimal open);
                        decimal.TryParse(rows[i][4].ToString().Replace(",", ""), out decimal max);
                        decimal.TryParse(rows[i][5].ToString().Replace(",", ""), out decimal min);
                        decimal.TryParse(rows[i][6].ToString().Replace(",", ""), out decimal close);
                        decimal.TryParse(rows[i][7].ToString().Replace(",", ""), out decimal markupAmount);
                        decimal.TryParse(rows[i][8].ToString().Replace(",", ""), out decimal markup);
                        decimal.TryParse(rows[i][9].ToString().Replace(",", ""), out decimal vol);
                        decimal.TryParse(rows[i][10].ToString().Replace(",", ""), out decimal amount);
                        quotation.Date = date;
                        quotation.Open = open;
                        quotation.Max = max;
                        quotation.Min = min;
                        quotation.Close = close;
                        quotation.MarkupAmount = markupAmount;
                        quotation.Markup = markup;
                        quotation.Volume = vol;
                        quotation.Amount = amount;
                        quotations.Add(quotation);
                    }
                    DataDao.UpdateIndexQuotations(quotations);
                }

                if (version == null)
                {
                    DataDao.AddFileVersion(new FileVersion
                    {
                        LastAccessTime = file.LastAccessTime,
                        CreationTime = file.CreationTime,
                        FullName = file.FullName,
                        LastWriteTime = file.LastWriteTime
                    });
                }
                else
                {
                    version.LastWriteTime = file.LastWriteTime;
                    version.LastAccessTime = file.LastAccessTime;
                    DataDao.UpdateFileVersion(version);
                }
            }
        }

        private static bool IsQuotation(DataRow row)
        {
            if (row.ItemArray.Length < 11)
            {
                return false;
            }
            if (row[0].ToString() == "证券代码" && row[1].ToString() == "证券名称" && row[2].ToString() == "交易时间" && 
                row[3].ToString() == "开盘价" && row[4].ToString() == "最高价" && row[5].ToString() == "最低价" && 
                row[6].ToString() == "收盘价" && row[7].ToString() == "涨跌" && row[8].ToString() == "涨跌幅%" && 
                row[9].ToString() == "成交量" && row[10].ToString() == "成交额")
            {
                return true;
            }
            return false;
        }
    }
}
