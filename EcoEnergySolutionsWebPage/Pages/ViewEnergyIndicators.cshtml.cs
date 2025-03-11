using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EcoEnergySolutionsWebPage.Pages
{
    public class ViewEnergyIndicatorModel : PageModel
    {
        private const string CsvFilePath = "./wwwroot/Files/InfoFiles/indicadors_energetics_cat.csv";
        private const string JsonFilePath = "./wwwroot/Files/InfoFiles/indicadors_energetics_cat.json";

        public List<EnergyIndicator> records { get; set; } = new List<EnergyIndicator>();
        public List<EnergyIndicator> highNetProduction { get; set; }
        public List<EnergyIndicator> highAutoGasConsumption { get; set; }
        public Dictionary<string, double> averageNetProductionByYear { get; set; }
        public List<EnergyIndicator> highDemandLowProduction { get; set; }
        private double ParseDouble(string value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            return 0; // Default value if parsing fails
        }
        public void OnGet()
        {
            // If the JSON file does not exist, create it from the CSV file
            if (!System.IO.File.Exists(JsonFilePath))
            {
                if (System.IO.File.Exists(CsvFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(CsvFilePath);
                    var header = lines[0].Split(','); // Get the header row
                    var records = lines.Skip(1) // Skip the header
                    .Select(line => line.Split(','))
                    .Select(parts => new EnergyIndicator
                    {
                        Date = parts[0],
                        PBEE_Hidroelectr = ParseDouble(parts[1]),
                        PBEE_Carbo = ParseDouble(parts[2]),
                        PBEE_GasNat = ParseDouble(parts[3]),
                        PBEE_FuelOil = ParseDouble(parts[4]),
                        PBEE_CiclComb = ParseDouble(parts[5]),
                        PBEE_Nuclear = ParseDouble(parts[6]),
                        CDEEBC_ProdBruta = ParseDouble(parts[7]),
                        CDEEBC_ConsumAux = ParseDouble(parts[8]),
                        CDEEBC_NetProduction = ParseDouble(parts[9]),
                        CDEEBC_ConsumBomb = ParseDouble(parts[10]),
                        CDEEBC_DispProd = ParseDouble(parts[11]),
                        CDEEBC_TotVendesXarxaCentral = ParseDouble(parts[12]),
                        CDEEBC_SaldoIntercanviElectr = ParseDouble(parts[13]),
                        CDEEBC_ElectricDemand = ParseDouble(parts[14]),
                        CDEEBC_TotalEBCMercatRegulat = parts[15],
                        CDEEBC_TotalEBCMercatLliure = parts[16],
                        FEE_Industria = ParseDouble(parts[17]),
                        FEE_Terciari = ParseDouble(parts[18]),
                        FEE_Domestic = ParseDouble(parts[19]),
                        FEE_Primari = ParseDouble(parts[20]),
                        FEE_Energetic = ParseDouble(parts[21]),
                        FEEI_ConsObrPub = ParseDouble(parts[22]),
                        FEEI_SiderFoneria = ParseDouble(parts[23]),
                        FEEI_Metalurgia = ParseDouble(parts[24]),
                        FEEI_IndusVidre = ParseDouble(parts[25]),
                        FEEI_CimentsCalGuix = ParseDouble(parts[26]),
                        FEEI_AltresMatConstr = ParseDouble(parts[27]),
                        FEEI_QuimPetroquim = ParseDouble(parts[28]),
                        FEEI_ConstrMedTrans = ParseDouble(parts[29]),
                        FEEI_RestaTransforMetal = ParseDouble(parts[30]),
                        FEEI_AlimBegudaTabac = ParseDouble(parts[31]),
                        FEEI_TextilConfecCuirCalçat = ParseDouble(parts[32]),
                        FEEI_PastaPaperCartro = ParseDouble(parts[33]),
                        FEEI_AltresIndus = ParseDouble(parts[34]),
                        DGGN_PuntFrontEnagas = ParseDouble(parts[35]),
                        DGGN_DistrAlimGNL = ParseDouble(parts[36]),
                        DGGN_ConsumGNCentrTerm = ParseDouble(parts[37]),
                        CCAC_AutoGas = ParseDouble(parts[38]),
                        CCAC_GasoilA = ParseDouble(parts[39])
                    })
                    .ToList();

                    // Write the records to the JSON file
                    var json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(JsonFilePath, json);
                }
            }

            // Read the data from the JSON file
            if (System.IO.File.Exists(JsonFilePath))
            {
                var json = System.IO.File.ReadAllText(JsonFilePath);
                records = JsonSerializer.Deserialize<List<EnergyIndicator>>(json);
            }

            // Perform statistical analyses if records are available
            if (records != null && records.Any())
            {
                // Example: Find records with net production greater than 3000
                highNetProduction = records
                    .Where(r => r.CDEEBC_NetProduction > 3000)
                    .OrderBy(r => r.CDEEBC_NetProduction)
                    .ToList();

                // Example: Find records with gasoline consumption greater than 100
                highAutoGasConsumption = records
                    .Where(r => r.CCAC_AutoGas > 100)
                    .OrderByDescending(r => r.CCAC_AutoGas)
                    .ToList();

                // Example: Calculate average net production by year
                averageNetProductionByYear = records
                    .GroupBy(r => r.Date.Substring(3)) // Extract year from "MM/yyyy"
                    .ToDictionary(g => g.Key, g => g.Average(r => r.CDEEBC_NetProduction))
                    .OrderBy(kvp => kvp.Key)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                // Example: Find records with electrical demand greater than 4000 and available production less than 300
                highDemandLowProduction = records
                    .Where(r => r.CDEEBC_ElectricDemand > 4000 && r.CDEEBC_DispProd < 300)
                    .ToList();
            }
        }
    }
}
