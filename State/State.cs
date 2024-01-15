using Quizyy_wpf.Model;
using Quizyy_wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Quizyy_wpf.State
{
    public abstract class State1
    {
        protected FitView context;      
		public void SetContext(FitView context)
        {
            this.context = context;
        }
        public abstract void ChooseOption(Button clickedButton);
        public abstract void ShowChosen();
        public abstract void ShowResult();

    }
}
