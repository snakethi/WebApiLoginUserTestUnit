using Dominio.Configuracao;
using Infraestrutura.Configuracoes;

namespace Dominio.Repositorio
{
    public class Configuracao : IConfiguracao
    {
       
        public void SetConexao(string contexto)
        {
            Contexto.connect = contexto;
        }
    }
}
