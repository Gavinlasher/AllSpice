
using System;
using System.Data;
using AllSpice.Models;
using Dapper;


namespace AllSpice.Services
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Ingredient GetById(int id)
    {
      string sql = "SELECT * FROM ingredients WHERE id = @id;";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }

    internal string Remove(int id)
    {
      string sql = @"
        DELETE FROM ingredients WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "delorted";
      }
      throw new Exception("could not delete");

    }



    internal Ingredient Create(Ingredient ingredientData)
    {
      string sql = @"
    INSERT INTO ingredients
    (name,quantity,recipeId)
    VALUE
    (@Name,@Quantity,@RecipeId);
    SELECT LAST_INSERT_ID();
    ";
      int id = _db.ExecuteScalar<int>(sql, ingredientData);
      ingredientData.Id = id;
      return ingredientData;
    }

    internal void Edit(Ingredient og)
    {
      string sql = @"
      UPDATE ingredinets
      SET
       name = @Name,
       quantity = @Quantity,
       recipeId = @RecipeId
      WHERE id = @Id;";
      _db.Execute(sql, og);
      // return og;
    }
  }
}