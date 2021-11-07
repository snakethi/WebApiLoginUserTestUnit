using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class TbUser
    {
        public int IdUser { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime Nascimento { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

    }
}
