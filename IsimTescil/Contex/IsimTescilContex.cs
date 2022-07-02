using IsimTescil.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IsimTescil.Contex
{
    public class IsimTescilContex : DbContext
    {
        public IsimTescilContex() : base("IsimTescilDB")
        {
        }
        public DbSet<Urunler> Urunler { get; set; }
        public DbSet<Sepet> Sepet { get; set; }
    }
}