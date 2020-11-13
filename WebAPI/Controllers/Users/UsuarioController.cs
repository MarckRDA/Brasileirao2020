using System;
using Domain.src.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Users
{
    [ApiController]
    [Route("Brasileirao2020/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServices usuarioServices;

        public UsuarioController()
        {
            usuarioServices = new UsuarioServices();
        }

        [HttpGet("{idUser}")]
        public Usuario GetUsuario(Guid idUser)
        {
            return usuarioServices.ObterUsuario(idUser);
        }

        [HttpPost]
        public IActionResult PostUsuario(UsuarioRequest request)
        {
            if (!usuarioServices.AdicionarUsuario(request.Nome, request.Senha))
            {
                return Unauthorized();
            }

            return NoContent();
        }

        [HttpDelete("{idUser}")]
        public IActionResult DeleteUsuario(Guid idUser)
        {
            usuarioServices.RemoverUsuario(idUser);
            return NoContent();
        }
    }
}