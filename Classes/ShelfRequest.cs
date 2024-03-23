using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    internal class ShelfRequest
    {
        public int Id { get; }
        public int ItemId { get; set; }
        public int ItemSku {  get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public ShelfRequest(int id, int itemId, int itemSku, string itemName, int quantity)
        {
            Id = id;
            ItemId = itemId;
            ItemSku = itemSku;
            ItemName = itemName;
            Quantity = quantity;
        }
    }
}
