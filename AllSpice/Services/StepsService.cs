using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{

  public class StepsService
  {
    private readonly StepsRepository s_repo;
    private readonly RecipesService _rs;

    public StepsService(StepsRepository repo, RecipesService rs)
    {
      s_repo = repo;
      _rs = rs;
    }

    internal Step Create(Step stepData, Account userInfo)
    {
      Recipe recipe = _rs.GetById(stepData.RecipeId);
      stepData.RecipeId = recipe.Id;
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new System.Exception("PLease no");
      }
      return s_repo.Create(stepData);
    }

    internal Step Edit(Step updates, Account userInfo)
    {
      Recipe recipe = _rs.GetById(updates.RecipeId);
      updates.RecipeId = recipe.Id;
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new System.Exception("PLease no");
      }
      Step og = s_repo.GetById(updates.Id);
      og.Position = updates.Position;
      og.Body = updates.Body ?? og.Body;
      s_repo.Edit(og);
      return og;
    }

    internal string Remove(int id, Account userInfo)
    {
      Step step = s_repo.GetById(id);
      Recipe recipe = _rs.GetById(step.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new System.Exception("PLease no");
      }
      return s_repo.Remove(id);
    }
  }
}