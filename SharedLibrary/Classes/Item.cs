using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Enums;

namespace SharedLibrary.Classes
{
    public class Item
    {
        public int Id { get; }
        public int Sku { get; set; }
        public string Name { get; set; }
        public int QuantityWarehouse { get; set; }
        public int QuantityStore { get; set; }
        public CategoryEnum Category { get; set; }
        public double WholesalePrice { get; set; }
        public double SellPrice { get; set; }
        public DateTime Experationdate { get; set; }

        public Item(int id, int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice, DateTime experationdate)
        {
            Id = id;
            Sku = sku;
            Name = name;
            QuantityWarehouse = quantitywarehouse;
            QuantityStore = quantitystore;
            Category = category;
            WholesalePrice = wholesaleprice;
            SellPrice = sellprice;
            Experationdate = experationdate;
        }
    }
}
