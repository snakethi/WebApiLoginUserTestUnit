CREATE PROCEDURE [dbo].[PRC_INS_USER]
@IdUser int = 0,
@NomeCompleto varchar(50),
@Nascimento Datetime,
@Email varchar(50),
@Login varchar(50),
@Senha varchar(150)

as


insert into TbUser values (@NomeCompleto, @Nascimento, @Email, @Login, @Senha)
