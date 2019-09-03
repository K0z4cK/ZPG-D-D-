using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CharacterItemRepository : IZPGRepository<CharacterItem>
    {
        private readonly EFContext _context;
        public CharacterItemRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(CharacterItem elem)
        {
            try
            {
                _context.CharacterItems.Add(elem);
                _context.SaveChanges();
                return elem.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var item = _context.CharacterItems.FirstOrDefault(t => t.Id == id);
                _context.CharacterItems.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, CharacterItem elem)
        {
            try
            {
                var item = _context.CharacterItems.FirstOrDefault(t => t.Id == id);
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

        public IEnumerable<CharacterItem> Get()
        {
            return _context.CharacterItems.AsEnumerable();
        }
    }
}
