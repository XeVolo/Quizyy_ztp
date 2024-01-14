using Quizyy_wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quizyy_wpf.Command
{
    public class FitConnectionCommand : FitCommand
    {
        private Button LeftButton;
        private Button RightButton;
        public FitConnectionCommand( Button leftbutton, Button rightbutton) 
        {
            
            LeftButton = leftbutton;
            RightButton = rightbutton;
        }
        public override void undo()
        {
            LeftButton.Visibility = Visibility.Visible;
            RightButton.Visibility = Visibility.Visible;
        }
        public override void redo()
        {
            LeftButton.Visibility = Visibility.Collapsed;
            RightButton.Visibility = Visibility.Collapsed;
        }
    }
}
