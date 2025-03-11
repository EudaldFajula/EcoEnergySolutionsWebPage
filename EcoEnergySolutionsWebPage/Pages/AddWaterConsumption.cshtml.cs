using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;

namespace EcoEnergySolutionsWebPage.Pages
{
    public class AddWaterConsumptionModel : PageModel
    {
        private const string CsvFilePath = "./wwwroot/Files/InfoFiles/consum_aigua_cat_per_comarques.csv";
        private const string XmlFilePath = "./wwwroot/Files/InfoFiles/consum_aigua_cat_per_comarques.xml";

        [BindProperty]
        public int RegionCode { get; set; }

        [BindProperty]
        public string Region { get; set; }

        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public int Population { get; set; }

        [BindProperty]
        public double DomesticConsumption { get; set; }

        [BindProperty]
        public double EconomicActivitiesConsumption { get; set; }

        [BindProperty]
        public double TotalConsumption { get; set; }

        [BindProperty]
        public double DomesticConsumptionPerCapita { get; set; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            // Validate the year format
            if (Year < 2000 || Year > 2100)
            {
                Message = "Invalid year. Please enter a year between 2000 and 2100.";
                return Page();
            }

            // Save the data to a CSV file
            var csvLine = $"{Year},{RegionCode},\"{Region}\",{Population},{DomesticConsumption},{EconomicActivitiesConsumption},{TotalConsumption},{DomesticConsumptionPerCapita}";
            if (!System.IO.File.Exists(CsvFilePath))
            {
                System.IO.File.WriteAllText(CsvFilePath, "Any,Codi comarca,Comarca,Població,Domèstic xarxa,Activitats econòmiques i fonts pròpies,Total,Consum domèstic per càpita\n");
            }
            System.IO.File.AppendAllText(CsvFilePath, csvLine + "\n");

            // Save the data to an XML file
            var records = new List<WaterConsumption>();
            if (System.IO.File.Exists(XmlFilePath))
            {
                var xmlSerializerVar = new XmlSerializer(typeof(List<WaterConsumption>));
                using (var reader = new StreamReader(XmlFilePath))
                {
                    records = (List<WaterConsumption>)xmlSerializerVar.Deserialize(reader);
                }
            }

            records.Add(new WaterConsumption
            {
                Year = Year,
                RegionCode = RegionCode,
                Region = Region,
                Population = Population,
                DomesticConsumption = DomesticConsumption,
                EconomicActivitiesConsumption = EconomicActivitiesConsumption,
                TotalConsumption = TotalConsumption,
                DomesticConsumptionPerCapita = DomesticConsumptionPerCapita
            });

            var xmlSerializer = new XmlSerializer(typeof(List<WaterConsumption>));
            using (var writer = new StreamWriter(XmlFilePath))
            {
                xmlSerializer.Serialize(writer, records);
            }

            Message = "Consumption added successfully!";
            return RedirectToPage("ViewWaterConsumption");
        }
    }
}