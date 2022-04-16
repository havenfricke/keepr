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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService kvs)
    {
      _vks = kvs;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<VaultKeep>> CreateKV([FromBody] VaultKeep data)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = user.Id;
        VaultKeep keepVault = _vks.CreateKV(data);
        return Created($"api/vaultkeeps/{keepVault.Id}", keepVault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> RemoveKV(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        return _vks.RemoveKV(user.Id, id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}