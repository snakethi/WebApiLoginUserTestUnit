using Entidades.Entidades;
using Entidades.Retorno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUser
    {
        Task<bool> Logar(string Login, string senha);
        Task<SugestaoUser> Cadastro(TbUser user);
        Task<SugestaoUser> VerificaExistenciaLogin(string Login);
        SugestaoUser CriaSugestoes(string nome, DateTime data);
        Task<IEnumerable<TbUser>> GetAllUser();
        Task<TbUser> GetUserByLogin(string Login);
    }
}
