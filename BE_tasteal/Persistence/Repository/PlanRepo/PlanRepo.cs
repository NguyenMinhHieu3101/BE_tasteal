using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Repository.PlanRepo
{
    public class PlanRepo : GenericRepository<PlanEntity>, IPlanRepo
    {
        private readonly IPlanItemRepo _planItemRepo;

        public PlanRepo(MyDbContext context,
         ConnectionManager connectionManager,
         IPlanItemRepo planItemRepo) : base(context, connectionManager)
        {
            _planItemRepo = planItemRepo;
        }
        public List<Plan_ItemEntity> getListPlanItem(string uid)
        {
            var plan = _context.Plan.Where(c => c.account_id == uid).ToList();

            if (plan == null)
            {
                return new List<Plan_ItemEntity>();
            }

            var list = new List<Plan_ItemEntity>();

            foreach (var item in plan)
            {
                var planItem = _context.Plan_Item.Where(c => c.plan_id == item.id)
                            .Include(c => c.plan)
                            .Include(c => c.recipe)
                            .Include(c => c.recipe.account)
                            .OrderBy(c => c.order)
                            .ToList();
                list.AddRange(planItem);
            }

            return list;
        }
        public async Task<bool> addRecipeToPlan(PlanReq req)
        {
            try
            {
                var plan = _context.Plan
                        .Where(c => c.account_id == req.account_id && c.date == ConvertToDateTime(req.date))
                        .FirstOrDefault();

                if (plan == null)
                {
                    PlanEntity planEntity = new PlanEntity
                    {
                        account_id = req.account_id,
                        date = ConvertToDateTime(req.date),
                        note = ""
                    };
                    plan = await InsertAsync(planEntity);
                }

                _planItemRepo.DeleteAll(plan.id);
                var index = 0;
                foreach (var item in req.recipeIds)
                {
                    var planItem = await _planItemRepo.InsertAsync(new Plan_ItemEntity
                    {
                        plan_id = plan.id,
                        recipe_id = item,
                        order = index + 1
                    });
                    index++;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> deleteRecipePlan(PlanDeleteReq req)
        {
            try
            {
                var plan = _context.Plan.Where(c => c.account_id == req.account_id && c.date == ConvertToDateTime(req.date)).FirstOrDefault();

                if (plan != null)
                {
                    var plan_item = _context.Plan_Item.Where(c => c.plan_id == plan.id && c.recipe_id == req.recipeId)
                                    .FirstOrDefault();
                    if (plan_item != null)
                    {
                        await _planItemRepo.DeleteAsync(plan_item);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
        public static DateTime ConvertToDateTime(string inputDateTime)
        {
            // Chuỗi ngày giờ đầu vào
            string input = "2023-12-29T08:45:30.123Z"; // Ví dụ chuỗi đầu vào

            // Định dạng của chuỗi đầu vào
            string format = "yyyy-MM-ddTHH:mm:ss.fffZ";

            // Chuyển đổi từ chuỗi ngày giờ sang kiểu DateTime
            if (DateTime.TryParseExact(inputDateTime, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Không thể chuyển đổi chuỗi ngày giờ.");
            }
        }
    }
}
