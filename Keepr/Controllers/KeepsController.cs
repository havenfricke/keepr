using Keepr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Keepr.Models;
using CodeWorks.Auth0Provider;
using System.Collections.Generic;

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

    [HttpGet]

    public ActionResult<List<Keep>> GetAllKeeps()
    {
      try
      {
        List<Keep> keeps = _ks.GetAllKeeps();
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<Keep> GetKeepById(int id)
    {
      try
      {
        Keep keep = _ks.GetKeepById(id);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep updateData, int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        updateData.CreatorId = user.Id;
        Keep keep = _ks.UpdateKeep(updateData);
        updateData.Creator = user;
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> RemoveKeep(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        return _ks.RemoveKeep(id, user.Id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}