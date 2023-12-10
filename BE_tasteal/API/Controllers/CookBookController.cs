﻿using BE_tasteal.Business.Cart;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Persistence.Repository.CookBookRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CookBookController : Controller
    {
        private readonly CookBookRepo _cookBookRepo;
        public CookBookController(CookBookRepo cartBusiness)
        {
            _cookBookRepo = cartBusiness;
        }

        [HttpGet]
        [Route("cookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCookbookByUid(string uid)
        {
            try
            {
                var allCart = await _cookBookRepo.getAllCookBookByUid(uid);
                return Ok(allCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("cookbook-recipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCookbook_Recipe(int cookBookId)
        {
            try
            {
                var allCart = _cookBookRepo.GetCookBookRecipesWithRecipes(cookBookId);
                return Ok(allCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("cookbook-recipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCookBookRecipeById(string id)
        {
            try
            {
                var allCart = await _cookBookRepo.DeleteCookBookRecipeById(id);
                if(allCart > 0)
                {
                    return Ok("success");
                }
                else
                    return BadRequest(false);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("recipetonewcookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MoveRecipeToNewCookBook(NewRecipeCookBookReq id)
        {
            try
            {
                var allCart = await _cookBookRepo.MoveRecipeToNewCookBook(id);
                if (allCart > 0)
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
        [HttpPut]
        [Route("namecookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RenameCookbookById(NewCookBookNameReq name)
        {
            try
            {
                var allCart = await _cookBookRepo.RenameCookBook(name);
                if (allCart > 0)
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
        [HttpDelete]
        [Route("cookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCookBookById(int id)
        {
            try
            {
                var allCart = await _cookBookRepo.DeleteCookBook(id);
                if (allCart > 0)
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
        [HttpPost]
        [Route("cookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewCookBook(NewCookBookReq id)
        {
            try
            {
                var allCart = await _cookBookRepo.CreateNewCookBook(id);
                if (allCart > 0)
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
        [HttpPost]
        [Route("recipetocookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRecipeToCookBook(RecipeToCookBook id)
        {
            try
            {
                var allCart = await _cookBookRepo.AddRecipeToCookBook(id);
                if (allCart > 0)
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
    }
}