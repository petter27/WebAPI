using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Custom;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // solo usuarios autorizados, si hay multiples formas de autorizacion
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly DbpruebaContext _dbpruebaContext;
        public ProductoController(DbpruebaContext dbpruebaContext)
        {
            _dbpruebaContext = dbpruebaContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _dbpruebaContext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }
    }
}
