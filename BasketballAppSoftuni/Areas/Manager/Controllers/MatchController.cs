using BasketballAppSoftuni.Areas.Manager.Models;
using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.ManagerAreaDTOs;
using BasketballAppSoftuni.Web.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Areas.Manager.Controllers
{
    public class MatchController : ManagerBaseController
    {
        private readonly IManagerService _managerService;
        public MatchController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            try
            {
                var dtos = await _managerService.GetAllTeamNamesAsync();

                var model = new ScheduleMatchModel()
                {
                    AllTeams = MapAllTeams(dtos),
                    MatchDate = DateTime.Now.AddDays(1)
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.GetScheduleError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(ScheduleMatchModel model)
        {
            try
            {
                var dtos = await _managerService.GetAllTeamNamesAsync();
                model.AllTeams = MapAllTeams(dtos);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                bool invalidTeams = AreTeamsValid(model);
                bool invalidDate = IsDateValid(model);

                if (invalidDate || invalidTeams)
                {
                    return View(model);
                }
                else
                {
                    var dto = MapScheduleMatchDTO(model);

                    await _managerService.AddMatchAsync(dto);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.PostScheduleError });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Reschedule()
        {
            try
            {
                var dtos = await _managerService.GetUnplayedMatchesAsync();

                var models = MapUnplayedMatchesModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllMatchesError });
            }
        }



        [HttpPost]
        public async Task<IActionResult> Reschedule(RescheduleMatchModel model)
        {
            try
            {
                if (model.MatchDate < DateTime.Now)
                {
                    ModelState.AddModelError("", "Enter a date that is not earlier than today!");
                }
                else
                {
                    await _managerService.RescheduleMatchAsync(model.MatchId, model.MatchDate);
                }

                var dtos = await _managerService.GetUnplayedMatchesAsync();

                var models = MapUnplayedMatchesModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.RescheduleError});
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateResult()
        {
            try
            {
                var dtos = await _managerService.GetMatchesForUpdateAsync();

                var models = MapUpdateMatchModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllMatchesError});
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateResult(UpdateMatchResultModel model)
        {
            try
            {
                await _managerService.UpdateMatchScoreAsync(model.MatchId, model.HomeTeamPoints, model.AwayTeamPoints);

                var dtos = await _managerService.GetMatchesForUpdateAsync();

                var models = MapUpdateMatchModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.UpdateResultError });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveMatch()
        {
            try
            {
                var dtos = await _managerService.GetUnplayedMatchesAsync();

                var models = MapUnplayedMatchesModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllMatchesError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMatch(int matchId)
        {
            try
            {
                await _managerService.RemoveMatch(matchId);

                var dtos = await _managerService.GetUnplayedMatchesAsync();

                var models = MapUnplayedMatchesModels(dtos);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.RemoveMatchError });
            }
        }

        private static IEnumerable<TeamShortInfoModel> MapAllTeams(IEnumerable<TeamShortInfoDTO> dtos)
        {
            return dtos.Select(d => new TeamShortInfoModel
            {
                Id = d.Id,
                Name = d.Name
            });
        }
        private bool AreTeamsValid(ScheduleMatchModel model)
        {
            bool invalidTeam = false;

            if (model.HomeTeamId == model.AwayTeamId)
            {
                invalidTeam = true;
                ModelState.AddModelError("AwayTeamId", "Home team and away team can not be the same!");
            }

            return invalidTeam;
        }

        private bool IsDateValid(ScheduleMatchModel model)
        {
            double[] time = model.MatchTime.Split(":").Select(double.Parse).ToArray();
            model.MatchDate = model.MatchDate.AddHours(time[0]);
            model.MatchDate = model.MatchDate.AddMinutes(time[1]);

            bool invalidDate = false;
            if (model.MatchDate < DateTime.Now)
            {
                invalidDate = true;
                ModelState.AddModelError("MatchDate", "Match date should not be earlier than today!");
            }

            return invalidDate;
        }
        private static ScheduleMatchDTO MapScheduleMatchDTO(ScheduleMatchModel model)
        {
            return new ScheduleMatchDTO
            {
                AwayTeamId = model.AwayTeamId,
                HomeTeamId = model.HomeTeamId,
                MatchDate = model.MatchDate,
                MatchTime = model.MatchTime,
                TicketPrice = model.TicketPrice
            };
        }
        private static IEnumerable<RescheduleMatchModel> MapUnplayedMatchesModels(IEnumerable<RescheduleMatchDTO> dtos)
        {
            return dtos.Select(d => new RescheduleMatchModel
            {
                AwayTeamLogo = d.AwayTeamLogo,
                AwayTeamName = d.AwayTeamName,
                HomeTeamLogo = d.HomeTeamLogo,
                HomeTeamName = d.HomeTeamName,
                MatchDate = d.MatchDate,
                MatchId = d.MatchId
            });
        }
        private static IEnumerable<UpdateMatchResultModel> MapUpdateMatchModels(IEnumerable<UpdateMatchResultDTO> dtos)
        {
            return dtos.Select(d => new UpdateMatchResultModel
            {
                AwayTeamName = d.AwayTeamName,
                AwayTeamPoints = d.AwayTeamPoints,
                HomeTeamName = d.HomeTeamName,
                HomeTeamPoints = d.HomeTeamPoints,
                MatchDate = d.MatchDate,
                MatchId = d.MatchId
            });
        }
    }
}
