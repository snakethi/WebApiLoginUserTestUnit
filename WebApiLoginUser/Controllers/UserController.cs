using Dominio.Interfaces;
using Entidades.Entidades;
using Entidades.Retorno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLoginUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _user;
        private ICry _cry;
        public UserController(IUser user , ICry cry)
        {
            _user = user;
            _cry = cry;
        }

        [HttpGet("Cry/{Texto}")]
        public async Task<ActionResult<string>> Logar(string Texto)
        {
            try
            {
                var res = await _cry.Criptografa(Texto);
                return Ok(res);
            }
            catch
            {
                return BadRequest("Error ao Criptografar!");
            }
        }

        [HttpGet("Logar/{Login},{Senha}")]
        public async Task<ActionResult<bool>> Logar(string Login , string Senha)
        {
            try
            {
                var res = await _user.Logar(Login, Senha);
                return Ok(res);
            }
            catch
            {
                return BadRequest("Error ao Logar!");
            }
        }

        [HttpGet("TodosUsuarios")]
        public async Task<ActionResult<IAsyncEnumerable<TbUser>>> GetAllUser()
        {
            try
            {
                var res = await _user.GetAllUser();
                return Ok(res);
            }
            catch
            {
                return BadRequest("Error ao trazer todos os Usuarios!");
            }
        }

        [HttpGet("UsuarioPorLogin/{login}")]
        public async Task<ActionResult<IAsyncEnumerable<TbUser>>> GetUserByLogin(string login)
        {
            try
            {
                var res = await _user.GetUserByLogin(login);
                return Ok(res);
            }
            catch
            {
                return BadRequest("Error ao trazer todos os Usuarios!");
            }
        }

        [HttpPost("CadastrarUsuario")]
        public async Task<ActionResult<IAsyncEnumerable<SugestaoUser>>> Create(TbUser user)
        {
            try
            {
               var res = await _user.Cadastro(user);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao Cadastrar Usuario!");
            }
        }

    }
}
