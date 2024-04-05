using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_116.Models;
using NationalParkAPI_116.Models.DTOs;
using NationalParkAPI_116.Repository.IRepository;

namespace NationalParkAPI_116.Controllers
{
    [Route("api/NationalPark")]
    [ApiController]
    
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository,IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nationalParkListDto=_nationalParkRepository.GetNationalParks().ToList().
                Select(_mapper.Map<NationalPark,NationalParkDto>);
            return Ok(nationalParkListDto);    //200
        }

        [HttpGet("{nationalParkId:int}",Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark=_nationalParkRepository.GetNationalPark(nationalParkId);
            if(nationalPark==null) return NotFound();  //404
            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalParkDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest(ModelState);  //400
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", $"National Park in db!!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest(); //400
            var nationalPark = _mapper.Map<NationalParkDto, NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong while save data:{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // return Ok(); //200

           return CreatedAtRoute("GetNationalPark", new {nationalParkId=nationalPark.Id},nationalPark); //201
        }

        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null) return BadRequest(ModelState);
            if(!ModelState.IsValid) return BadRequest();
            var nationalPark = _mapper.Map<NationalParkDto, NationalPark>(nationalParkDto);
            if(!_nationalParkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong while update data:{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // return Ok();

            return NoContent(); //204
        }

        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if(!_nationalParkRepository.NationalParkExists(nationalParkId)) return NotFound();
            var nationalPark=_nationalParkRepository.GetNationalPark(nationalParkId);
            if(nationalPark == null) return NotFound();
            if(!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Something went wrong while delete data:{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError) ;
            }
            return Ok();
        }

    }
}
