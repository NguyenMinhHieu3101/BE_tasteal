﻿using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository
{
    public class RecipeRepository : GenericRepository<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(MyDbContext context,
           ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }


    }
}
