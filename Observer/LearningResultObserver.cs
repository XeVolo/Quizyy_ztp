using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Observer
{
    public class LearningResultObserver : PointsObserver
    {
        private int CurrentResult;
        public int GetPoints()
        {
            return CurrentResult;
        }
        new public void Update(int result)
        {
            CurrentResult = result;
        }
    }
}
