using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Proxy
{
	public class RealConnection : DatabaseConnection
	{		
		
		public List<FlashCardsModel> GetFlashCardsList()
		{
			using var context = new MyBaseContext();
			return context.FlashCards.ToList();

		}
		public  List<WriteModel> GetWriteList()
		{
			using (var context = new MyBaseContext())
			{
				List<WriteModel> list = context.Writes.ToList();
				return list;
			}
		}
		public void SaveFlashCards()
		{

		}

		public void SaveWriteList()
		{

		}
	}
}
