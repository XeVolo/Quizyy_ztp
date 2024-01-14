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
            using var context = new MyBaseContext();
            return context.FlashCards.ToList();

        }
        public List<WriteModel> GetWriteList()
        {
            using (var context = new MyBaseContext())
            {
                List<WriteModel> list = context.Writes.ToList();
                return list;
            }
        }
        public void SaveFlashCards()
        {
            /*using (var context = new MyBaseContext())
            {

                var isExist = context.FlashCards.FirstOrDefault(f => f.concept == flashCard.concept && f.definition == flashCard.definition);
                if (isExist == null)
                {
                    var lastId = context.FlashCards.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
                    flashCard.id = lastId + 1;
                    context.FlashCards.Add(flashCard);
                    context.SaveChanges();
                }
            }*/
        }

        public void SaveWriteList()
        {
            /*
            using (var context = new MyBaseContext())
            {
                var isExist = context.Writes.FirstOrDefault(f => f.question == write.question);
				if (isExist == null)
				{
					var lastId = context.Writes.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
					write.id = lastId + 1;
					context.Writes.Add(write);
					context.SaveChanges();
				}
            }*/
        }
    }
}
