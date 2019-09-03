using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CharacterInventoryRepository : IZPGRepository<CharacterInventory>
    {
        private readonly EFContext _context;
        public CharacterInventoryRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(CharacterInventory elem)
        {
            try
            {
                _context.CharInventories.Add(elem);
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
                var inventory = _context.CharInventories.FirstOrDefault(t => t.Id == id);
                _context.CharInventories.Remove(inventory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, CharacterInventory elem)
        {
            try
            {
                var inventory = _context.CharInventories.FirstOrDefault(t => t.Id == id);
                inventory.CharacterItems = elem.CharacterItems;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<CharacterInventory> Get()
        {
            return _context.CharInventories.AsEnumerable();
        }
    }
}
