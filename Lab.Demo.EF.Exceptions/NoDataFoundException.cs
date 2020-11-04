using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Exceptions
{
    public class NoDataFoundException : Exception
    {
        public NoDataFoundException() : base("No se encontraron datos para eliminar.") { }
    }
}
