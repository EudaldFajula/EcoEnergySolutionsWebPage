using EcoEnergySolutionsWebPage;
namespace EcoEnergySolutionsXUnit
{
    public class TestxUnit
    {
        [Fact]
        public void CalcularEnergiaParametreNegatiuTipusSolarRatiPositiu()
        {
            //Arrange i Act
            Simulacio result = new Simulacio {TipusEnergia = "Solar", Parametre = -1, Rati = 1 };
            //Assert
            Assert.Equal(result.CalculateTotalEnergiaGenerada(), -1);
        }
        public void CalcularEnergiaParametrePositiuTipusSolarRatiNegatiu()
        {
            //Arrange i Act
            Simulacio result = new Simulacio { TipusEnergia = "Solar", Parametre = 1, Rati = -1 };
            //Assert
            Assert.Equal(result.CalculateTotalEnergiaGenerada(), -1);
        }
        public void CalcularEnergiaParametrePositiuTipusSolarRatiPositiu()
        {
            //Arrange i Act
            Simulacio result = new Simulacio { TipusEnergia = "Solar", Parametre = 1, Rati = 1 };
            //Assert
            Assert.Equal(result.CalculateTotalEnergiaGenerada(), 1);
        }
        public void CalcularEnergiaParametreNegaiuTipusSolarRatiNegatiu()
        {
            //Arrange i Act
            Simulacio result = new Simulacio { TipusEnergia = "Solar", Parametre = -1, Rati = -1 };
            //Assert
            Assert.Equal(result.CalculateTotalEnergiaGenerada(), 1);
        }
    }
}