using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExchangeRate_Domains.Models
{
    public class Trade
    {
        [Key]
        public Guid Id { get; set; }
        public string BaseCurrency { get; set; }    
        public string TargetCurrency { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Amount{ get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal ConversionRate{ get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

       
        public DateTime Dateprocesed { get; set; }  

    }
}
