using SharedLibrary.Enums;
using SharedLibrary.Helpers;

namespace SharedLibrary.Classes
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

        public List<ShelfRequest> GetShelfRequests(ShelfRequestType? type = null)
        {
            List<ShelfRequest> shelfRequests = new List<ShelfRequest>();

            try
            {
                _dbHelper.OpenConnection();
                shelfRequests = _dbHelper.GetShelfRequestFromDB(type);
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

        public void AddNewItem(int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice, DateTime experationdate)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddItemToDB(sku, name, quantitywarehouse, quantitystore, category, wholesaleprice, sellprice, experationdate);
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

        public void EditItem(int id, int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice, DateTime experationdate)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.UpdateItemInDB(id, sku, name, quantitywarehouse, quantitystore, category, wholesaleprice, sellprice, experationdate);
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

        public void AddShelfRequest(int itemId, int quantity, ShelfRequestType type)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddShelfRequestToDB(itemId, quantity, type);

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

        public void EditShelfRequest(int id, int itemId, int quantity, ShelfRequestType type)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.UpdateShelfRequest(id, itemId, quantity, type);
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

        public bool DataBaseContainsId(int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                if (_dbHelper.ListItemIdInDatabase().Contains(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }

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

        public bool ItemNameExists(string name)
        {
            try
            {
                _dbHelper.OpenConnection();
                if (_dbHelper.ItemNameExistsInDB(name))
                {
                    return true;
                }
                else
                {
                    return false;
                }

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
