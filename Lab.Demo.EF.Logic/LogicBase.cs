using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    public class LogicBase
    {
        protected NorthwindContext context;

        public LogicBase()
        {
            context = new NorthwindContext();
        }

    }
}
