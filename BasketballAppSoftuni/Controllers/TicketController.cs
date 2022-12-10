using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.TicketDTOs;
using BasketballAppSoftuni.Models.TicketViewModels;
using BasketballAppSoftuni.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BasketballAppSoftuni.Controllers
{
	[Authorize]
	public class TicketController : Controller
	{
		private readonly ITicketService _ticketService;

		public TicketController(ITicketService ticketService)
		{
			_ticketService = ticketService;
		}

		[HttpGet]
		public async Task<IActionResult> BuyTickets(int matchId)
		{
			try
			{
                var dto = await _ticketService.CreateTicketAsync(matchId);

                var ticketModel = MapTicketModel(dto);

                return View(ticketModel);
            }
			catch (Exception)
			{
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.GetTicketError });
			}
			
		}
		
		[HttpPost]
		public async Task<IActionResult> BuyTickets(TicketViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

			try
			{
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await _ticketService.BuyTicketsAsync(userId, model.MatchId, model.Quantity);

                return RedirectToAction("MyMatches", "Match");
            }
			catch (Exception)
			{
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.BuyTicketError });
            }
            
		}

        private static TicketViewModel MapTicketModel(TicketDTO dto)
        {
            return new TicketViewModel
            {
                ArenaLocation = dto.ArenaLocation,
                AwayTeamLogo = dto.AwayTeamLogo,
                ArenaName = dto.ArenaName,
                ShippingAddress = dto.ShippingAddress,
                TicketsAvailable = dto.TicketsAvailable,
                FirstName = dto.FirstName,
                HomeTeamLogo = dto.HomeTeamLogo,
                LastName = dto.LastName,
                MatchId = dto.MatchId,
                PricePerTicket = dto.PricePerTicket,
                Quantity = dto.Quantity
            };
        }
    }
}
