using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository fs_repo;

    public FavoritesService(FavoritesRepository fs_repo)
    {
      this.fs_repo = fs_repo;
    }
  }
}