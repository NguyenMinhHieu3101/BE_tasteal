﻿using BE_tasteal.API.AppSettings;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.PlanRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BE_tasteal.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PlanController : Controller
    {
        private readonly IPlanRepo _planRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRecipeRepository _recipeRepository;

        public PlanController(IPlanRepo planRepo,
            IUserRepo userRepo,
            IRecipeRepository recipeRepository)
        {
            _planRepo = planRepo;
            _userRepo = userRepo;
            _recipeRepository = recipeRepository;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AllCart(string accountId)
        {
            try
            {
                if (await _userRepo.FindByIdAsync(accountId) == null)
                    return BadRequest("User Id invalid");

                var allCart = _planRepo.getListPlanItem(accountId);

                return Ok(allCart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("addorupdate")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> addrecipe(PlanReq req)
        {
            try
            {
                if (await _userRepo.FindByIdAsync(req.account_id) == null)
                    return BadRequest("User Id invalid");

                foreach (var item in req.recipeIds)
                {
                    if (await _recipeRepository.FindByIdAsync(item) == null)
                        return BadRequest("recipe id invalid");
                }

                var result = await _planRepo.addRecipeToPlan(req);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> deletePlanItem(PlanDeleteReq req)
        {
            try
            {
                if (await _userRepo.FindByIdAsync(req.account_id) == null)
                    return BadRequest("User Id invalid");

                if (await _recipeRepository.FindByIdAsync(req.recipeId) == null)
                    return BadRequest("recipe Id invalid");

                var result = await _planRepo.deleteRecipePlan(req);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("recommendMealPlan")]
        public async Task<IActionResult> getRecommendMealPlan(RecommendMealPlanReq req)
        {
            try
            {
                List<RecipeEntity> userRecipe = new List<RecipeEntity>();
                decimal userCalo = 0;
               foreach(var item in req.recipe_ids)
                {
                    var recipe = _recipeRepository.FindByIdAsyncWithNutrition(item);
                    if (recipe == null)
                        return BadRequest("Recipe id invalid");

                    userRecipe.Add(recipe);
                    userCalo += recipe.nutrition_info.calories ?? 0;
                }

                decimal standardCalo = 0;

                if(req.gender) //nam
                {
                    standardCalo = (((decimal)13.397 * req.weight) + ((decimal)4.799 * req.height) - ((decimal)5.677 * (decimal)req.age) + (decimal)88.362);
                }
                else
                {
                    standardCalo = (((decimal)9.247 * req.weight) + ((decimal)3.098 * req.height) - ((decimal)4.330 * (decimal)req.age) + (decimal)447.593);
                }

                standardCalo = standardCalo * req.rate;

               


                if((standardCalo * 92 / 100) <= userCalo && userCalo <= (standardCalo * 108 / 100))
                {
                    RecommendMealPlanRes response = new RecommendMealPlanRes();
                    response.state = "equal";
                    response.recipe_add_ids = new List<planRecipe>();
                    response.recipe_remove_ids = new List<planRecipe>();
                    response.standard_calories = standardCalo;
                    response.real_calories = userCalo;

                    return Ok(response);
                }

                decimal dis = Math.Abs(standardCalo - userCalo);


                var mindifCaloRecipe = userRecipe
                        .OrderByDescending(r => Math.Abs(r.nutrition_info.calories ?? 0 - dis))
                        .FirstOrDefault();
                if (userCalo > standardCalo)
                {

                    RecommendMealPlanRes response = new RecommendMealPlanRes();
                    response.state = "higher";
                    response.recipe_add_ids = new List<planRecipe>();
                    response.recipe_remove_ids = new List<planRecipe>();
                    response.real_calories = userCalo - mindifCaloRecipe.nutrition_info.calories ?? 0;

                    while(!((standardCalo * 92 / 100) <= response.real_calories 
                        && response.real_calories <= (standardCalo * 108 / 100)))
                    {
                        var newCalo = userCalo - mindifCaloRecipe.nutrition_info.calories ?? 0;
                        dis = standardCalo - newCalo;

                        if (dis < 0)
                            continue;

                        mindifCaloRecipe = userRecipe
                           .OrderByDescending(r => Math.Abs(r.nutrition_info.calories ?? 0 - dis))
                           .FirstOrDefault();
                        var element = userRecipe.FirstOrDefault(c => c.id == mindifCaloRecipe.id);
                        userRecipe.Remove(element);

                     
                        var dis1 = Math.Abs(standardCalo - newCalo);


                        //tru roi ma no nam trong khoang thi tra
                        if ((standardCalo * 92 / 100) <= newCalo && newCalo  <= (standardCalo * 108 / 100))
                        {
                            planRecipe planRecp = new planRecipe
                            {
                                id = mindifCaloRecipe.id,
                                amount = 1
                            };
                            response.recipe_remove_ids.Add(planRecp);
                            response.real_calories = newCalo;
                         }
                        else
                        {
                            var closetRecipeDiff = _recipeRepository.closetRecipeDiff(dis1);

                            if (closetRecipeDiff != null)
                            {
                            
                                response.recipe_add_ids.Add(new planRecipe { id = closetRecipeDiff.id , amount = 1 });
                                response.recipe_remove_ids.Add(new planRecipe { id = mindifCaloRecipe.id,  amount = 1 });
                                response.standard_calories = standardCalo;
                                response.real_calories = newCalo + closetRecipeDiff.nutrition_info.calories ?? 0;
                            }
                            else
                            {
                                var recipeN =  _recipeRepository.FindByIdAsyncWithNutrition(115);
                                int n = (int)Math.Floor(dis1 / recipeN.nutrition_info.calories??1);
                                response.recipe_add_ids.Add(new planRecipe { id = recipeN.id, amount = n });
                                response.recipe_remove_ids.Add(new planRecipe { id = mindifCaloRecipe.id , amount = 1 });
                                response.standard_calories = standardCalo;
                                response.real_calories = newCalo + dis1;
                            }
                        }
                    }
                    return Ok(response);
                }
                else
                {
                    var closetRecipeDiff = _recipeRepository.closetRecipeDiff(dis);

                    if ((dis * 92 / 100) <= closetRecipeDiff.nutrition_info.calories 
                        && closetRecipeDiff.nutrition_info.calories <= (dis * 108 / 100)) 
                    {
                        RecommendMealPlanRes response = new RecommendMealPlanRes();
                        response.state = "smaller";
                        response.recipe_add_ids = new List<planRecipe>();
                        response.recipe_add_ids.Add(new planRecipe { id = closetRecipeDiff.id , amount = 1});
                        response.recipe_remove_ids = new List<planRecipe>();
             
                        response.standard_calories = standardCalo;
                        response.real_calories = userCalo + closetRecipeDiff.nutrition_info.calories??0;

                        return Ok(response);
                    }
                    else
                    {
                        RecommendMealPlanRes response = new RecommendMealPlanRes();
                        var recipeN = _recipeRepository.FindByIdAsyncWithNutrition(115);

                        int n = (int)Math.Floor(dis / recipeN.nutrition_info.calories ?? 1);

                        response.state = "smaller";
                        response.recipe_add_ids = new List<planRecipe>();
                        response.recipe_add_ids.Add(new planRecipe { id = recipeN.id, amount = n });
                        response.recipe_remove_ids = new List<planRecipe>();
                        response.recipe_remove_ids.Add(new planRecipe { id = mindifCaloRecipe.id, amount = 1 });
                        response.standard_calories = standardCalo;
                        response.real_calories = userCalo - dis + n * recipeN.nutrition_info.calories??0;

                        return Ok(response);
                    }
                }


                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
