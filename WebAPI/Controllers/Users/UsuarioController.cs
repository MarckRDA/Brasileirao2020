using System;
using Domain.ClassesAuxiliadoras;
using Domain.src.ClassesAuxiliadoras;
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
        public ActionResult<dynamic> Authenticate(UsuarioRequest request)
        {
            StringValues usuarioId;
            var md5 = new CriadorMD5();

            if (!Request.Headers.TryGetValue("UserId", out usuarioId))
            {
                return Unauthorized();
            }
            var usuarioObtido = usuarioServices.ObterUsuario(Guid.Parse(usuarioId));
            if (usuarioObtido.Name != request.Nome || !md5.ComparaMD5(request.Senha, usuarioObtido.Senha))
            {
                return Unauthorized();
            }

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

 
        [HttpPost]
        public IActionResult PostUsuario(UsuarioRequest request)
        {
            var md5 = new CriadorMD5();
            var senhaCriptografada = md5.RetornarMD5(request.Senha);
            var usuarioAAdicionar = usuarioServices.AdicionarUsuario(request.Nome, senhaCriptografada, request.Tipo);

            if (!usuarioAAdicionar.isValid)
            {
                return Unauthorized();
            }

            return Ok(usuarioAAdicionar.id);
        }

     }
}