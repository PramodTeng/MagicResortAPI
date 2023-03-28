namespace MagicResort_API.Model
{
    public class Resort
    {

        public int Id { get; set; }
        public string Name { get; set; }
        
        public int Occupancy { get; set; }  

        public int Sqft { get; set; }   
        public DateTime CreatedDate { get; set; }   
    }

}
