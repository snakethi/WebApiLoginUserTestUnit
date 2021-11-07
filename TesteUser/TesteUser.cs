using Dominio.Interfaces;
using Dominio.Repositorio;
using Entidades.Entidades;
using Entidades.Retorno;
using Infraestrutura.Configuracoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteUser
{
    [TestClass]
    public class TesteUser
    {
        IUser _user = new User();
        ICry _cry = new Cry();
        private string conexao = "Data Source=DESKTOP-BVKCR2J\\SQLDB01;Initial Catalog=DbUser;User Id=sistema;Password=26@26;";

        [TestMethod]
        public void Cadastro()
        {

            Contexto.connect = conexao;

            var user = new TbUser
            {
                IdUser = 0,
                NomeCompleto = "Thiago Guimarães Botaro",
                Nascimento = Convert.ToDateTime("11/08/1988"),
                Email = "teste@hotmail.com",
                Login = "teste",
                Senha = _cry.Criptografa("123456").Result.ToString()
            };

            var resultado = _user.Cadastro(user);

            if(resultado.Result.Retorno == "Cadastrado com Sucesso!")
            {
                Assert.AreEqual("Cadastrado com Sucesso!", resultado.Result.Retorno);
            }
            else
            {
                if (resultado.Result.Retorno == "Encontrou!")
                {
                    if(!string.IsNullOrWhiteSpace(resultado.Result.Sugestao1) || !string.IsNullOrWhiteSpace(resultado.Result.Sugestao2) || !string.IsNullOrWhiteSpace(resultado.Result.Sugestao3))
                    {
                        Assert.AreEqual("Encontrou!", resultado.Result.Retorno);
                    }
                    else
                    {
                        Assert.Fail("Erro ao Cadastrar");
                    }
                    
                }
                else
                {
                    Assert.Fail("Erro ao Cadastrar");
                }
            }
        }

        [TestMethod]
        public void GetUserByLogin()
        {
            Contexto.connect = conexao;

            string Login = "teste";

            var resultado = _user.GetUserByLogin(Login).Result.Login;

            Assert.AreEqual("teste", resultado);
        }

        [TestMethod]
        public void Logar()
        {

            Contexto.connect = conexao;

            string Login = "teste";
            string Senha = _cry.Criptografa("123456").Result.ToString();

            var resultado = _user.Logar(Login, Senha).Result;

            Assert.AreEqual(true, resultado);
        }

        [TestMethod]
        public void GetAllUser()
        {
            Contexto.connect = conexao;

            string Login = "novo";

            var users = _user.GetAllUser().Result.ToList();
            bool retorno = true;

            if(users.Count == 0)
            {
                retorno = false;
            }

            Assert.AreEqual(true, retorno);
        }


    }
}
