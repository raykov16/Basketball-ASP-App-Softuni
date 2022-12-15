using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.ArenaDTOs;
using BasketballAppSoftuni.Models.ArenaViewModels;
using BasketballAppSoftuni.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BasketballAppSoftuni.Controllers
{
    public class ArenaController : Controller
    {
        private readonly IArenaService _arenaService;
        private readonly IMemoryCache _cache;
        public ArenaController(IArenaService arenaService, IMemoryCache cache)
        {
            _arenaService = arenaService;
            _cache = cache;
        }

        public async Task<IActionResult> AllArenas()
        {
            try
            {
                var models = _cache.Get<IEnumerable<ArenaDetailsViewModel>>(CacheKeys.AllArenasKey);

                if (models == null)
                {
                    var dtos = await _arenaService.GetAllAsync();
                    models = MapModels(dtos);

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(40));

                    _cache.Set(CacheKeys.AllArenasKey, models, cacheOptions);
                }

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.AllArenasError });
            }
        }
        public async Task<IActionResult> ArenaDetails(int arenaId)
        {
            try
            {
                var dto = await _arenaService.GetAsync(arenaId);

                var model = MapModel(dto);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = ErrorMessages.ArenaError });
            }

        }

        private static IEnumerable<ArenaDetailsViewModel> MapModels(List<ArenaDetailsDTO> dtos)
        {
            return dtos
           .Select(d => new ArenaDetailsViewModel
           {
               Location = d.Location,
               Name = d.Name,
               PictureURL = d.PictureURL,
               Seats = d.Seats
           });
        }
        private static ArenaDetailsViewModel MapModel(ArenaDetailsDTO dto)
        {
            return new ArenaDetailsViewModel
            {
                Location = dto.Location,
                Name = dto.Name,
                PictureURL = dto.PictureURL,
                Seats = dto.Seats
            };
        }
    }
}
