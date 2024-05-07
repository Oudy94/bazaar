﻿using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Classes
{
    public class ShelfRequest
    {
        public int Id { get; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public ShelfRequestType Type { get; set; }

        public ShelfRequest(int id, int itemId, int quantity, ShelfRequestType type)
        {
            Id = id;
            ItemId = itemId;
            Quantity = quantity;
            Type = type;
		}
    }
}
