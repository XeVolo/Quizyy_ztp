using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Flyweight
{
	public class DifficultyLevelFlyweight
	{
		private Dictionary<string, string> difficultyLevels = new Dictionary<string, string>();

		public string GetDifficultyLevel(string difficulty)
		{
			if (!difficultyLevels.ContainsKey(difficulty))
			{				
				difficultyLevels[difficulty] = difficulty;
			}

			return difficultyLevels[difficulty];
		}
	}
}
