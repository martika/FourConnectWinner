
using System.ComponentModel.DataAnnotations;

namespace ConnectFourWinner.Domain.Entities.Models
{
    public class InputModel
    {
        [Required]
        public string Input { get; set; }        
        [Required]
        public int Width { get; set; }
        [Required]
        public int Height { get; set; }
    }
}
