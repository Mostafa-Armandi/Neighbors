using System.ComponentModel.DataAnnotations;
using Neighbors.Models;

namespace Neighbors.Controllers.RequestModels
{
    public class NearestLocationsRequestModel
    {
        [Required]
        public Coordinate Coordinate { get; set; }
        
        [Required]
        [Range(1,int.MaxValue)]
        public int MaxDistance { get; set; }
        
        [Required]
        [Range(1,int.MaxValue)]
        public int MaxResults { get; set; }
    }
}