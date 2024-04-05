using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_116.Models;
using NationalParkAPI_116.Models.DTOs;
using NationalParkAPI_116.Repository.IRepository;

namespace NationalParkAPI_116.Controllers
{
    [Route("api/Trail")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trailRepository,IMapper mapper)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepository.GetTrails().ToList().Select(_mapper.Map<Trail,TrailDto>));
        }

        [HttpGet("{trailId:int}",Name = "GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail=_trailRepository.GetTrail(trailId);
            if(trailId==null) return NotFound();
            return Ok(_mapper.Map<TrailDto>(trail));
        }

        [HttpPost]
        public IActionResult CreateTrail([FromBody]TrailDto trailDto)
        {
            if (trailDto == null) return BadRequest();
            if(_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail in db!!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if(!ModelState.IsValid) return BadRequest();
            var trail=_mapper.Map<TrailDto,Trail>(trailDto);
            if(!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while save trail:{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtRoute("GetTrail", new {trailId=trail.Id},trail); 
        }

        [HttpPut]
        public IActionResult UpdateTrail([FromBody] TrailDto trailDto)
        {
            if(trailDto == null) return BadRequest();
            if(!ModelState.IsValid) return BadRequest();
            var trail = _mapper.Map<TrailDto, Trail>(trailDto);
            if(!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("",$"Something went wrong while update trail");
                return StatusCode(StatusCodes.Status500InternalServerError) ;
            }
            return NoContent();
        }

        [HttpDelete("{trailId:int}")]
        public IActionResult DeleteTrail(int trailId)
        {
            if(!_trailRepository.TrailExists(trailId)) return NotFound();
            var trail=_trailRepository.GetTrail(trailId);
            if(trail==null) return NotFound();
            if(!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while delete trail");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
