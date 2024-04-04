using System.ComponentModel.DataAnnotations;

namespace SocialUniftec.Models
{
	public class UsuarioCadastroModel
	{
		[Required(ErrorMessage = "O nome não foi informado. Por favor, verifique!")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O sobrenome não foi informado. Por favor, verifique!")]
		public string Sobrenome { get; set; }

		[Required(ErrorMessage = "O e-mail não foi informado. Por favor, verifique!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "A senha não foi informada. Por favor, verifique!")]
		public string Senha { get; set; }

		[Required(ErrorMessage = "A senha repetida não foi informada. Por favor, verifique!")]
		public string SenhaRepetida { get; set; }

		[Required(ErrorMessage = "A data comemorativa não foi informada. Por favor, verifique!")]
		public DateTime DataComemorativa { get; set; }

	}
}
