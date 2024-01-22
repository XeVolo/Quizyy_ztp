using Quizyy_wpf.Flyweight;
using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Proxy
{
	public class DatabaseConnectionProxy : IDatabaseConnection
	{
		private DatabaseConnection realConnection;
		private List<FlashCardsModel>? flashcardslist;
		private List<WriteModel>? writelist;

		public  DatabaseConnectionProxy(DatabaseConnection realConnection)
		{
			this.realConnection = realConnection;
		}

		public List<FlashCardsModel> GetFlashCardsList()
		{

			
			if (flashcardslist == null)
			{
				List<FlashCardsModel> rawData = realConnection.GetFlashCardsList();				
				flashcardslist = rawData;
			}
			return flashcardslist;
		}
		public List<WriteModel> GetWriteList()
		{
			
			if (writelist == null)
			{
				List<WriteModel> rawData = realConnection.GetWriteList();
                writelist = rawData;
			}
			return writelist;
		}
        public void SaveFlashCards(string _concept, string _definition, string _difficultylvl)
		{
            FlashCardsModel flashCard = new FlashCardsModel
            {
                concept = _concept,
                definition = _definition,
                difficultylvl = _difficultylvl
            };

            if(flashcardslist == null)
                GetFlashCardsList();

                var lastId = flashcardslist.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
                flashCard.id = lastId + 1;
                flashcardslist.Add(flashCard);
            
           
        }
		public void SaveWriteList(string _question, string _answer, string _incorrectans1, string _incorrectans2, string _incorrectans3, string _difficultylvl)
		{
            WriteModel write= new WriteModel
            {
                question = _question,
                answer = _answer,
                incorrectans1 = _incorrectans1,
                incorrectans2 = _incorrectans2,
                incorrectans3 = _incorrectans3,
                difficultylvl = _difficultylvl
            };

            if (writelist == null)
                GetWriteList();

                var lastId = writelist.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
                write.id = lastId + 1;
                writelist.Add(write);           
        }

	}
}
