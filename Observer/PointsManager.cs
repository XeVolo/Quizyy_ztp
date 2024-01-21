using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Observer
{
    public class PointsManager
    {
        private int Points=0;
        public List<PointsObserver> Observers = new List<PointsObserver>();

        public PointsManager()
        {
            RecordObserver recordObserver = new RecordObserver();
            AddObserver(recordObserver);
            LearningResultObserver learningResultObserver = new LearningResultObserver();
            AddObserver(learningResultObserver);
        }

        public void AddObserver(PointsObserver observer)
        {
            Observers.Add(observer);
        }
        public void Notify()
        {
            foreach(PointsObserver observer in Observers) 
            {
                observer.Update(Points);
            }
        }
        public int Show(int x)
        {
            return Observers[x].GetPoints();      
        }
        public void IncreasePoints()
        {
            Points++;
            Notify();
        }
        public void ResetPoints()
        {
            Points = 0;
            Notify();
        }
    }
}
