using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCascadingDropDowns.Models
{
    public class SelectedCityState
    {
        [Key]
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
