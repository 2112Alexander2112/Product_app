using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int IdCatalog {  get; set; }
        public string Name { get; set; }
        public DateTime DateTime1 { get; set; }
        public DateTime DateTime2 { get; set; }
        public int Party {  get; set; }
        public int Count { get; set; }
        public string PhotoName { get; set; }
    }
}
