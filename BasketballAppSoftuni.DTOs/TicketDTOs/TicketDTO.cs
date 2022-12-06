using System.ComponentModel.DataAnnotations;

namespace BasketballAppSoftuni.DTOs.TicketDTOs
{
	public class TicketDTO
	{
		public int MatchId { get; set; }
		[Required]
		public string FirstName { get; set;}
        [Required]
        public string LastName { get; set;}
        [Required]
		public string ShippingAddress { get; set; }
        public string HomeTeamLogo { get; set;}
		public string AwayTeamLogo { get; set;}
		public string ArenaName { get; set; }
		public string ArenaLocation { get; set; }
		public int TicketsAvailable { get; set; }
		public int Quantity { get; set; }
		public decimal PricePerTicket { get; set;}
	}
}
