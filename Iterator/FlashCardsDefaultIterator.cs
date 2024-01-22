using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quizyy_wpf.Model;

namespace Quizyy_wpf
{
	public interface IFlashCardsIterator<T>
	{
		T Current { get; }
		bool MoveNext();
		bool MovePrevious();
		bool HasNext();
		bool HasPrevious();
		int GetCurrentIndex();

    }

	public class FlashCardsDefaultIterator : IFlashCardsIterator<FlashCardsModel>
	{
		public List<FlashCardsModel> items;
		private int currentIndex;

		public FlashCardsDefaultIterator(List<FlashCardsModel> items)
		{
			this.items = items;
			this.currentIndex = 0;
		}

		public FlashCardsModel Current
		{
			get { return items[currentIndex]; }
		}

        public bool HasNext()
        {
			return currentIndex < items.Count - 1;
        }

        public bool MoveNext()
		{
			if (HasNext())
			{
				currentIndex++;
				return true;
			}
			else if (currentIndex == items.Count - 1)
			{
				currentIndex = 0;
				return true;
			}

			return false;
		}

		public bool HasPrevious()
		{
			return currentIndex > 0;
		}

		public bool MovePrevious()
		{
			if (HasPrevious())
			{
				currentIndex--;
				return true;
			}
			else if (currentIndex == 0)
			{
				currentIndex = items.Count - 1;
				return true;
			}

			return false;
		}

		public int GetCurrentIndex()
		{
			return currentIndex;
		}
	}

	public class FlashCardsRandomIterator : IFlashCardsIterator<FlashCardsModel>
	{
		public List<FlashCardsModel> items;
		private List<int> randomIndexes;
		private int currentIndex;

		public FlashCardsRandomIterator(List<FlashCardsModel> items)
		{
			this.items = items;
			this.randomIndexes = new List<int>();
			this.currentIndex = 0;
		}

		public FlashCardsModel Current
		{
			get { return items[currentIndex]; }
		}

		public bool MoveNext()
		{
			int randomNumber = new Random().Next(items.Count - 0) + 0;
			currentIndex = randomNumber;
			randomIndexes.Add(currentIndex);
			return true;
		}

		public bool MovePrevious()
		{
			if (randomIndexes.Count > 0)
			{
				randomIndexes.RemoveAt(randomIndexes.Count - 1);
				currentIndex = randomIndexes.Count > 0 ? randomIndexes[randomIndexes.Count - 1] : 0;
				return true;
			}

			return false;
		}

		public bool HasNext()
		{
			return randomIndexes.Count < items.Count;
		}

		public bool HasPrevious()
		{
			return randomIndexes.Count > 0;
		}

		public int GetCurrentIndex()
		{
			return randomIndexes.Count > 0 ? randomIndexes[randomIndexes.Count - 1] : 0;
		}
	}

	public class FlashCardsBy3Iterator : IFlashCardsIterator<FlashCardsModel>
	{
		public List<FlashCardsModel> items;
		private int currentIndex;
		private int counter;

		public FlashCardsBy3Iterator(List<FlashCardsModel> items)
		{
			this.items = items;
			this.currentIndex = 0;
			this.counter = 1;
		}
		
		public bool HasNext()
		{
			return currentIndex < items.Count - 4;

        }

		public bool MoveNext()
		{
			if (HasNext())
			{
				currentIndex += 3;
				return true;
			}
			else if (currentIndex >= items.Count - 4)
			{
				currentIndex = counter;
				counter++;
				return true;
			}

			return false;
		}


		public bool HasPrevious()
		{
			return currentIndex > 4;
		}

		public bool MovePrevious()
		{
			if (HasPrevious())
			{
				currentIndex -= 3;
				return true;
			}
			else if (currentIndex <= 3)
			{
				currentIndex = items.Count - 1;
				return true;
			}

			return false;
		}

		public int GetCurrentIndex()
		{
			return currentIndex;
		}

		public FlashCardsModel Current
		{
			get { return items[currentIndex]; }
		}
	}

}