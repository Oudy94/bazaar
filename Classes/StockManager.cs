using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheSandwichMakersHardwareStoreSolution.Enums;
using TheSandwichMakersHardwareStoreSolution.Helpers;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    internal class StockManager
    {
        public List<Item> itemList;
        public List<ShelfRequest> shelfRequests;

        private readonly DatabaseHelper _dbHelper;

        public int Idcounter = 1;
        public int IdCounterShelf = 1;

        public StockManager()
        {
            itemList = new List<Item>();
            shelfRequests = new List<ShelfRequest>();

            this._dbHelper = new DatabaseHelper();
        }

        public void LoadItemsFromDatabase()
        {
            try
            {
                _dbHelper.OpenConnection();
                itemList = _dbHelper.RetrieveItems();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void LoadShelfRequestFromDatabase()
        {
            try
            {
                _dbHelper.OpenConnection();
                shelfRequests = _dbHelper.RetrieveShelfRequests();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void AddNewItem(int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice)
        {
            try
            {
                Item item = new Item(sku, name, quantitywarehouse, quantitystore, category, wholesaleprice, sellprice);
                itemList.Add(item);

                _dbHelper.OpenConnection();
                _dbHelper.AddItem(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void EditItem(int id, int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice)
        {
            try
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

                        _dbHelper.OpenConnection();
                        _dbHelper.UpdateItem(item);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

        }

        public void RemoveItem(int id)
        {
            try
            {
                itemList.RemoveAll(x => x.Id == id);

                _dbHelper.OpenConnection();
                _dbHelper.RemoveItem(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public Item GetItemById(int id)
        {
            Item item = itemList.First(x => x.Id == id);
            return item;
        }

        public void AddShelfRequest(int itemId, int quantity)
        {
            try
            {
                int id = IdCounterShelf;
                IdCounterShelf += 1;
                ShelfRequest shelfRequest = new ShelfRequest(id, itemId, quantity);
                shelfRequests.Add(shelfRequest);

                _dbHelper.OpenConnection();
                _dbHelper.AddShelfRequest(shelfRequest);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void EditShelfRequest(int id, int quantity)
        {
            try
            {
                foreach (ShelfRequest shelfRequest in shelfRequests)
                {
                    if (shelfRequest.Id == id)
                    {
                        shelfRequest.Quantity = quantity;

                        _dbHelper.OpenConnection();
                        _dbHelper.UpdateShelfRequest(shelfRequest);
                    }
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void FulFillShelfRequest(int id)
        {
            try
            {
                foreach (ShelfRequest shelfRequest in shelfRequests)
                {
                    if (shelfRequest.Id == id)
                    {
                        foreach (Item item in itemList)
                        {
                            if (item.Id == shelfRequest.ItemId)
                            {
                                item.QuantityStore += shelfRequest.Quantity;
                                //TO-DO: deduct quantity item from warehouse

                                _dbHelper.OpenConnection();
                                _dbHelper.RemoveShelfRequest(id);
                            }
                        }
                    }
                }
                shelfRequests.RemoveAll(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }
    }
}
