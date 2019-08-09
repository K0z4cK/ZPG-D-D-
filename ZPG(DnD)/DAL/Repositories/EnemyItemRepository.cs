using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EnemyItemRepository : IZPGRepository<EnemyItem>
    {
        private readonly EFContext _context;
        public EnemyItemRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(EnemyItem elem)
        {
            try
            {
                _context.EnemyItems.Add(elem);
                _context.SaveChanges();
                return elem.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var item = _context.EnemyItems.FirstOrDefault(t => t.Id == id);
                _context.EnemyItems.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, EnemyItem elem)
        {
            try
            {
                var item = _context.EnemyItems.FirstOrDefault(t => t.Id == id);
                item.InventoryId = elem.InventoryId;
                item.InventoryOf = elem.InventoryOf;
                item.ItemId = elem.ItemId;
                item.ItemOf = elem.ItemOf;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<EnemyItem> Get()
        {
            return _context.EnemyItems.AsEnumerable();
        }
    }
}
