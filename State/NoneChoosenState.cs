using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Quizyy_wpf.State
{
    public class NoneChoosenState : State1
    {



        public override void ChooseOption(Button clickedButton)
        {
			context.concept = clickedButton.Content.ToString();
			context.conceptid = Convert.ToInt32(((object[])clickedButton.Tag)[0]);
            context.firstpicked = ((object[])clickedButton.Tag)[1].ToString();
			context.chosen1 = clickedButton;
        }
		public override void ShowChosen()
		{
			context.DisplayTextBlock1.Text = "Wybrano: " + context.concept;
			context.TransitionTo(new FirstChoosenState());
		}
		public override void ShowResult()
        {
            
            
        }

    }
}
