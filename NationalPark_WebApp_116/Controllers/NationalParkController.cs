using Microsoft.AspNetCore.Mvc;
using NationalPark_WebApp_116.Repository.IRepository;
using NationalPark_WebApp_116.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NationalPark_WebApp_116.Controllers
{
    //[Authorize]
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
             _nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region APIs

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _nationalParkRepository.DeleteAsync(SD.NationalParkAPIPath, id);
            if (status)
                return Json(new { success = true, message = "Data Deleted Successfully!!!" });
            return Json(new { success = false, message = "Something went wrong while deleting data!!!" });
        }
        #endregion

        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark=new NationalPark();
            if(id==null) return View(nationalPark);            
            nationalPark=await _nationalParkRepository.GetAsync(SD.NationalParkAPIPath,id.GetValueOrDefault());
            if(nationalPark==null) return NotFound();
            return View(nationalPark);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count>0)
                {
                    byte[] p1 = null;
                    using(var fs1 = files[0].OpenReadStream())
                    {
                        using(var ms1=new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1=ms1.ToArray();
                        }
                    }
                    nationalPark.Picture = p1;
                }
                else
                {
                    var nationalParkInDb=await _nationalParkRepository.GetAsync(SD.NationalParkAPIPath,nationalPark.Id);
                    nationalPark.Picture = nationalParkInDb.Picture;
                }
                if(nationalPark.Id==0)
                {
                    await _nationalParkRepository.CreateAsync(SD.NationalParkAPIPath,nationalPark);
                }
                else
                {
                    await _nationalParkRepository.UpdateAsync(SD.NationalParkAPIPath,nationalPark);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(nationalPark);
            }
        }




    }
}
