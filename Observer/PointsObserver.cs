using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Observer
{
    public class PointsObserver : IObserver
    {
        public virtual void Update(int Points)
        {

        }
        public virtual int GetPoints()
        {
            return 0;
        }
    }
}
