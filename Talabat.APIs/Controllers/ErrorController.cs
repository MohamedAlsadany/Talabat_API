using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{
	[Route("Error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code) 
		{ 
		return NotFound(new ApiResponse(code));
		}
	}
}
