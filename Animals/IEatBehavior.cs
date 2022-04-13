using Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public interface IEatBehavior
    {
        void Eat(IEater eater, Food food);
    }
}
