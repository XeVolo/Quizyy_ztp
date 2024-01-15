using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Quizyy_wpf.State
{
    public class FirstChoosenState : State1
    {
        public override void ChooseOption(Button clickedButton)
        {
			context.definition = clickedButton.Content.ToString();
            context.definitionid = Convert.ToInt32(clickedButton.Tag);
			context.chosen2 = clickedButton;
        }
		public override void ShowChosen()
		{
			context.DisplayTextBlock2.Text = "Wybrano: " + context.definition;
			context.TransitionTo(new SecondChoosenState());

		}
		public override void ShowResult()
        {
            
			
            
		}
    }
}
