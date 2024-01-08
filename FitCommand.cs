using Quizyy_wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf
{
	public abstract class FitCommand
	{
		public FitView FitView { get; set; }

		public FitCommand(FitView fitView) 
		{ 
		this.FitView = fitView;
		}
		public abstract void undo();
		public abstract void redo();
	}
}
