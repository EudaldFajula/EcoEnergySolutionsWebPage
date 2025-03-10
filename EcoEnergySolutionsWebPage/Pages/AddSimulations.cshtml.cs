using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace EcoEnergySolutionsWebPage.Pages
{
    public class AddSimulationsModel : PageModel
    {
        [BindProperty]
        public SistemaEnergia SistemaEnergia { get; set; }
        [BindProperty]
        public SistemaSolar SistemaSolar { get; set; }
        [BindProperty]
        public SistemaHidroelectric SistemaHidroelectric { get; set; }
        [BindProperty]
        public SistemaEolic SistemaEolic { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "./wwwroot/Files/InfoFiles/Simulacions.csv";
            string energyLine = "";

            switch (SistemaEnergia.TipusEnergia)
            {
                case TipusEnergiaEnum.Solar:
                    energyLine = $"{SistemaEnergia.TipusEnergia},{SistemaSolar.HoresDeSol},{SistemaEnergia.Rati},{SistemaEnergia.Cost},{SistemaEnergia.Preu}";
                    break;
                case TipusEnergiaEnum.Eolica:
                    energyLine = $"{SistemaEnergia.TipusEnergia},{SistemaEolic.VelocitatVent},{SistemaEnergia.Rati},{SistemaEnergia.Cost},{SistemaEnergia.Preu}";
                    break;
                case TipusEnergiaEnum.Hidroelectrica:
                    energyLine = $"{SistemaEnergia.TipusEnergia},{SistemaHidroelectric.CabalAigua},{SistemaEnergia.Rati},{SistemaEnergia.Cost},{SistemaEnergia.Preu}";
                    break;
            }

            System.IO.File.AppendAllText(filePath, energyLine + Environment.NewLine);
            return RedirectToPage("ViewSimulations");
        }
    }
}