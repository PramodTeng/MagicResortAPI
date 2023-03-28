using MagicResort_API.Data;
using MagicResort_API.Logging;
using MagicResort_API.Model;
using MagicResort_API.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace MagicResort_API.Controllers
{

    [ApiController]
    [Route("api/ResortAPI")]
    public class ResortAPIController : ControllerBase
    {
        private readonly ILogging _logger;

        public ResortAPIController(ILogging logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ResortDTO>> GetResorts()
        {

            _logger.Log("Getting all the Resorts", "");
            return Ok(ResortStore.ResortList);
        }

        [HttpGet("{id:int}", Name = "Get Resort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ResortDTO> GetResort(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Resort Error with Id" + id, "error"); 
                return BadRequest();
            }

            var resort = ResortStore.ResortList.FirstOrDefault(u => u.Id == id);
            if (resort == null)
            {
                return NotFound();
            }
            return Ok(resort);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResortDTO> CreateResort([FromBody] ResortDTO resortDTO)
        {
            if (ResortStore.ResortList.FirstOrDefault(u => u.Name.ToLower() == resortDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Resort Already Exist!");
                return BadRequest(ModelState);
            }
            if (resortDTO == null)
            {
                return BadRequest(resortDTO);
            }
            if (resortDTO.Id > 0)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            resortDTO.Id = ResortStore.ResortList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            ResortStore.ResortList.Add(resortDTO);

            return CreatedAtRoute("Get Resort", new { id = resortDTO.Id }, resortDTO);
        }



        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpDelete("{id:int}", Name = "Delete Resort")]
        public IActionResult DeleteResort(int id)
        {

            if (id == 0)
            {

                return BadRequest();
            }

            var resort = ResortStore.ResortList.FirstOrDefault(u => u.Id == id);
            if (resort == null)
            {
                return NotFound();

            }
            ResortStore.ResortList.Remove(resort);
            return NoContent();
        }



        [HttpPut("{id:int}", Name = "Update Resort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateResort(int id, [FromBody] ResortDTO resortDTO)
        {
            if(resortDTO == null || id != resortDTO.Id )
            {

                return BadRequest();

            }
            var resort = ResortStore.ResortList.FirstOrDefault(u => u.Id == id);
           resort.Name = resortDTO.Name;    
            resort.Id = resortDTO.Id;   
            resort.Occupancy = resortDTO.Occupancy; 
            resort.Sqft = resortDTO.Sqft;


            return NoContent();

        }



        [HttpPatch("{id:int}", Name = "PatchUpdate Resort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PatchUpdateResort(int id, JsonPatchDocument<ResortDTO> patchresortDTO)
        {
            if (patchresortDTO == null || id == 0)
            {

                return BadRequest();

            }
            var resort = ResortStore.ResortList.FirstOrDefault(u => u.Id == id);
            if (resort == null)
            {
                return NotFound();
            }
            patchresortDTO.ApplyTo(resort, ModelState);
            if (!ModelState.IsValid)
            {
                  return BadRequest(ModelState);    

            }

            return NoContent();

        }
    }
}