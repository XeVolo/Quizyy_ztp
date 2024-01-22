using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Flyweight
{
	public class DifficultyLevelFlyweight
	{
		private static List<DifficultyLevelFlyweight> difficultyLevelFlyweights = new List<DifficultyLevelFlyweight>();
		public string Difficulty { get; private set; }

		private DifficultyLevelFlyweight(string difficulty)
		{
			Difficulty = difficulty;
		}
		public static DifficultyLevelFlyweight GetDifficulty(string difficulty)
		{
			DifficultyLevelFlyweight type = new DifficultyLevelFlyweight(difficulty);
			DifficultyLevelFlyweight found = difficultyLevelFlyweights.Find(item => item.Equals(type));
			if (found == null)
			{
				difficultyLevelFlyweights.Add(type);

			}
			return type;
		}

		public static List<FlyFlashCardsModel> FlyFlashCardsModels(List<FlashCardsModel>flashCardsModels)
		{
			List<FlyFlashCardsModel> flyFlashCardsModels = new List<FlyFlashCardsModel>();
			foreach(FlashCardsModel item in flashCardsModels)
			{
				flyFlashCardsModels.Add(
					new FlyFlashCardsModel
					{
						id=item.id,
						definition=item.definition,
						concept=item.concept,
						difficultylvl=GetDifficulty(item.difficultylvl)
					});
			}
			return flyFlashCardsModels;
		}
		public static List<FlyWriteModel> FlyWriteModels(List<WriteModel> writeModels)
		{
			List<FlyWriteModel> flywriteModels = new List<FlyWriteModel>();
			foreach (WriteModel item in writeModels)
			{
				flywriteModels.Add(
					new FlyWriteModel
					{
						id = item.id,
						question=item.question,
						answer=item.answer,
						incorrectans1 = item.incorrectans1,
						incorrectans2 = item.incorrectans2,
						incorrectans3 = item.incorrectans3,
						difficultylvl = GetDifficulty(item.difficultylvl)
					});
			}
			return flywriteModels;
		}
	}
}
