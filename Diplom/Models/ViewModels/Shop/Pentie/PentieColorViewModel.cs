using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PentieColorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the penties")]
        public int PentieId { get; set; }
        public string PentieName { get; set; }
        [Required(ErrorMessage = "Enter the color of penties")]
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
