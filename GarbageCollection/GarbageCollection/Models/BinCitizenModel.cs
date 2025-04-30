namespace GarbageCollection.Models
{
    public class BinCitizenModel
    {
        public int Id { get; set; }
        public int IdBin { get; set; }
        public int IdCitizen { get; set; }
        public string Address { get; set; }

        public BinModel Bin { get; set; }
        public CitizenModel Citizen { get; set; }
    }
}
