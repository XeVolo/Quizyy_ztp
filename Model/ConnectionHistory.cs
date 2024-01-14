using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizyy_wpf.Command;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Quizyy_wpf.Model
{
    public class ConnectionHistory
	{
		private Stack<FitCommand> history = new Stack<FitCommand>();

		public void Push(FitCommand com)
		{
			history.Push(com);
		}

		public FitCommand Pop()
		{
			return history.Pop();
		}

		public void Clear()
		{
			history.Clear();
		}

		public bool IsEmpty()
		{
			return history.Count == 0;
		}
	}
}
