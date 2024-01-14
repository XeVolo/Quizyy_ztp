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

<<<<<<< Updated upstream
		void SaveFlashCards();

		void SaveWriteList();
=======
                var isExist = context.FlashCards.FirstOrDefault(f => f.concept == flashCard.concept && f.definition == flashCard.definition);
                if (isExist == null)
                {
                    var lastId = context.FlashCards.OrderByDescending(f => f.id).Select(f => f.id).FirstOrDefault();
                    flashCard.id = lastId + 1;
                    context.FlashCards.Add(flashCard);
                    context.SaveChanges();
                }
            } */
        }
>>>>>>> Stashed changes

	}
}
