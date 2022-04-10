using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> GetAll()
    {
      string sql = @"
      SELECT
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a WHERE a.id = r.creatorId;
      ";
      // NOTE     first (g) second (a) third (return type)
      return _db.Query<Recipe, Account, Recipe>(sql, (r, account) =>
      {
        // NOTE this is the populate creator
        r.Creator = account;
        return r;
      }).ToList();
    }

    internal Recipe Create(Recipe recipeData)
    {
      string sql = @"
    INSERT INTO recipes
    (title,subtitle,category,creatorId,picture)
    VALUE
    (@Title,@Subtitle,@Category,@CreatorId,@Picture);
    SELECT LAST_INSERT_ID();
    ";
      int id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }

    internal Recipe GetById(int id)
    {
      string sql = @"
      SELECT 
        r.*,
        a.* 
      FROM recipes r
      JOIN accounts a ON r.creatorId = a.id
      WHERE r.id = @id;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (r, a) =>
      {

        r.Creator = a;
        return r;
      }, new { id }).FirstOrDefault();
    }

    internal string Remove(int id)
    {
      string sql = @"
        DELETE FROM recipes WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "delorted";
      }
      throw new Exception("could not delete");

    }

    internal List<RecipeFavoriteModel> GetFavoritesByAccId(string id)
    {
      string sql = @"
          SELECT
            a.*,
            f.*,
            r.*
          FROM favorites f
          JOIN recipes r ON f.recipeId = r.id
          JOIN accounts a ON r.creatorId = a.id
          WHERE f.accountId = @id;
      ";
      List<RecipeFavoriteModel> recipes = _db.Query<Account, Favorite, RecipeFavoriteModel, RecipeFavoriteModel>(sql, (a, f, r) =>
      {
        r.Creator = a;

        r.FavoriteId = f.Id;
        return r;
      }, new { id }).ToList<RecipeFavoriteModel>();
      return recipes;
    }

    internal List<Step> GetSteps(int id)
    {
      string sql = @"
      SELECT
     s.*
     
      FROM steps s
       WHERE s.recipeId = @id;
      ";
      return _db.Query<Step>(sql, new { id }).ToList();
    }

    internal List<Ingredient> GetIngredients(int id)
    {
      string sql = @"
      SELECT
     i.*
     
      FROM ingredients i
       WHERE i.recipeId = @id;
      ";
      return _db.Query<Ingredient>(sql, new { id }).ToList();

    }

    internal void Edit(Recipe og)
    {
      string sql = @"
      UPDATE recipes
      SET
       title = @Title,
       subtitle = @Subtitle,
       category = @Category,
       picture = @Picture
      WHERE id = @Id;";
      _db.Execute(sql, og);
    }
  }
}