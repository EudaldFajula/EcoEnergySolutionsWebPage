namespace EcoEnergySolutionsWebPage
{
    public class WaterConsumption
    {
        public int Year { get; set; } // Any (Year)
        public int RegionCode { get; set; } // Codi comarca (Region Code)
        public string Region { get; set; } // Comarca (Region)
        public int Population { get; set; } // Població (Population)
        public double DomesticConsumption { get; set; } // Domèstic xarxa (Domestic Consumption)
        public double EconomicActivitiesConsumption { get; set; } // Activitats econòmiques i fonts pròpies (Economic Activities Consumption)
        public double TotalConsumption { get; set; } // Total (Total Consumption)
        public double DomesticConsumptionPerCapita { get; set; } // Consum domèstic per càpita (Domestic Consumption Per Capita)
    }
}
