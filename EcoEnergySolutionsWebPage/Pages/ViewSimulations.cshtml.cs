using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EcoEnergySolutionsWebPage.Pages
{
    public class ViewSimulationsModel : PageModel
    {
        public List<Simulacio> simulacions { get; set; } = new List<Simulacio>();

        public void OnGet()
        {
            string filePath = "./wwwroot/Files/InfoFiles/Simulacions.csv";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    if (values.Length == 5)
                    {
                        simulacions.Add(new Simulacio
                        {
                            TipusEnergia = values[0],
                            Parametre = double.Parse(values[1], CultureInfo.InvariantCulture),
                            Rati = double.Parse(values[2], CultureInfo.InvariantCulture),
                            Cost = double.Parse(values[3], CultureInfo.InvariantCulture),
                            Preu = double.Parse(values[4], CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
        }
    }
}
