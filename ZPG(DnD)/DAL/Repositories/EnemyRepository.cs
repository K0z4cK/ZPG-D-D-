using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EnemyRepository : IZPGRepository<Enemy>
    {
        private readonly EFContext _context;
        public EnemyRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(Enemy elem)
        {
            try
            {
                _context.Enemy.Add(elem);
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
                var enemy = _context.Enemy.FirstOrDefault(t => t.Id == id);
                _context.Enemy.Remove(enemy);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, Enemy elem)
        {
            try
            {
                var enemy = _context.Enemy.FirstOrDefault(t => t.Id == id);
                enemy.Name = elem.Name;
                enemy.HP = elem.HP;
                enemy.HPMax = elem.HPMax;
                enemy.Intitiative = elem.Intitiative;
                enemy.Speed = elem.Speed;
                enemy.ArmorClass = elem.Speed;
                enemy.IsBoss = elem.IsBoss;
                enemy.ExpGained = elem.ExpGained;


                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Enemy> Get()
        {
            return _context.Enemy.AsEnumerable();
        }
    }
}
