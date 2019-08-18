using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly EFContext _context = new EFContext();
        private readonly IZPGRepository<Character> _repository;

        public CharacterService()
        {
            _repository = new CharacterRepository(_context);
        }
        public IEnumerable<CreateCharacterModel> GetCharactersByUserId(int userId)
        {
            return _repository.Get().
                  Where(t => t.UserId == userId).
                  Select(t => new CreateCharacterModel()
                  {
                      ArmorClass = t.ArmorClass,
                      Name = t.Name,
                      HP= t.HP,
                      HPMax=t.HPMax,
                      Intitiative=t.Intitiative,
                      Speed = t.Speed
                  }
              );
        }
        public bool CheckSituation()
        {
            throw new NotImplementedException();
        }

        public int Create(CreateCharacterModel character, int userId)
        {
            //string UserAnswer = 
            Random random = new Random();
            character.Name = InputBox;
            character.HPMax = random.Next(150);
            character.HP = character.HPMax;
            character.Intitiative = random.Next(-6, 10);
            character.Speed = random.Next(50);
            character.ArmorClass = random.Next(40);
            return _repository.Add(new Character()
            {
                Name = character.Name,
                HPMax = character.HPMax,
                HP = character.HP,
                Intitiative = character.Intitiative,
                Speed = character.Speed,
                ArmorClass = character.ArmorClass,
                UserId = userId

            });
        }
        public bool Die()
        {
            throw new NotImplementedException();
        }

        public bool Explore()
        {
            throw new NotImplementedException();
        }

        public bool Fight()
        {
            throw new NotImplementedException();
        }

        public bool Pick()
        {
            throw new NotImplementedException();
        }

        public bool Pray()
        {
            throw new NotImplementedException();
        }

        public bool Sneak()
        {
            throw new NotImplementedException();
        }

        public bool Steal()
        {
            throw new NotImplementedException();
        }

        public bool TryToSpeak()
        {
            throw new NotImplementedException();
        }

        public bool Walk()
        {
            throw new NotImplementedException();
        }
    }
}
