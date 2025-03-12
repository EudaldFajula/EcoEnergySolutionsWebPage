namespace EcoEnergySolutionsWebPage
{
    public class Simulacio
    {
        public string TipusEnergia { get; set; }
        public double Parametre { get; set; } // HoresDeSol, CabalAigua, or VelocitatVent
        public double Rati { get; set; }
        public double Cost { get; set; }
        public double Preu { get; set; }

        public double CalculateTotalEnergiaGenerada()
        {
            switch (TipusEnergia)
            {
                case "Solar":
                    return Parametre * Rati;
                case "Eolica":
                    return Math.Pow(Parametre, 3) * Rati;
                case "Hidroelectrica":
                    return Parametre * 9.8 * Rati;
                default:
                    return Parametre;
            }
        }
        public double CalculateTotalCost() => CalculateTotalEnergiaGenerada() * Cost;
        public double CalculateTotalPreu() => CalculateTotalEnergiaGenerada() * Preu;
    }
}
