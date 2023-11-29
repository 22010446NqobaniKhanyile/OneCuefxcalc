using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPrivateExnessCalculator.Models
{
	public class Chart
	{
		[Key]
		public int ChartID { get; set; }

		[Required]
		public string Pair { get; set; }
		public enum PairList
		{
			GBPUSD,
			AUDUSD,
			EURUSD,
			USDCHF
		}
		[Required]
		[DataType(DataType.Date)]
		public string Date { get; set; }
		[DisplayName("Upload File")]
		public string ImagePath { get; set; }
		[NotMapped]
		public HttpPostedFileBase ImageFile { get; set; }

	}
}