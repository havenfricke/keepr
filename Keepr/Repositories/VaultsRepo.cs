using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;


namespace Keepr.Repositories
{
  public class VaultsRepo
  {
    private readonly IDbConnection _db;

    public VaultsRepo(IDbConnection db)
    {
      _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @isPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal Vault GetVaultById(int id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM
      vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE
      v.id = @id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal Vault UpdateVault(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @isPrivate
      WHERE
      id = @id;
      ";
      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected > 0)
      {
        return original;
      }
      throw new Exception("Sql Error on UpdateVaults");
    }

    internal ActionResult<string> RemoveVault(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE
      id = @id
      LIMIT
      1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Vault Removed";
      }
      throw new Exception("Sql Error on RemoveVault - Repo Layer");
    }

    internal List<KeepVM> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
       SELECT
        kv.*,
        kv.vaultId AS VaultKeepId,
        k.*,
        a.*
       FROM vaultkeeps kv
       JOIN keeps k ON k.id = kv.keepId
       JOIN accounts a ON k.creatorId = a.id
       WHERE
        kv.vaultId = @vaultId;
      ";
      return _db.Query<KeepVM, Keep, Account, KeepVM>(sql, (kv, k, a) =>
      {
        kv.Creator = a;
        kv.Name = k.Name;
        kv.Img = k.Img;
        kv.Views = k.Views;
        kv.Kept = k.Kept;
        kv.Description = k.Description;
        return kv;
      }, new { vaultId }).ToList();
    }
  }
}