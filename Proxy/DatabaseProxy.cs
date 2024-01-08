using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Proxy
{
	public class DatabaseProxy : DatabaseConnection
	{
		private RealConnection realConnection;
		private List<FlashCardsModel>? flashcardslist;
		private List<WriteModel>? writelist;

		public  DatabaseProxy(RealConnection realConnection)
		{
			this.realConnection = realConnection;
		}

		public List<FlashCardsModel> GetFlashCardsList()
		{
			if (flashcardslist == null)
			{
				flashcardslist=realConnection.GetFlashCardsList();
			}
			return flashcardslist;
		}
		public List<WriteModel> GetWriteList()
		{
			if (writelist == null)
			{
				writelist=realConnection.GetWriteList();
			}
			return writelist;
		}
		public void SaveFlashCards()
		{

		}

		public void SaveWriteList()
		{

		}
	}
}
