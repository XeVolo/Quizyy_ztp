using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Proxy
{
	public interface IDatabaseConnection
	{
		List<FlashCardsModel> GetFlashCardsList();

		List<WriteModel> GetWriteList();

		void SaveFlashCards(string _concept, string _definition, string _difficultylvl);
		 
		void SaveWriteList(string _question, string _answer, string _incorrectans1, string _incorrectans2, string _incorrectans3, string _difficultylvl);

	}
}
