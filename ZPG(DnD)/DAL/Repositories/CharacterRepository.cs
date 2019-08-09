using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CharacterRepository: IZPGRepository<Character>
    {
        private readonly EFContext _context;
        public CharacterRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(Character elem)
        {
            try
            {
                _context.Characters.Add(elem);
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
                var character = _context.Characters.FirstOrDefault(t => t.Id == id);
                _context.Characters.Remove(character);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, Character elem)
        {
            try
            {
                var character = _context.Characters.FirstOrDefault(t => t.Id == id);
                character.Name = elem.Name;
                character.HP = elem.HP;
                character.HPMax = elem.HPMax;
                character.Intitiative = elem.Intitiative;
                character.Speed = elem.Speed;
                character.ArmorClass = elem.Speed;
                character.UserId = elem.UserId;
                character.UserOf = elem.UserOf;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Character> Get()
        {
            return _context.Characters.AsEnumerable();
        }
    }
}
