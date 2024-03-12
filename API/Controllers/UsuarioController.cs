using Data;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenServicio _tokenServicio;

        public UsuarioController(ApplicationDbContext db, ITokenServicio tokenServicio)
        {
            _db = db;
            _tokenServicio = tokenServicio;
        }
        [Authorize]
        [HttpGet] //Api/usuario
        public async Task<ActionResult<IEnumerable<USUARIO>>> GetUsuarios()
        {

            var usuarios = await _db.Usuarios.ToArrayAsync();
            return Ok(usuarios);

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<USUARIO>> GetUsuario(int id)
        {

            var usuario = await _db.Usuarios.FindAsync(id);
            return Ok(usuario);

        }

        [HttpPost("registro")] // POST: api/usuario/registro
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.UserName)) return BadRequest("User Name ya se encuentra registrado");
            using var hmac = new HMACSHA512();
            var usuario = new USUARIO
            {

                UserName = registroDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                PasswordSalt = hmac.Key
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioDto>> login(LoginDto loginDto) {

            var usuario = await _db.Usuarios.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (usuario == null) return Unauthorized("Usuario no Valido");
            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Pasword nos es correcto"); 
            }
            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _db.Usuarios.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
