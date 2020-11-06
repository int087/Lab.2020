using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    interface ILogic<T>
    {
        List<T> GetAll();
        T GetOne(int id);
        T Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
