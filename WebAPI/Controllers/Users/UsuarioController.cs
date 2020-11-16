using System;
using System.Collections.Generic;
using Domain.ClassesAuxiliadoras;
using Domain.src.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Users
{
    [ApiController]
    [Route("Brasileirao2020/[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioServices usuarioServices;

        public UsuariosController()
        {
            usuarioServices = new UsuarioServices();
        }

        [HttpGet("{idUser}")]
        public Usuario GetUsuario(Guid idUser)
        {
            return usuarioServices.ObterUsuario(idUser);
        }

        [HttpPost("Login")]
        public ActionResult<dynamic> Authenticate()
        {
            StringValues usuarioId;

            if (!Request.Headers.TryGetValue("UsuarioId", out usuarioId))
            {
                return Unauthorized();
            }
            var usuarioObtido = usuarioServices.ObterUsuario(Guid.Parse(usuarioId));

            if (usuarioObtido == null)
            {
               return Unauthorized(); 
            }

            var token = TokenServices.GerarToken(usuarioObtido);

            return new
            {
                usuario = usuarioObtido,
                token = token
            };
        }

        [HttpGet("Proibido")]
        [Authorize(Roles = "cbf")]
        public string Authenticado() => "Authenticado";
 
 
        [HttpPost]
        public IActionResult PostUsuario(Usuario request)
        {
            if (!usuarioServices.AdicionarUsuario(request.Name, request.Senha, request.Tipo))
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpDelete("{idUser}")]
        public IActionResult DeleteUsuario(Guid idUser)
        {
            usuarioServices.RemoverUsuario(idUser);
            return NoContent();
        }
    }
}