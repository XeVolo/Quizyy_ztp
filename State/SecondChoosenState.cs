using Quizyy_wpf.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quizyy_wpf.State
{
	public class SecondChoosenState : State1
	{
		public override void ChooseOption(Button clickedButton)
		{
			
		}
		public override void ShowChosen()
		{
				
		}
		public override void ShowResult()
		{
			if (context.conceptid == context.definitionid)
			{
				context.DisplayTextBlock3.Text = "Połączenie poprawne";
				context.chosen1.Visibility = Visibility.Collapsed;
				context.chosen2.Visibility = Visibility.Collapsed;
				context.resoult++;
				FitConnectionCommand connection = new FitConnectionCommand(context.chosen1, context.chosen2);
				context.undoButton.IsEnabled = true;
				context.redoButton.IsEnabled = false;
				context.undoHistory.Push(connection);
				context.redoHistory.Clear();
			}
			else
			{
				context.DisplayTextBlock3.Text = "Połączenie błędne";
			}
			context.DisplayTextBlock1.Text = "";
			context.DisplayTextBlock2.Text = "";
			context.concept = null;
			context.definition = null;
			context.conceptid = null;
			context.definitionid = null;
			context.chosen1 = null;
			context.chosen2 = null;
			if (context.resoult == 7)
			{
				context.DifficultyLvlTextBlock.Text = "";
				context.DisplayTextBlock3.Text = "";
				context.resoult = 0;
				context.OpenMode();
			}
			context.TransitionTo(new NoneChoosenState());
		}
	}
}
