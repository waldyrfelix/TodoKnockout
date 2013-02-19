using System.ComponentModel.DataAnnotations;

namespace TodoKnock.Controllers.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string Titulo { get; set; }
        public bool Terminada { get; set; }
    }
}