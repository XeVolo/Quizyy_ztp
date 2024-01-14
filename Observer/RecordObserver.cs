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
        public override int GetPoints()
        {
            return BestResult;
        }
        public override void Update(int result)
        {
            if(BestResult<result)
                BestResult = result;
        }
    }
}
