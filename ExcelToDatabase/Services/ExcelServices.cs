using ExcelToDatabase.Models;
using ExcelToDatabase.Services.Interfaces;
using OfficeOpenXml;

namespace ExcelToDatabase.Services
{
    public class ExcelServices : IExcelInterface
    {
        public List<Products> ReadXls(MemoryStream file)
        {
            var response = new List<Products>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(ExcelPackage package = new ExcelPackage(file)) 
            {
                //Worksheets são as páginas(abas) do arquivo excel, no caso planilha 1
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                //Pega a ultima coluna para definir quantas colunas o projeto tem
                int colCount = worksheet.Dimension.End.Column;

                //Pega a ultima linha para definir a quantidade de linhas do projeto
                int rowCount = worksheet.Dimension.End.Row;
                
                //Col igual a 2 para que o cabeçalho seja ignorado
                for (int row = 2; row <= rowCount; row++) 
                {
                    Products produto = new Products();
                    produto.name = worksheet.Cells[row, 1].Value.ToString();
                    produto.price = int.Parse(worksheet.Cells[row, 2].Value.ToString());
                    produto.stock = int.Parse(worksheet.Cells[row, 3].Value.ToString());

                    response.Add(produto);
                }
            }

            return response;
        }

        public MemoryStream CreateExcelFile(IEnumerable<Products> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            
            using(var excel = new ExcelPackage())
            {
                var worksheet = excel.Workbook.Worksheets.Add("Product stock");
                worksheet.TabColor = System.Drawing.Color.Green;
                worksheet.DefaultRowHeight = 12;
                worksheet.Rows.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Height = 20;
                worksheet.Row(1).Style.Font.Bold = true;

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Price";
                worksheet.Cells[1, 3].Value = "stock";

                int indice = 2;

                foreach (var item in data)
                {
                    worksheet.Cells[indice, 1].Value = item.name;
                    worksheet.Cells[indice, 2].Value = item.price;
                    worksheet.Cells[indice, 3].Value = item.stock;
                    indice++;
                }

                var stream = new MemoryStream(excel.GetAsByteArray());

                return stream;
            }
        }

        public MemoryStream CreateExcelModelFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excel = new ExcelPackage())
            {
                var worksheet = excel.Workbook.Worksheets.Add("Product stock");
                worksheet.TabColor = System.Drawing.Color.Green;
                worksheet.DefaultRowHeight = 12;
                worksheet.Rows.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Height = 20;
                worksheet.Row(1).Style.Font.Bold = true;

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Price";
                worksheet.Cells[1, 3].Value = "stock";

                var stream = new MemoryStream(excel.GetAsByteArray());

                return stream;
            }
        }
    }
}
