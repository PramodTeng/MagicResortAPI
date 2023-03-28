using MagicResort_API.Model.DTO;

namespace MagicResort_API.Data
{
    public static class ResortStore
    {
         
         public static  List<ResortDTO> ResortList = new List<ResortDTO>   {
                new ResortDTO { Id =1, Name = "Nagarkot Resort", Occupancy =4, Sqft=400 },
                new ResortDTO { Id = 2, Name = "Shree Antu Resort", Occupancy =5, Sqft=600}

            };  

    }
}
