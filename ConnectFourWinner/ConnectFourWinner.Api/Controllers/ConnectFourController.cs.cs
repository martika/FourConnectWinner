using ConnectFourWinner.Api.Helpers;
using ConnectFourWinner.Domain.Entities;
using ConnectFourWinner.Domain.Entities.Models;
using ConnectFourWinner.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace ConnectFourWinner.Api.Controllers
{
    
    [Route("api/connect-four")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]    
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ConnectFourController : ControllerBase
    {
        private readonly IServiceConnectFour connectFourService;

        public ConnectFourController(IServiceConnectFour connectFourService)
        {
            this.connectFourService = connectFourService;            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Solution))]
        public IActionResult Get([FromQuery] InputModel inputModel)
        {
            try
            {
                var res = connectFourService.GetSolution(inputModel.Input, inputModel.Width, inputModel.Height);
                if (res != null)
                {
                    Log.Logger.Information($"Return OK solution");
                    return Ok(res);
                }
                else
                {
                    Log.Logger.Warning($"No solution");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {               
                Log.Logger.Error($"Bad Request getting winner: {ex.Message}");                        
                return BadRequest(ErrorHelper.GetErrorResponse(ex));
            }
        }      
     
    }
}
