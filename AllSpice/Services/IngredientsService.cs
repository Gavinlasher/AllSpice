using AllSpice.Models;
using AllSpice.Services;

namespace AllSpice.Repositories
{
  public class IngredientsService
  {
    private readonly IngredientsRepository ingredents_Repo;
    private readonly RecipesService _rs;

    public IngredientsService(IngredientsRepository ingredents_Repo, RecipesService rs)
    {
      this.ingredents_Repo = ingredents_Repo;
      _rs = rs;
    }

    internal Ingredient Create(Ingredient ingredientData, Account userInfo)
    {
      Recipe recipe = _rs.GetById(ingredientData.RecipeId);
      ingredientData.RecipeId = recipe.Id;
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new System.Exception("PLease no");
      }
      return ingredents_Repo.Create(ingredientData);
    }

    internal Ingredient Edit(Ingredient updates, Account userInfo)
    {
      Recipe recipe = _rs.GetById(updates.RecipeId);
      updates.RecipeId = recipe.Id;
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new System.Exception("PLease no");
      }
      Ingredient og = ingredents_Repo.GetById(updates.Id);
      og.Name = updates.Name ?? og.Name;
      og.Quantity = updates.Quantity ?? og.Quantity;
      ingredents_Repo.Edit(og);
      return og;
    }

    internal string Remove(int id, Account userInfo)
    {
      Ingredient ingredient = ingredents_Repo.GetById(id);
      Recipe recipe = _rs.GetById(ingredient.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new System.Exception("PLease no");
      }
      return ingredents_Repo.Remove(id);
    }
  }
}