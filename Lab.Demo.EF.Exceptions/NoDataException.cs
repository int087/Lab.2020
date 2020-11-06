using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Exceptions
{
    public class NoDataException : Exception
    {
        public NoDataException() : base("No se encontraron datos.") { }
        public NoDataException(string entity) : base(("No se encontraron datos para {0}.", entity).ToString()) { }
    }
}
