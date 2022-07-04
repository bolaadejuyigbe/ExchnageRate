using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeRate_Domains.Models
{
    public class ExchangeRate
    {
        [Key]
        public Guid Id { get; set; }

       

        public string? Base { get; set; }
        public string? Symbol { get; set; }

            public bool success { get; set; }
            public int timestamp { get; set; }
           
            public string? date { get; set; }

        public string UserId { get; set; }
        public DateTime DateProcessed { get; set; } 
     
        public string? Currency { get; set; }
        [Column(TypeName="decimal(18, 6)")]
        public decimal Rates { get; set; }
  

    }
}
