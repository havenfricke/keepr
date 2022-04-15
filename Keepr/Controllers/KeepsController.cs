using Keepr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Keepr.Models;
using CodeWorks.Auth0Provider;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = user.Id;
        Keep keep = _ks.CreateKeep(keepData);
        keep.Creator = user;
        return Created($"api/keeps/{keep.Id}", keepData);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }
  }
}