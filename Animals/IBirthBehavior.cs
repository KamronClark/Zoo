using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public interface IBirthBehavior
    {
        IReproducer Reproduce(Animal animal);
    }
}
