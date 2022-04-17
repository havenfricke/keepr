using System;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Repositories
{
  public class VaultKeepsRepo
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepo(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep CreateKV(VaultKeep data)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (keepId, vaultId, creatorId)
      VALUES
      (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();
      UPDATE
      keeps
      SET
      kept = kept + 1
      WHERE
      id = @keepId;
      ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    internal VaultKeep GetVKById(int id)
    {
      string sql = @"
      SELECT
      *
      FROM
      vaultkeeps
      WHERE
      id = @id
      ";
      return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
    }

    internal ActionResult<string> RemoveVK(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps
      WHERE
      id = @id
      LIMIT
      1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "VaultKeep Removed";
      }
      throw new Exception("Sql Error at RemoveVK - repo layer");
    }
  }
}