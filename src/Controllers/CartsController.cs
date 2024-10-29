using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Services.cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static src.DTO.CartDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // api/v1/carts
    public class CartsController : ControllerBase // CartsController inherits from ControllerBase
    {

        /// <summary>
        /// The point of the carts controller is to act as the entry point for handling HTTP requests related to the shopping cart.
        /// It defines endpoints (routes) for clients to interact with the application.
        /// include: create, read, update and delete (GET, POST, PUT, DELETE).
        /// 1- Adding items to the cart
        /// 2- Removing items from the cart
        /// 3- Updating items quantities
        /// 4- Retrieving the current state of the cart
        /// </summary>

        // field
        protected readonly ICartService _cartService;

        // Constructor for DI (Dependency Injection)
        public CartsController(ICartService service)
        {
            _cartService = service;
        }

        /// <API>
        /// {
        ///  "cartPrice": 0,
        ///  "cartQuantity": 0,
        ///  "userId": 0,
        ///  "jewelryId": 0
        /// }
        /// </API>
        /// return cart info

        // Create a new Cart
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartReadDTO>> CreateOne([FromBody] CartCreateDTO createDto)
        {
            var authenticateClaims = HttpContext.User;
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            var userGuid = new Guid(userId);
            //var cartCreated = await _cartService.CreateOneAsync(createDto);
            //return Ok(cartCreated); // 200 Ok
            return await _cartService.CreateOneAsync(userGuid, createDto);
        }

        // Update a cart
        [HttpPut("{id}")]
        public async Task<ActionResult<CartReadDTO>> UpdateOne(Guid id, CartUpdateDTO updateDto)
        {
            var cartUpdated = await _cartService.UpdateOneAsync(id, updateDto);
            if (cartUpdated == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(cartUpdated); // 200 OK 
        }

        /// <API>
        /// [
        ///  {
        ///   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///   "cartPrice": 0,
        ///   "cartQuantity": 0,
        ///   "userId": 0,
        ///   "jewelryId": 0
        ///  }
        /// ]
        /// </API>
        /// return cart info

        // Get all carts
        [HttpGet]
        public async Task<ActionResult<List<CartReadDTO>>> GetAll()
        {
            var cartList = await _cartService.GetAllAsync();
            return Ok(cartList); // 200 Ok
        }

        // Get cart by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CartReadDTO>> GetById(Guid id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(cart); // 200 OK 
        }

    } // end class
} // end namespace