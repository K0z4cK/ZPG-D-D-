using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ItemRepository : IZPGRepository<Item>
    {
        private readonly EFContext _context;
        public ItemRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(Item elem)
        {
            try
            {
                _context.Items.Add(elem);
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
                var item = _context.Items.FirstOrDefault(t => t.Id == id);
                _context.Items.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, Item elem)
        {
            try
            {
                var item = _context.Items.FirstOrDefault(t => t.Id == id);
                item.Name = elem.Name;
                item.Description = elem.Description;
                item.CharacterItems = elem.CharacterItems;
                item.EnemyItems = elem.EnemyItems;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Item> Get()
        {
            return _context.Items.AsEnumerable();
        }
    }
}
