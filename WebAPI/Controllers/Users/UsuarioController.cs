using System.Collections.Generic;
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

        [HttpGet]
        public List<Usuario> GetUsuarios()
        {
            return usuarioServices.ObterUsuarios();
        }
    }
}