using SocialUniftec.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Application.Application
{
    public class UsuarioApplication
    {

        UsuarioRepository usuarioRepository;

        public UsuarioApplication()
        {
            string strConexao = "User ID=postgres;Password=123456789;Host=localhost;Port=5432;Database=socialuniftec;"; ;
            this.usuarioRepository = new UsuarioRepository(strConexao);
        }



    }
}
