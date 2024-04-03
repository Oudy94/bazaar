using TheSandwichMakersHardwareStoreSolution.Enums;
using TheSandwichMakersHardwareStoreSolution.Helpers;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class StockManager
    {
        private readonly DatabaseHelper _dbHelper;

        public int Idcounter = 1;
        public int IdCounterShelf = 1;

        public StockManager()
        {
            this._dbHelper = new DatabaseHelper();
        }

        public List<Item> GetItems()
        {
            List<Item> items = new List<Item>();

            try
            {
                _dbHelper.OpenConnection();
                items = _dbHelper.GetItemsFromDB();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return items;
        }

        public List<ShelfRequest> GetShelfRequests()
        {
            List<ShelfRequest> shelfRequests = new List<ShelfRequest>();

            try
            {
                _dbHelper.OpenConnection();
                shelfRequests = _dbHelper.GetShelfRequestFromDB();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shelfRequests;
        }

        public void AddNewItem(int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddItemToDB(sku, name, quantitywarehouse, quantitystore, category, wholesaleprice, sellprice);
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
                _dbHelper.OpenConnection();
                _dbHelper.UpdateItemInDB(id, sku, name, quantitywarehouse, quantitystore, category, wholesaleprice, sellprice);
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

        public void RemoveItem(int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.RemoveItemFromDB(id);
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
            Item item = null;
            try
            {
                _dbHelper.OpenConnection();
                item = _dbHelper.GetItemByIdFromDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return item;
        }

        public void AddShelfRequest(int itemId, int quantity)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddShelfRequestToDB(itemId, quantity);

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

        public void EditShelfRequest(int id, int itemId, int quantity)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.UpdateShelfRequest(id, itemId, quantity);
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

        public void FulFillShelfRequest(int id)
        {
            try
            {
                //TO-DO: deduct quantity item from warehouse
                _dbHelper.OpenConnection();
                _dbHelper.FulFillShelfRequestInDB(id);
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
