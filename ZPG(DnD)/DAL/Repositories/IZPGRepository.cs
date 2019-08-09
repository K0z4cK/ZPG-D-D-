using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IZPGRepository<T>
    {
        IEnumerable<T> Get();
        int Add(T elem);
        bool Delete(int id);
        bool Edit(int id, T elem);
    }
}
