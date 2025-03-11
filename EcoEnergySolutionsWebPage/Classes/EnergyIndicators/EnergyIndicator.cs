namespace EcoEnergySolutionsWebPage
{
    public class EnergyIndicator
    {
        public string Date { get; set; }
        public double CDEEBC_NetProduction { get; set; }
        public double CCAC_AutoGas { get; set; }
        public double CDEEBC_ElectricDemand { get; set; }
        public double CDEEBC_DispProd { get; set; }

        // Default values for non-obligatory fields
        public double? PBEE_Hidroelectr { get; set; } = 0;
        public double? PBEE_Carbo { get; set; } = 0;
        public double? PBEE_GasNat { get; set; } = 0;
        public double? PBEE_FuelOil { get; set; } = 0;
        public double? PBEE_CiclComb { get; set; } = 0;
        public double? PBEE_Nuclear { get; set; } = 0;
        public double? CDEEBC_ProdBruta { get; set; } = 0;
        public double? CDEEBC_ConsumAux { get; set; } = 0;
        public double? CDEEBC_ConsumBomb { get; set; } = 0;
        public double? CDEEBC_TotVendesXarxaCentral { get; set; } = 0;
        public double? CDEEBC_SaldoIntercanviElectr { get; set; } = 0;
        public double? CDEEBC_TotalEBCMercatRegulat { get; set; } = 0;
        public double? CDEEBC_TotalEBCMercatLliure { get; set; } = 0;
        public double? FEE_Industria { get; set; } = 0;
        public double? FEE_Terciari { get; set; } = 0;
        public double? FEE_Domestic { get; set; } = 0;
        public double? FEE_Primari { get; set; } = 0;
        public double? FEE_Energetic { get; set; } = 0;
        public double? FEEI_ConsObrPub { get; set; } = 0;
        public double? FEEI_SiderFoneria { get; set; } = 0;
        public double? FEEI_Metalurgia { get; set; } = 0;
        public double? FEEI_IndusVidre { get; set; } = 0;
        public double? FEEI_CimentsCalGuix { get; set; } = 0;
        public double? FEEI_AltresMatConstr { get; set; } = 0;
        public double? FEEI_QuimPetroquim { get; set; } = 0;
        public double? FEEI_ConstrMedTrans { get; set; } = 0;
        public double? FEEI_RestaTransforMetal { get; set; } = 0;
        public double? FEEI_AlimBegudaTabac { get; set; } = 0;
        public double? FEEI_TextilConfecCuirCalçat { get; set; } = 0;
        public double? FEEI_PastaPaperCartro { get; set; } = 0;
        public double? FEEI_AltresIndus { get; set; } = 0;
        public double? DGGN_PuntFrontEnagas { get; set; } = 0;
        public double? DGGN_DistrAlimGNL { get; set; } = 0;
        public double? DGGN_ConsumGNCentrTerm { get; set; } = 0;
        public double? CCAC_GasoilA { get; set; } = 0;
    }
}
