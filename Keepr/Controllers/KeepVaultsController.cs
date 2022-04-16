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
  public class KeepVaultsController : ControllerBase
  {
    private readonly KeepVaultsService _kvs;

    public KeepVaultsController(KeepVaultsService kvs)
    {
      _kvs = kvs;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<KeepVault>> CreateKV([FromBody] KeepVault data)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = user.Id;
        KeepVault keepVault = _kvs.CreateKV(data);
        return Created($"api/vaultkeeps/{keepVault.Id}", keepVault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}