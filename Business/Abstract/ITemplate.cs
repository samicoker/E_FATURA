using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Abstract
{
    public interface ITemplate<T> where T : class
    {
        public abstract XElement Run(T model);
    }
}
