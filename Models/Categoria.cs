using System.Collections.Generic;

namespace Filmes.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public int VideoId { get; set; }
        public ICollection<Video>? Video { get; set; }
    }
}