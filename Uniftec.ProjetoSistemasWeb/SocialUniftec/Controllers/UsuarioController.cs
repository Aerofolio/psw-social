﻿using Microsoft.AspNetCore.Mvc;
using SocialUniftec.Models;

namespace SocialUniftec.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login) {
            
            if (ModelState.IsValid)
            {
                return Redirect("Postagem/Feed");
			} 
            else
            {
				return this.Index();
			}

        }

        public IActionResult Cadastrar() 
        { 
            return View();
        }

        [HttpPost]
		public IActionResult Cadastrar(UsuarioCadastroModel usuarioCadastro)
		{

            if (usuarioCadastro.Senha != null && usuarioCadastro.SenhaRepetida != null && !usuarioCadastro.Senha.Equals(usuarioCadastro.SenhaRepetida))
            {
                ModelState.AddModelError("senhainvalida", "As senhas não coincidem!");
            }

			if (ModelState.IsValid)
			{
				return Redirect("Postagem/Feed");
			}
			else
			{
				return View("Cadastrar");
			}
		}

	}

}