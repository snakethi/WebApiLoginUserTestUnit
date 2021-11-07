CREATE TABLE [dbo].[TbUser]
(
	[IdUser] INT  IDENTITY (1, 1) NOT NULL, 
    [NomeCompleto] VARCHAR(50) NOT NULL, 
    [Nascimento] DATETIME NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [Login] VARCHAR(50) NOT NULL, 
    [Senha] VARCHAR(150) NOT NULL,
)
