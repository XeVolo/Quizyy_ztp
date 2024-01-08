using Quizyy_wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Command
{
    public abstract class FitCommand
    {
        public FitCommand()
        {           
        }
        public abstract void undo();
        public abstract void redo();
    }
}
