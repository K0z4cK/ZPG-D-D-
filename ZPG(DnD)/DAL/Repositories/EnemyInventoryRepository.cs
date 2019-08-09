using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EnemyInventoryRepository : IZPGRepository<EnemyInventory>
    {
        private readonly EFContext _context;
        public EnemyInventoryRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(EnemyInventory elem)
        {
            try
            {
                _context.EnemyInventories.Add(elem);
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
                var inventory = _context.EnemyInventories.FirstOrDefault(t => t.Id == id);
                _context.EnemyInventories.Remove(inventory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, EnemyInventory elem)
        {
            try
            {
                var inventory = _context.EnemyInventories.FirstOrDefault(t => t.Id == id);
                inventory.EnemyItems = elem.EnemyItems;
                inventory.EnemyOf = elem.EnemyOf;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<EnemyInventory> Get()
        {
            return _context.EnemyInventories.AsEnumerable();
        }
    }
}
