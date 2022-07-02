using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsimTescil.Models
{
    public  class Sepet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SepetId { get; set; }
        public int? Id { get; set; }
        public string UrunAdi { get; set; }
        public int? Fiyat { get; set; }
        public int? Adet { get; set; }
        public DateTime? Date { get; set; }
        
    }
}
