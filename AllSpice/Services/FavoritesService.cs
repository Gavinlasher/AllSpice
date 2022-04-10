using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository fs_repo;
    private readonly RecipesService _rs;

    public FavoritesService(FavoritesRepository fs_repo, RecipesService rs)
    {
      this.fs_repo = fs_repo;
      _rs = rs;
    }

    internal Favorite Create(Favorite favoriteData)
    {
      return fs_repo.Create(favoriteData);
    }

    internal string Remove(int id, Account userInfo)
    {
      Favorite favorite = fs_repo.GetById(id);
      if (favorite.AccountId != userInfo.Id)
      {
        throw new System.Exception("not yours to delete");
      }
      return fs_repo.Remove(id);
    }
  }
}