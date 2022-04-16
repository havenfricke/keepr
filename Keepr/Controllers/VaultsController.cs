using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }


    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = user.Id;
        Vault vault = _vs.CreateVault(vaultData);
        vault.Creator = user;
        return Created($"api/vaults/{vault.Id}", vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<Vault> GetVaultById(int id)
    {
      try
      {
        Vault vault = _vs.GetVaultById(id);
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault updateData, int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        updateData.CreatorId = user.Id;
        Vault vault = _vs.UpdateVault(updateData);
        updateData.Creator = user;
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> RemoveVault(int id)
    {
      try
      {

        Account user = await HttpContext.GetUserInfoAsync<Account>();
        return _vs.RemoveVault(user.Id, id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<KeepVM>> GetAllKV(int id)
    {
      try
      {
        List<KeepVM> keepVaults = _vs.GetAllKV(id);
        return Ok(keepVaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}