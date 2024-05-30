using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha {  get; set; }
        public DateTime DataComemorativa { get; set; }
        public TipoSexo Sexo { get; set; }
        public string Bio {  get; set; }
        public Byte[] FotoPefil { get; set; }
        public string Cidade { get; set; }
        public EstadosBrasil Uf { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }
        public TipoPessoa Tipo { get; set; }
        public List<Usuario> Amigos { get; }

        public Usuario() {
            this.Id = Guid.NewGuid();
            this.Nome = string.Empty;
            this.Sobrenome = string.Empty;
            this.Senha = string.Empty;
            this.DataComemorativa = DateTime.Now;
            this.Sexo = TipoSexo.Masculino;
            this.Bio = string.Empty;
            this.FotoPefil = [];
            this.Cidade = string.Empty;
            this.Uf = EstadosBrasil.RS;
            this.Telefone = string.Empty;
            this.Documento = string.Empty;
            this.Tipo = TipoPessoa.Fisica;
            this.Amigos = [];
        }

        public void AdicionarAmigo(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Usuário não informado!");
            }

            this.Amigos.Add(usuario);
        }

        public void RemoverAmigo(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Usuário não informado!");
            }

            this.Amigos.Remove(usuario);
            
        }

    }

    public enum EstadosBrasil
    {
        [Display(Name = "Acre")]
        AC,
        [Display(Name = "Alagoas")]
        AL,
        [Display(Name = "Amapá")]
        AP,
        [Display(Name = "Amazonas")]
        AM,
        [Display(Name = "Bahia")]
        BA,
        [Display(Name = "Ceará")]
        CE,
        [Display(Name = "Distrito Federal")]
        DF,
        [Display(Name = "Espírito Santo")]
        ES,
        [Display(Name = "Goiás")]
        GO,
        [Display(Name = "Maranhão")]
        MA,
        [Display(Name = "Mato Grosso")]
        MT,
        [Display(Name = "Mato Grosso do Sul")]
        MS,
        [Display(Name = "Minas Gerais")]
        MG,
        [Display(Name = "Pará")]
        PA,
        [Display(Name = "Paraíba")]
        PB,
        [Display(Name = "Paraná")]
        PR,
        [Display(Name = "Pernambuco")]
        PE,
        [Display(Name = "Piauí")]
        PI,
        [Display(Name = "Rio de Janeiro")]
        RJ,
        [Display(Name = "Rio Grande do Norte")]
        RN,
        [Display(Name = "Rio Grande do Sul")]
        RS,
        [Display(Name = "Rondônia")]
        RO,
        [Display(Name = "Roraima")]
        RR,
        [Display(Name = "Santa Catarina")]
        SC,
        [Display(Name = "São Paulo")]
        SP,
        [Display(Name = "Sergipe")]
        SE,
        [Display(Name = "Tocantins")]
        TO
    }

    public enum TipoPessoa
    {
        Fisica = 0,
        Juridica = 1
    }

    public enum TipoSexo
    {
        Masculino = 0, 
        Feminino = 1
    }
}
