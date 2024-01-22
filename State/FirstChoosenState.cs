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
		private int control = 0;
        public override void ChooseOption(Button clickedButton)
        {
			if (!context.firstpicked.Equals(((object[])clickedButton.Tag)[1].ToString()))
			{
				control = 1;
				context.definition = clickedButton.Content.ToString();
				context.definitionid = Convert.ToInt32(((object[])clickedButton.Tag)[0]);
				context.chosen2 = clickedButton;
			}
        }
		public override void ShowChosen()
		{
			if (control == 1)
			{
				context.DisplayTextBlock2.Text = "Wybrano: " + context.definition;
				context.TransitionTo(new SecondChoosenState());
			}

		}
		public override void ShowResult()
        {
            
			
            
		}
    }
}
