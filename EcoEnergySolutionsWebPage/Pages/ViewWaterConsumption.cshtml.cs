using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;

namespace EcoEnergySolutionsWebPage.Pages
{
    public class ViewWaterConsumptionModel : PageModel
    {
        private const string CsvFilePath = "./wwwroot/Files/InfoFiles/consum_aigua_cat_per_comarques.csv";
        private const string XmlFilePath = "./wwwroot/Files/InfoFiles/consum_aigua_cat_per_comarques.xml";

        public List<WaterConsumption> records = new List<WaterConsumption>();
        public List<WaterConsumption> top10Regions = new List<WaterConsumption>();
        public Dictionary<string, double> averageConsumptionByRegion = new Dictionary<string, double>();
        public List<WaterConsumption> suspiciousConsumption = new List<WaterConsumption>();
        public List<string> increasingTrendRegions = new List<string>();
        public int mostRecentYear;

        public void OnGet()
        {
            // If the XML file does not exist, create it from the CSV file
            if (!System.IO.File.Exists(XmlFilePath))
            {
                if (System.IO.File.Exists(CsvFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(CsvFilePath);
                    var records = lines.Skip(1) // Skip the header
                    .Select(line => ParseCsvLine(line))
                    .Select(parts => new WaterConsumption
                    {
                        Year = int.Parse(parts[0]),
                        RegionCode = int.Parse(parts[1]),
                        Region = parts[2].Trim('"'),
                        Population = int.Parse(parts[3]),
                        DomesticConsumption = double.Parse(parts[4]),
                        EconomicActivitiesConsumption = double.Parse(parts[5]),
                        TotalConsumption = double.Parse(parts[6]),
                        DomesticConsumptionPerCapita = double.Parse(parts[7])
                    })
                    .ToList();

                    // Helper method to parse CSV lines with quoted fields
                    string[] ParseCsvLine(string line)
                    {
                        var result = new List<string>();
                        var inQuotes = false;
                        var currentField = "";

                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] == '"')
                            {
                                inQuotes = !inQuotes; 
                            }
                            else if (line[i] == ',' && !inQuotes)
                            {
                                result.Add(currentField);
                                currentField = "";
                            }
                            else
                            {
                                currentField += line[i];
                            }
                        }

                        result.Add(currentField); 
                        return result.ToArray();
                    }

                    // Write the records to the XML file
                    var xmlSerializer = new XmlSerializer(typeof(List<WaterConsumption>));
                    using (var writer = new StreamWriter(XmlFilePath))
                    {
                        xmlSerializer.Serialize(writer, records);
                    }
                }
            }

            // Read the data from the XML file
            if (System.IO.File.Exists(XmlFilePath))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<WaterConsumption>));
                using (var reader = new StreamReader(XmlFilePath))
                {
                    records = (List<WaterConsumption>)xmlSerializer.Deserialize(reader);
                }
            }

            // Perform statistical analyses if records are available
            if (records.Any())
            {
                mostRecentYear = records.Max(r => r.Year);
                top10Regions = records
                    .Where(r => r.Year == mostRecentYear)
                    .OrderByDescending(r => r.TotalConsumption)
                    .Take(10)
                    .ToList();

                averageConsumptionByRegion = records
                    .GroupBy(r => r.Region)
                    .ToDictionary(g => g.Key, g => g.Average(r => r.TotalConsumption))
                    .OrderByDescending(kvp => kvp.Value)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                suspiciousConsumption = records
                    .Where(r => r.TotalConsumption > 999999) // Adjust threshold as needed
                    .ToList();

                increasingTrendRegions = records
                    .GroupBy(r => r.Region)
                    .Where(g => g.Count() >= 5)
                    .Select(g => new
                    {
                        Region = g.Key,
                        Trend = g.OrderBy(r => r.Year).Select(r => r.TotalConsumption).ToList()
                    })
                    .Where(g => g.Trend.Zip(g.Trend.Skip(1), (a, b) => b > a).All(x => x))
                    .Select(g => g.Region)
                    .ToList();
            }
        }
    }
}