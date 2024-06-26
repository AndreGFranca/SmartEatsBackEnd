﻿using SmartEats.Enums.Users;
using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Users
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MinLength(1, ErrorMessage = "Campo nome requer no minimo 1 caracter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo cpf é obrigatório")]
        [Length(14, 14, ErrorMessage = "Campo cpf requer no minimo 14 caracteres")]
        public string CPF { get; set; }
        public string UserName { get; set; }

        public bool Ativo { get; set; } = true;
        public int Id_Company { get; set; }
        public TypeUser TypeUser { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais")]
        public string RePassword { get; set; }
    }
}
