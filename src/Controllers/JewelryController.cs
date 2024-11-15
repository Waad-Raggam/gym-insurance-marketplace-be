using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.Jewelry;
using src.Utils;
using static src.DTO.JewelryDTO;

namespace src.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]//api/v1/Jewelry
    public class JewelryController : ControllerBase
    {

        /// <summary>
        /// The point of the JewelryController file is to handle HTTP requests related to jewelry items in the application.
        /// It defines endpoints for creating, reading, updating, and deleting jewelry items.
        /// 1- Creating a new jewelry item.
        /// 2- Retrieving a list of all jewelry items.
        /// 3- Updating jewelry item information such as name, type, price, image, and description.
        /// 4- Deleting a jewelry item.
        /// </summary>
        protected readonly IJewelryService _jewelryService;

        public JewelryController(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        // POST: api/v1/Jewelry
        // Create a new jewelry item
        /// <API>
        /// {
        ///  "jewelryName": " ",
        ///   "jewelryType": " ",
        ///   "jewelryPrice": ,
        ///   "jewelryImage": " ",
        ///   "description": " ",
        ///  "gemstoneId": " ",
        ///  "carvingId": " ",
        ///  "userId": " "
        ///}
        /// <API>
        /// return jewelry info 
        [HttpPost]
        // [Authorize(Roles = "Admin")] //--> Just the Admin Can Create New Jewelry
        public async Task<ActionResult<JewelryReadDto>> CreateOne(JewelryCreateDto createDto)
        {
            var nweJewelry = await _jewelryService.CreateOneAsync(createDto);
            return Ok(nweJewelry);//200 Ok
        }


        // Get all jewelry items
        [AllowAnonymous]
        [HttpGet] // GET: api/v1/Jewelry
        public async Task<ActionResult<List<JewelryReadDto>>> GetAll()
        {
            var jewelryList = await _jewelryService.GetAllAsync();
            return Ok(jewelryList); //200 OK
        }

        [AllowAnonymous]
        // Get a jewelry item by its ID
        [HttpGet("{JewelryId}")]// GET: api/v1/Jewelry/{JewelryId}
        public async Task<ActionResult<JewelryReadDto>> GetById(Guid JewelryId)
        {
            var foundJewelry = await _jewelryService.GetByIdAsync(JewelryId);
            if (foundJewelry == null)
            {
                return NotFound("Jewelry item not found"); //400 Not Found
            }
            return Ok(foundJewelry); //200 Ok
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{JewelryId}")]
        public async Task<ActionResult<JewelryReadDto>> UpdateOne(Guid JewelryId, JewelryUpdateDto updateDto)
        {
            var jewelryUpdate = await _jewelryService.UpdateOneAsync(JewelryId, updateDto);
            if (jewelryUpdate == null)
            {
                return NotFound("Jewelry item not found"); //400  Not Found
            }
            return Ok(jewelryUpdate); //200 OK
        }

        // Delete a jewelry item by its ID
        [Authorize(Roles = "Admin")]
        [HttpDelete("{JewelryId}")] //api/v1/Jewelry/{JewelryId}
        public async Task<ActionResult> DeleteOne(Guid JewelryId)
        {
            var jewelryDeleted = await _jewelryService.DeleteOneAsync(JewelryId);
            if (jewelryDeleted == false)
            {
                return NotFound(); // 404 Not Found
            }
            return NoContent(); // 200 OK 
        }

        //Searsh with pagination
        [AllowAnonymous]
        [HttpGet("Search")] // /api/v1/Jewelry/Search?Limit=3&Offset=0&Search=ring
        public async Task<ActionResult<List<JewelryReadDto>>> GetAllJewelryBySearch([FromQuery] PaginationOptions paginationOptions)
        {
            var jewelryList = await _jewelryService.GetAllBySearchAsync(paginationOptions);
            return Ok(jewelryList);
        }

        [AllowAnonymous]
        [HttpGet("Filter")] // /api/v1/Jewelry/Filter?Name or MinPrice Or MaxPrice
        public async Task<ActionResult<List<Jewelry>>> FilterJe([FromQuery] FilterationOptions jewelryFilter)
        {
            var jewelries = await _jewelryService.GetAllByFilterationAsync(jewelryFilter);
            return Ok(jewelries);
        }

    }
}