using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrabalhoWeb_25940.Models
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; }

        public bool Aprovado { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // AQUI ESTÁ A NOVIDADE: Guarda o ID do Formador que criou este workshop
        public int? FormadorId { get; set; }

        public List<Participante> Participantes { get; set; } = new List<Participante>();
    }
}