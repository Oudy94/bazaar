using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class ShelfRequest
    {
        public int Id { get; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public ShelfRequest(int id, int itemId, int quantity)
        {
            Id = id;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
