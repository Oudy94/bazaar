using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    internal class StockManager
    {
        public List<Item> itemList = new List<Item>();
        public List<ShelfRequest> shelfRequests = new List<ShelfRequest>();

        public int Idcounter = 1;
        public int IdCounterShelf = 1;
        public void AddNewItem(int sku, string name, int quantitywarehouse, int quantitystore, string category, double wholesaleprice, double sellprice)
        {
            int id = Idcounter;
            Idcounter += 1;
            itemList.Add(new Item(id, sku, name, quantitywarehouse, quantitystore, category, wholesaleprice, sellprice));
        }

        public void EditItem(int id, int sku, string name, int quantitywarehouse, int quantitystore, string category, double wholesaleprice, double sellprice)
        {
            foreach (Item item in itemList)
            {
                if (item.Id == id)
                {
                    item.Sku = sku;
                    item.Name = name;
                    item.QuantityWarehouse = quantitywarehouse;
                    item.QuantityStore = quantitystore;
                    item.Category = category;
                    item.WholesalePrice = wholesaleprice;
                    item.SellPrice = sellprice;
                }
            }
        }

        public void RemoveItem(int id)
        {
            itemList.RemoveAll(x => x.Id == id);
        }

        public Item GetItemById(int id)
        {
            Item item = itemList.First(x => x.Id == id);
            return item;
        }

        public void AddShelfRequest(int itemId, int quantity)
        {
            int id = IdCounterShelf;
            IdCounterShelf += 1;
            Item item = GetItemById(itemId);
            int itemSku = item.Sku;
            string itemName = item.Name;
            shelfRequests.Add(new ShelfRequest(id, itemId, itemSku, itemName, quantity));
        }

        public void EditShelfRequest(int id, int quantity)
        {
            foreach (ShelfRequest shelfRequest in shelfRequests)
            {
                if (shelfRequest.Id == id)
                {
                    shelfRequest.Quantity = quantity;
                }
            }
        }

        public void FulFillShelfRequest(int id)
        {
            foreach (ShelfRequest shelfRequest in shelfRequests)
            {
                if (shelfRequest.Id == id)
                {
                    foreach(Item item in itemList)
                    {
                        if (item.Id == shelfRequest.ItemId)
                        {
                            item.QuantityStore += shelfRequest.Quantity;
                        }
                    }
                }
            }
            shelfRequests.RemoveAll(x => x.Id == id);
        }
    }
}
