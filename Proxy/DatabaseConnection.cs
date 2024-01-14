using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Proxy
{
	public interface DatabaseConnection
	{
		List<FlashCardsModel> GetFlashCardsList();

		List<WriteModel> GetWriteList();

		void SaveFlashCards();

		void SaveWriteList();

	}
}
