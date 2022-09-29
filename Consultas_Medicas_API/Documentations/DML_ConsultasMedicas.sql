USE ConsultasMedicasDB;
GO

/* Inserindo dados para as colunas das tabelas abaixo*/
/* No qual o médico sempre será com o Id 1, e o paciente, com o Id 2. */

		-- Tipos de Usuários existentes
INSERT INTO TipoUsuario(Tipo) VALUES ('Médico');
INSERT INTO TipoUsuario(Tipo) VALUES ('Paciente');
INSERT INTO TipoUsuario(Tipo) VALUES ('Administrador');
GO
		-- Dados dos Usuários
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Hagrid', 'hagrid@email.com', '123456', 1);  -- Medico
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Snape', 'snape@email.com', '678910', 1);    -- Medico
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Minerva', 'minerva@email.com', '1102938', 1); -- Medico
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Harry', 'harry@email.com', '147258', 2);   -- Paciente
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Hermione', 'hermione@email.com', '9876543', 2);  -- Paciente
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Rony', 'rony@email.com', '6549873', 2);  -- Paciente
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Malfoy', 'malfoy@email.com', '6549873', 2);  -- Paciente
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Dumbledore', 'director@email.com', '54656465', 3);  -- Administrador

GO
	
		-- Dados da Especialidade 
INSERT INTO Especialidade(Categoria) VALUES ('Cardiologista'),
										    ('Clínico Geral'),
											('Dermatologista'),
											('Endocrinologista'),
											('Otorrinolaringologista');
GO

		-- Dados do Administrador
INSERT INTO Administrador(TipoAcesso, CPF, IdUsuario) VALUES ('Admin', '12345678912', 8)											
		
GO

		-- Dados do Médico
INSERT INTO Medico(CRM, IdEspecialidade, IdUsuario) VALUES ('651466213', 1, 3),
														   ('651466213', 5, 1),
														   ('651466213', 2, 2);
GO

		-- Dados do Paciente
INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('684656356', '1980-05-08T12:50:36', 1, 4 );
INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('91452641561', '1983-02-23T05:14:01', 1, 5 );
INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('46514951209', '1985-05-1T10:03:52', 1, 6 );
INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('1645142194', '1995-04-1T12:00:00', 1, 7 );
GO

	
		-- Dados das Consultas
INSERT INTO Consultas(DataHora, IdMedico, IdPaciente) VALUES (GETDATE(), 1, 1);
INSERT INTO Consultas(DataHora, IdMedico, IdPaciente) VALUES ('2022-11-08T12:00:00', 2, 3);
INSERT INTO Consultas(DataHora, IdMedico, IdPaciente) VALUES ('2022-10-15T12:00:00', 3, 4);
GO


