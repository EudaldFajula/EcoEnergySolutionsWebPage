using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Text.Json;

namespace EcoEnergySolutionsWebPage.Pages
{
    public class AddEnergyIndicatorModel : PageModel
    {
        private const string CsvFilePath = "./wwwroot/Files/InfoFiles/indicadors_energetics_cat.csv";
        private const string JsonFilePath = "./wwwroot/Files/InfoFiles/indicadors_energetics_cat.json";

        [BindProperty]
        public string Date { get; set; }

        [BindProperty]
        public double CDEEBC_NetProduction { get; set; }

        [BindProperty]
        public double CCAC_AutoGas { get; set; }

        [BindProperty]
        public double CDEEBC_ElectricDemand { get; set; }

        [BindProperty]
        public double CDEEBC_DispProd { get; set; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            // Validate the date format (MM/yyyy)
            if (!DateTime.TryParseExact(Date, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                Message = "Invalid date format. Please use MM/yyyy.";
                return Page(); // Return the same page with an error message
            }

            // Validate obligatory fields
            if (CDEEBC_NetProduction < 0 || CCAC_AutoGas < 0 || CDEEBC_ElectricDemand < 0 || CDEEBC_DispProd < 0)
            {
                Message = "All fields must have non-negative values.";
                return Page(); // Return the same page with an error message
            }

            // Save the data to a CSV file
            var csvLine = $"{Date},{CDEEBC_NetProduction},{CCAC_AutoGas},{CDEEBC_ElectricDemand},{CDEEBC_DispProd}";
            if (!System.IO.File.Exists(CsvFilePath))
            {
                // Add header if the file doesn't exist
                System.IO.File.WriteAllText(CsvFilePath, "Date,CDEEBC_NetProduction,CCAC_AutoGas,CDEEBC_ElectricDemand,CDEEBC_DispProd\n");
            }
            System.IO.File.AppendAllText(CsvFilePath, csvLine + "\n");

            // Save the data to a JSON file with default values for non-obligatory fields
            var newRecord = new EnergyIndicator
            {
                Date = Date,
                CDEEBC_NetProduction = CDEEBC_NetProduction,
                CCAC_AutoGas = CCAC_AutoGas,
                CDEEBC_ElectricDemand = CDEEBC_ElectricDemand,
                CDEEBC_DispProd = CDEEBC_DispProd
            };

            List<EnergyIndicator> records;

            if (System.IO.File.Exists(JsonFilePath))
            {
                // Read existing JSON data
                var jsonData = System.IO.File.ReadAllText(JsonFilePath);
                records = JsonSerializer.Deserialize<List<EnergyIndicator>>(jsonData);
            }
            else
            {
                // Create a new list if the file doesn't exist
                records = new List<EnergyIndicator>();
            }

            // Add the new record
            records.Add(newRecord);

            // Save the updated JSON data
            var updatedJsonData = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(JsonFilePath, updatedJsonData);

            
            return RedirectToPage("ViewEnergyIndicators");
        }
    }

    
}