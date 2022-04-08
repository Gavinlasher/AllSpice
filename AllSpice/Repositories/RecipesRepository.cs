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