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

		DifficultyLevelFlyweight difficultyFlyweight = new DifficultyLevelFlyweight();
		public  DatabaseConnectionProxy(DatabaseConnection realConnection)
		{
			this.realConnection = realConnection;
		}

		public List<FlashCardsModel> GetFlashCardsList()
		{

			
			if (flashcardslist == null)
			{
				List<FlashCardsModel> rawData = realConnection.GetFlashCardsList();
				foreach (var card in rawData)
				{
					card.difficultylvl = difficultyFlyweight.GetDifficultyLevel(card.difficultylvl);
				}

				flashcardslist = rawData;
			}
			return flashcardslist;
		}
		public List<WriteModel> GetWriteList()
		{
			
			if (writelist == null)
			{
				List<WriteModel> rawData = realConnection.GetWriteList();
				foreach (var question in rawData)
				{
					question.difficultylvl = difficultyFlyweight.GetDifficultyLevel(question.difficultylvl);
				}
                writelist = rawData;
			}
			return writelist;
		}
        /*
        public void SetFlashCardsData(string concept, string definition, string difficultylvl)
        {
            _concept = concept;
            _definition = definition;
            _difficultylvl = difficultyFlyweight.GetDifficultyLevel(difficultylvl);
            SaveFlashCards();
        }*/
        
        /*
        public void SetWriteListData(string question, string answer, string incorrectans1, string incorrectans2, string incorrectans3, string difficultylvl)
        {
            _question = question;
            _answer = answer;
            _incorrectans1 = incorrectans1;
            _incorrectans2 = incorrectans2;
            _incorrectans3 = incorrectans3;
            _difficultylvl = difficultyFlyweight.GetDifficultyLevel(difficultylvl);
			SaveWriteList();
        }*/
        public void SaveFlashCards(string _concept, string _definition, string _difficultylvl)
		{
            // int id = 0;
            FlashCardsModel flashCard = new FlashCardsModel
            {
                concept = _concept,
                definition = _definition,
                difficultylvl = _difficultylvl
            };

            //realConnection.SaveFlashCards(flashCard);

            if(flashcardslist == null)
                GetFlashCardsList();
                //flashCard.id = id;
                //flashcardslist.Add(flashCard);

                var lastId = flashcardslist.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
                flashCard.id = lastId + 1;
                flashcardslist.Add(flashCard);
            
           
        }
		public void SaveWriteList(string _question, string _answer, string _incorrectans1, string _incorrectans2, string _incorrectans3, string _difficultylvl)
		{
            //int id = 0;
            WriteModel write= new WriteModel
            {
                question = _question,
                answer = _answer,
                incorrectans1 = _incorrectans1,
                incorrectans2 = _incorrectans2,
                incorrectans3 = _incorrectans3,
                difficultylvl = _difficultylvl
            };

            //realConnection.SaveWriteList(question);

            if (writelist == null)
                GetWriteList();
                //write.id = id;
                //writelist.Add(write);

                var lastId = writelist.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
                write.id = lastId + 1;
                writelist.Add(write);           
        }

	}
}
