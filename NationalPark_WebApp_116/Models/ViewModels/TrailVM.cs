using Microsoft.AspNetCore.Mvc.Rendering;

namespace NationalPark_WebApp_116.Models.ViewModels
{
    public class TrailVM
    {
        public Trail Trail { get; set; }
        public IEnumerable<SelectListItem> nationalParkList { get; set; }
    }
}
