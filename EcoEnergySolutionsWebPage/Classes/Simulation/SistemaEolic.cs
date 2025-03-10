using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EcoEnergyWeb
{
    public class SistemaEolic : SistemaEnergia
    {
        public double VelocitatVent { get; set; }
        public SistemaEolic(double velocitatVent, double rati, double cost, double preu)
        {
            VelocitatVent = velocitatVent;
            Cost = cost;
            Preu = preu;
            Data = DateTime.Now;
            TipusEnergia = TipusEnergiaEnum.Eolica;
        }
        public SistemaEolic() 
        {
			Data = DateTime.Now;
			TipusEnergia = TipusEnergiaEnum.Eolica;
		}
    }
}