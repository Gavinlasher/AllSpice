using System;
using System.Data;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;

    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Step Create(Step stepData)
    {
      string sql = @"
    INSERT INTO steps
    (position,body,recipeId)
    VALUE
    (@Position,@Body,@RecipeId);
    SELECT LAST_INSERT_ID();
    ";
      int id = _db.ExecuteScalar<int>(sql, stepData);
      stepData.Id = id;
      return stepData;
    }

    internal void Edit(Step og)
    {
      string sql = @"
      UPDATE steps
      SET
       position = @Position,
       body = @Body,
       recipeId = @RecipeId
      WHERE id = @Id;";
      _db.Execute(sql, og);
      // return og;
    }

    internal Step GetById(int id)
    {
      string sql = "SELECT * FROM steps WHERE id = @id;";
      return _db.QueryFirstOrDefault<Step>(sql, new { id });
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
  }
}