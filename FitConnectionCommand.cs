using Quizyy_wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quizyy_wpf
{
	public class FitConnectionCommand : FitCommand
	{
		private FitView view;
		private Button LeftButton;
		private Button RightButton;
		public FitConnectionCommand(FitView fitView, Button leftbutton,Button rightbutton) : base(fitView)
		{
			this.view = fitView;
			this.LeftButton = leftbutton;
			this.RightButton = rightbutton;
		}
		public override void undo()
		{
			LeftButton.Visibility = Visibility.Visible; 
			RightButton.Visibility=Visibility.Visible;
		}
		public override void redo()
		{
			LeftButton.Visibility = Visibility.Collapsed;
			RightButton.Visibility = Visibility.Collapsed;
		}
	}
}
