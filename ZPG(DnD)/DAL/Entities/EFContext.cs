using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EFContext : DbContext
    {
        public EFContext() : base("ZPGConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Enemy> Enemy { get; set; }
        public DbSet<CharacterItem> CharacterItems { get; set; }
        public DbSet<EnemyItem> EnemyItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CharacterInventory> CharInventories { get; set; }
        public DbSet<EnemyInventory> EnemyInventories { get; set; }
        public DbSet<CharacterSkills> CharacterSkills { get; set; }
        public DbSet<CharacterStats> CharacterStats { get; set; }
    }
}
