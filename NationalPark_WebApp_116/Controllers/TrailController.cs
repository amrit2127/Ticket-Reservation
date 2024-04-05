using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Models.ViewModels;
using NationalPark_WebApp_116.Repository.IRepository;

namespace NationalPark_WebApp_116.Controllers
{
   
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;
        public TrailController(INationalParkRepository nationalParkRepository,ITrailRepository trailRepository)
        {
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new {data = await _trailRepository.GetAllAsync(SD.TrailAPIPath)});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            if(!status)
            {
                return Json(new { success = false, message = "Something Went wrong while Deleting Data!!!" });
            }
            return Json(new { success = true, message = "Data deleted successfully!!!" });
        }
        #endregion
        
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVM trailVM = new TrailVM()
            {
                nationalParkList = nationalParks.Select(np => new SelectListItem()
                {
                    Text = np.Name,
                    Value = np.Id.ToString()
                }),
                Trail = new Trail()
            };
            if (id == null) return View(trailVM);
            trailVM.Trail = await _trailRepository.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault());
            if (trailVM.Trail == null) return NotFound();
            return View(trailVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {
            if(ModelState.IsValid)
            {
                if(trailVM.Trail.Id==0)
                {
                    await _trailRepository.CreateAsync(SD.TrailAPIPath,trailVM.Trail);
                }
                else
                    await _trailRepository.UpdateAsync(SD.TrailAPIPath,trailVM.Trail);
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<NationalPark> nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
                trailVM = new TrailVM()
                {
                    nationalParkList = nationalParks.Select(np => new SelectListItem()
                    {
                        Text = np.Name,
                        Value = np.Id.ToString()
                    }),
                    Trail = new Trail()
                };
                return View(trailVM);
            }

        }
        
    }
}
