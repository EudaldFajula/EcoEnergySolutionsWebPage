using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace EcoEnergyWeb
{
	public enum TipusEnergiaEnum
	{
		Solar,
		Hidroelectrica,
		Eolica
	}

	public class SistemaEnergia
	{
		public System.DateTime Data { get; set; }

		[EnumDataType(typeof(TipusEnergiaEnum))]
		public TipusEnergiaEnum TipusEnergia { get; set; }
		public int Rati { get; set; }
		public double Cost { get; set; }
		public double Preu { get; set; }
	}
}
