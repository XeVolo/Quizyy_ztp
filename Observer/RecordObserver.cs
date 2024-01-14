using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Observer
{
    public class RecordObserver : PointsObserver
    { 
        private int BestResult;
        public int GetPoints()
        {
            return BestResult;
        }
        new public void Update(int result)
        {
            if(BestResult<result)
                BestResult = result;
        }
    }
}
