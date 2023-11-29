using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPrivateExnessCalculator.Models
{
	public class Trade
	{
		[Key]
		public int TradeId { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

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
		public decimal Entry { get; set; }
		[Required]
		
		public decimal StopLoss { get; set; }

		
		public decimal StopLossPoints { get; set; }

		[Required]
		public decimal TP { get; set; }
		[Required]
		

		public decimal PipValue { get; set; }
		
		public decimal OnePipette { get; set; }

		[Required]

		
		public decimal Margin { get; set; }

		
		public decimal MinLot { get; set; }

		
		public decimal FinalLot { get; set; }

		
		public decimal StopLossValue { get; set; }
		public string Results { get; set; }
		public enum ResultsList
		{
			Win,
			Loose,
			Pending
		}
		[Display(Name = "Hit stop")]
		public bool HitStop { get; set; }
		[Display(Name = "Gained amount")]
		public decimal Gain { get; set; }
		[Display(Name = "Deficit")]
		public decimal Loss { get; set; }


	}
}