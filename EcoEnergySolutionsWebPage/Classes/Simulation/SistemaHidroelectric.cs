using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EcoEnergyWeb
{
    public class SistemaHidroelectric : SistemaEnergia
    {
        public double CabalAigua { get; set; }
        public SistemaHidroelectric(double cabalAigua, double rati, double cost, double preu)
        {
            CabalAigua = cabalAigua;
			
			Cost = cost;
			Preu = preu;
			Data = DateTime.Now;
            TipusEnergia = TipusEnergiaEnum.Hidroelectrica;

		}
        public SistemaHidroelectric() 
        {
			Data = DateTime.Now;
			TipusEnergia = TipusEnergiaEnum.Hidroelectrica;
		}
    }
}