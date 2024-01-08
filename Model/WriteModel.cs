using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizyy_wpf.Model
{
    public class WriteModel
    {
		public int id { get; set; }
		[Required]
		[StringLength(100)]
		public string question { get; set; }
		[Required]
		[StringLength(100)]
		public string answer { get; set; }
		[StringLength(100)]
		public string incorrectans1 { get; set; }
		[StringLength(100)]
		public string incorrectans2 { get; set; }
		[StringLength(100)]
		public string incorrectans3 { get; set; }
		public string difficultylvl { get; set; }
	}
}
