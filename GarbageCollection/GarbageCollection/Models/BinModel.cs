namespace GarbageCollection.Models
{
    public class BinModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public ICollection<BinCitizenModel> BinCitizens { get; set; }
        public ICollection<CollectionModel> Collections { get; set; }
    }
}
