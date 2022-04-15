using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Repositories
{
  public class KeepsRepo
  {
    private readonly IDbConnection _db;

    public KeepsRepo(IDbConnection db)
    {
      _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, kept, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
      SELECT
      *
      FROM
      keeps
      WHERE
      id = @id;
      ";
      return _db.Query<Keep>(sql, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetAllKeeps()
    {
      string sql = @"
      SELECT 
      *
      FROM 
      keeps;
      ";
      return _db.Query<Keep>(sql).ToList();
    }

    internal Keep UpdateKeep(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img
      WHERE
      id = @id;
      ";
      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected > 0)
      {
        return original;
      }
      throw new Exception("Sql Error on UpdateKeep - Repo Layer");
    }

    internal Keep IncreaseKeepViews(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET
      views = @Views
      WHERE
      id = @id
      ";
      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected > 0)
      {
        return original;
      }
      throw new Exception("Sql Error on IncreaseKeepViews - Repo Layer");
    }

    internal ActionResult<string> RemoveKeep(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE
      id = @id
      LIMIT
      1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Keep Removed";
      }

      throw new Exception("Sql Error on RemoveKeep - Repo Layer");
    }
  }
}