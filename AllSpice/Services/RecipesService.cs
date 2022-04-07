using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllSpice.Models;


using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository rs_repo;

    public RecipesService(RecipesRepository rs_repo)
    {

      this.rs_repo = rs_repo;
    }

    internal Recipe Create(Recipe recipeData)
    {
      return rs_repo.Create(recipeData);
    }
    internal List<Recipe> GetAll()
    {
      return rs_repo.GetAll();
    }

    internal Recipe GetById(int id)
    {
      Recipe found = rs_repo.GetById(id);
      if (found == null)
      {
        throw new Exception("invaild Id");
      }
      return found;
    }
    internal Recipe Edit(Recipe updates, Account userInfo)
    {
      Recipe og = GetById(updates.Id);
      if (og.CreatorId != userInfo.Id)
      {
        throw new Exception("not yours to edit");
      }
      og.Title = updates.Title ?? og.Title;
      og.Subtitle = updates.Subtitle ?? og.Subtitle;
      og.Category = updates.Category ?? og.Category;
      og.Category = updates.Category ?? og.Category;
      rs_repo.Edit(og);
      return og;
    }
    internal string Remove(int id, Account userInfo)
    {
      Recipe recipe = rs_repo.GetById(id);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("Not Yours!!");
      }
      return rs_repo.Remove(id);
    }
  }
}