using Keepr.Services;
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

  }
}