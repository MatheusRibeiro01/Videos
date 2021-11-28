using System;
using System.ComponentModel.DataAnnotations;

namespace Filmes.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este Campo é obrigatório")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "Este Campo é obrigatório")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "Este Campo é obrigatório")]
        public string Url { get; set; }
        
        public Categoria Categoria { get; set; }
    }
}
