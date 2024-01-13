using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Model
{
    public class FlashCardsModel
    {
		public int id { get; set; }
		[Required]
		[StringLength(40)]
		public string concept { get; set; }
		[Required]
		[StringLength(40)]
		public string definition { get; set; }
		public string difficultylvl { get; set; }
	}
}
