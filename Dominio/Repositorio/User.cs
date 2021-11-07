using Dapper;
using Dominio.Interfaces;
using Entidades.Entidades;
using Entidades.Retorno;
using Infraestrutura.Configuracoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorio
{
    public class User : IUser
    {
        public async Task<bool> Logar(string Login, string Senha)
        {
            try
            {
                using (var conn = Contexto.Conexao())
                {
                    string query = $"select * from TbUser where Login = '{Login}' and Senha = '{Senha}' ";
                    var user = (await conn.QueryAsync<TbUser>(sql: query)).ToList();

                    if(user.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    

                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<SugestaoUser> Cadastro(TbUser user)
        {
            using (var conn = Contexto.Conexao())
            {
                var res = VerificaExistenciaLogin(user.Login);

                if (res.Result.Retorno == "Não Encontrou!")
                {
                    var res2 = VerificaEmail(user.Email);

                    if (res2.Retorno == "Valido")
                    {
                        conn.Execute("PRC_INS_USER", user, commandType: CommandType.StoredProcedure);
                        res.Result.Retorno = "Cadastrado com Sucesso!";
                        return res.Result;
                    }

                    return res2;
                }
                else
                {
                    return res.Result;
                }
            }
        }

        public async Task<SugestaoUser> VerificaExistenciaLogin(string Login)
        {
            using (var conn = Contexto.Conexao())
            {
                string query = $"select * from TbUser where Login = '{Login}'";
                var user = (await conn.QueryAsync<TbUser>(sql: query)).ToList();

                if (user.Count == 0)
                {
                    var retorno = new SugestaoUser
                    {
                        Retorno = "Não Encontrou!"
                    };
                    return retorno;
                }
                else
                {
                    return CriaSugestoes(user.ElementAt(0).NomeCompleto, user.ElementAt(0).Nascimento);
                }
            }
        }

        public SugestaoUser CriaSugestoes(string nome , DateTime data)
        {
            try
            {
                bool controle = true;
                int nSugestoes = 1;
                string sugestao1 = "";
                string sugestao2 = "";
                string sugestao3 = "";
                string aux = "";
                int combinacao = 0;
                string[] Nomes = nome.Split(' ');

                while (controle)
                {
                    switch (combinacao)
                    {
                        case 0:
                            aux = Nomes[0].ToString() + data.Day.ToString();
                            combinacao++;
                            break;
                        case 1:
                            aux = Nomes[2].ToString() + data.Day.ToString();
                            combinacao++;
                            break;
                        case 2:
                            aux = Nomes[2].ToString() + Nomes[0].ToString();
                            combinacao++;
                            break;
                        case 3:
                            aux = Nomes[0].ToString() + data.Year.ToString();
                            combinacao++;
                            break;
                        case 4:
                            aux = Nomes[2].ToString() + data.Year.ToString();
                            combinacao++;
                            break;
                        case 5:
                            aux = Nomes[0].ToString() + Nomes[2].ToString() + data.Year.ToString();
                            combinacao++;
                            break;
                        case 6:
                            aux = Nomes[2].ToString() + Nomes[0].ToString() + data.Year.ToString();
                            combinacao++;
                            nSugestoes = 4;
                            break;
                    }

                    using (var conn = Contexto.Conexao())
                    {
                        string query = $"select * from TbUser where Login = '{aux}'";
                        var res = (conn.QueryAsync<TbUser>(sql: query)).Result;
                        var teste = res.ToList();

                        if (teste.Count == 0)
                        {
                            switch (nSugestoes)
                            {
                                case 1:
                                    sugestao1 = aux;
                                    nSugestoes++;
                                    break;
                                case 2:
                                    sugestao2 = aux;
                                    nSugestoes++;
                                    break;
                                case 3:
                                    sugestao3 = aux;
                                    nSugestoes++;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    if (nSugestoes == 4)
                    {
                        controle = false;
                    }
                }

                var retorno = new SugestaoUser
                {
                    Retorno = "Encontrou!",
                    Sugestao1 = sugestao1,
                    Sugestao2 = sugestao2,
                    Sugestao3 = sugestao3
                };
                return retorno;
            }
            catch(Exception ex)
            {
                var retorno = new SugestaoUser
                {
                    Retorno = "Encontrou!",
                    Sugestao1 = "",
                    Sugestao2 = "",
                    Sugestao3 = ""
                };
                return retorno;
            }
        }

        public SugestaoUser VerificaEmail(string Email)
        {
            string mensagem = "Valido";


            if(!Email.Contains("@"))
            {
                mensagem = "Email Invalido!";
            }

            if (!Email.Contains(".com"))
            {
                mensagem = "Email Invalido!";
            }

            var retorno = new SugestaoUser
            {
                Retorno = mensagem,
                Sugestao1 = "",
                Sugestao2 = "",
                Sugestao3 = ""
            };
            return retorno;
        }

        public async Task<IEnumerable<TbUser>> GetAllUser()
        {
            try
            {
                using (var conn = Contexto.Conexao())
                {
                    string query = $"select * from TbUser";
                    return (await conn.QueryAsync<TbUser>(sql: query)).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<TbUser> GetUserByLogin(string Login)
        {
            try
            {
                using (var conn = Contexto.Conexao())
                {
                    string query = $"select * from TbUser where Login = '{Login}'";
                    var user = (await conn.QueryAsync<TbUser>(sql: query)).ToList();
                    return user.ElementAt(0);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
