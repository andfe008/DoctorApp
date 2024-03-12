using API.Errores;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entidades;

namespace API.Controllers
{
    public class ErrorTestController : BaseApiController
    {
        private readonly ApplicationDbContext _db;

        public ErrorTestController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorize()
        {
            return "No Autorizado";
        }

        [HttpGet("not-found")]
        public ActionResult<USUARIO> GetNotFount()
        {
            var objeto =  _db.Usuarios.Find(-1);
            if (objeto == null) return NotFound( new ApiErrorMensaje(404));
            return objeto;
        }

    
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var objeto = _db.Usuarios.Find(-1);
            var objetostring = objeto.ToString();
            return objetostring;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest(new ApiErrorMensaje(401));
        }
    }
}
