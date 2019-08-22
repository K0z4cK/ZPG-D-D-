using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CharacterStatsRepository : IZPGRepository<CharacterStats>
    {
        private readonly EFContext _context;
        public CharacterStatsRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(CharacterStats elem)
        {
            try
            {
                _context.CharacterStats.Add(elem);
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
                var stats = _context.CharacterStats.FirstOrDefault(t => t.Id == id);
                _context.CharacterStats.Remove(stats);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, CharacterStats elem)
        {
            try
            {
                var stats = _context.CharacterStats.FirstOrDefault(t => t.Id == id);
                stats.Charisma = elem.Charisma;
                stats.Constitution = elem.Constitution;
                stats.Dexterity = elem.Dexterity;
                stats.Intelligence = elem.Intelligence;
                stats.Strength = elem.Strength;
                stats.Wisdom = elem.Wisdom;
                stats.CharacterOf = elem.CharacterOf;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<CharacterStats> Get()
        {
            return _context.CharacterStats.AsEnumerable();
        }
    }
}
