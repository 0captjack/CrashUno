using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Models
{
    public class Location
    {
        [Key]
        [Required]
        public int locid { get; set; }
        public string main_road_name { get; set; }
        public string city { get; set; }
        public string county_name { get; set; }
    }
}
