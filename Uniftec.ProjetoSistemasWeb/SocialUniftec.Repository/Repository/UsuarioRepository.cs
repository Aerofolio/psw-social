using Npgsql;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Repository.Repository
{
	public class UsuarioRepository : IUsuarioRepository
	{
		public void Alterar(Usuario usuario)
		{
			throw new NotImplementedException();
		}

		public void Excluir(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Inserir(Usuario usuario)
		{
			using var con = new NpgsqlConnection($"User ID=postgres;Password=;Host=localhost;Port=5432;Database=socialuniftec;");
			con.Open();
			
			using var cmd = new NpgsqlCommand(
				@"INSERT INTO public.usuario
					(id, nome, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo)
					VALUES(@id, @nome, @sobrenome, @senha, @datacomemorativa, @sexo, @bio, @fotodeperfil, @cidade, @uf, @telefone, @documento, @tipo);",
				con);
				
			cmd.Parameters.AddWithValue("id", usuario.Id);
			cmd.Parameters.AddWithValue("nome", usuario.Nome);
			cmd.Parameters.AddWithValue("sobrenome", usuario.Sobrenome);
			cmd.Parameters.AddWithValue("senha", usuario.Senha);
			cmd.Parameters.AddWithValue("datacomemorativa", usuario.DataComemorativa);
			cmd.Parameters.AddWithValue("sexo", (int)usuario.Sexo);
			cmd.Parameters.AddWithValue("bio", usuario.Bio);
			cmd.Parameters.AddWithValue("fotodeperfil", usuario.FotoPefil);
			cmd.Parameters.AddWithValue("cidade", usuario.Cidade);
			cmd.Parameters.AddWithValue("uf", (int)usuario.Uf);
			cmd.Parameters.AddWithValue("telefone", usuario.Telefone);
			cmd.Parameters.AddWithValue("documento", usuario.Documento);
			cmd.Parameters.AddWithValue("tipo", (int)usuario.Tipo);
			cmd.ExecuteNonQuery();
			
			foreach(var amizade in usuario.Amigos)
			{
				cmd.Parameters.Clear();
				cmd.CommandText = @"
					INSERT INTO public.amizade
						(idusuario, idusuarioamigo)
						VALUES(@idusuario, @idusuarioamigo);";
					
				cmd.Parameters.AddWithValue("idusuario", usuario.Id);
				cmd.Parameters.AddWithValue("idusuarioamigo", amizade.Id);
				cmd.ExecuteNonQuery();
			}
		}

		public Usuario Procurar(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<Usuario> ProcurarTodos()
		{
			throw new NotImplementedException();
		}
	}
}
