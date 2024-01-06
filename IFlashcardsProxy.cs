using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf
{
    public interface IFlashCardsProxy 
    {
        string TakeConcept();
        string TakeDefinition();
        string TakeLevel();
        void Save();
    }
}
