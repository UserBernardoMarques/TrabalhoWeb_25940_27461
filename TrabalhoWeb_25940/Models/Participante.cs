using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrabalhoWeb_25940.Models
{
    public class Participante
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Idade { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "É Aluno?")]
        public bool IsAluno { get; set; }

        [Display(Name = "É Formador?")]
        public bool IsFormador { get; set; }

        // AQUI ESTÁ O SEGREDO: Controla se o Admin já aceitou a conta!
        [Display(Name = "Conta Aprovada?")]
        public bool Aprovado { get; set; } = false;

        public List<Workshop> Workshops { get; set; } = new List<Workshop>();
    }
}