using System.ComponentModel.DataAnnotations;

namespace Roamler.Models
{
    public class Coordinate
    {
        private const string ErrorMessage = "{0} length must be between {1} and {2}";
        
        [Required]
        [Range(-90, 90, ErrorMessage = ErrorMessage)]
        public float Latitude { get; }

        [Required]
        [Range(-180, 180, ErrorMessage = ErrorMessage)]
        public float Longitude { get; }

        public Coordinate(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}