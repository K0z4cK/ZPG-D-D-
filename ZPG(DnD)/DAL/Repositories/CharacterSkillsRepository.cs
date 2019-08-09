using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CharacterSkillsRepository : IZPGRepository<CharacterSkills>
    {
        private readonly EFContext _context;
        public CharacterSkillsRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(CharacterSkills elem)
        {
            try
            {
                _context.CharacterSkills.Add(elem);
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
                var skills = _context.CharacterSkills.FirstOrDefault(t => t.Id == id);
                _context.CharacterSkills.Remove(skills);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, CharacterSkills elem)
        {
            try
            {
                var skills = _context.CharacterSkills.FirstOrDefault(t => t.Id == id);
                skills.Acrobatics = elem.Acrobatics;
                skills.AnimalHandling = elem.AnimalHandling;
                skills.Athletics = elem.Athletics;
                skills.Medicine = elem.Medicine;
                skills.Persuasion = elem.Persuasion;
                skills.Religion = elem.Religion;
                skills.SleightOfHand = elem.SleightOfHand;
                skills.Stealth = elem.Stealth;
                skills.Survival = elem.Survival;
                skills.CharacterOf = elem.CharacterOf;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<CharacterSkills> Get()
        {
            return _context.CharacterSkills.AsEnumerable();
        }
    }
}
