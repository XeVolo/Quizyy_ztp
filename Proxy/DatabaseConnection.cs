using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Proxy
{
    public class DatabaseConnection : IDatabaseConnection
    {

        public List<FlashCardsModel> GetFlashCardsList()
        {
            using (var context = new MyBaseContext())
            {
                List<FlashCardsModel> list = context.FlashCards.ToList();
                return list;
            }

        }
        public List<WriteModel> GetWriteList()
        {
            using (var context = new MyBaseContext())
            {
                List<WriteModel> list = context.Writes.ToList();
                return list;
            }
        }
        public void SaveFlashCards(string _concept, string _definition, string _difficultylvl)
        {
        }

        public void SaveWriteList(string _question, string _answer, string _incorrectans1, string _incorrectans2, string _incorrectans3, string _difficultylvl)
        {
        }
    }
}
