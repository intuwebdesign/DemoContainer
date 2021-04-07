using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using ExcelDataReader;

namespace Web.Controller.Helpers
{
    public interface IExtractAndBulkInsert
    {
        bool ReadExcelFile(string pathToFile);
    }

    public class ExtractAndBulkInsert : IExtractAndBulkInsert
    {
        public bool ReadExcelFile(string pathToFile)
        {
            string zipFilePath      = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            string extractImagesTo  = HttpContext.Current.Server.MapPath("~/UploadedFiles");

            string pathToExcelFileOnServer = string.Empty;

            using (ZipArchive archive = ZipFile.OpenRead(pathToFile))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith("xls", StringComparison.OrdinalIgnoreCase))
                    {
                        if (File.Exists(Path.Combine(zipFilePath, entry.Name)))
                        {
                            File.Delete(Path.Combine(zipFilePath, entry.Name));
                        }

                        pathToExcelFileOnServer = Path.Combine(zipFilePath, entry.Name);
                        entry.ExtractToFile(Path.Combine(zipFilePath, entry.Name));
                    }
                    else
                    {
                        if (!entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) && !entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)) continue;
                        if (File.Exists(Path.Combine(extractImagesTo, entry.Name)))
                        {
                            File.Delete(Path.Combine(extractImagesTo, entry.Name));
                        }
                        entry.ExtractToFile(Path.Combine(extractImagesTo, entry.Name));
                    }
                }
            }

            if (!string.IsNullOrEmpty(pathToExcelFileOnServer))
            {
                using (var stream = File.Open(pathToExcelFileOnServer, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        var dt = new DataTable();
                        dt.Columns.Add("productID");
                        dt.Columns.Add("title");
                        dt.Columns.Add("description");
                        dt.Columns.Add("price");
                        dt.Columns.Add("image");

                        foreach (DataTable table in result.Tables)
                        {
                            foreach (DataRow dr in table.Rows.Cast<DataRow>().Skip(1)) //Skipping header
                            {
                                dt.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                            }
                        }


                        using (var sqlBulkCopy = new SqlBulkCopy(DbConnectionAbstractClass.ConnectionString))
                        {
                            sqlBulkCopy.BatchSize = 5000;
                            sqlBulkCopy.DestinationTableName = "dbo.Products";

                            sqlBulkCopy.ColumnMappings.Add("productId", "productID");
                            sqlBulkCopy.ColumnMappings.Add("title", "title");
                            sqlBulkCopy.ColumnMappings.Add("description", "description");
                            sqlBulkCopy.ColumnMappings.Add("price", "price");
                            sqlBulkCopy.ColumnMappings.Add("image", "imageName");

                            sqlBulkCopy.WriteToServer(dt);
                        }
                    }
                }

                return true;
            }
            return false;
        }
    }
}
