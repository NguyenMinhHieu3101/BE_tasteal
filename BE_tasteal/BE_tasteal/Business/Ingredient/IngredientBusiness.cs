using BE_tasteal.Entity.Entity;
using OfficeOpenXml;

namespace BE_tasteal.Business.Ingredient
{
    public class IngredientBusiness : IIngredientBusiness
    {
        List<IngredientEntity> IIngredientBusiness.AddFromExel(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<IngredientEntity> entities = new List<IngredientEntity>();
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                var data = new List<string>();
                for (int row = 3; row <= rowCount; row++)
                {
                    var ingredientName = worksheet.Cells[row, 1].Value?.ToString();
                    var ingredientType = worksheet.Cells[row, 2].Value?.ToString();

                    Console.WriteLine(ingredientName + " " + ingredientType);
                }
            }
            return entities;
        }
    }
}
