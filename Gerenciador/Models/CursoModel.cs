using Gerenciador.Enums;

namespace Gerenciador.Models
{
    public class CursoModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Duracao { get; set; }
        public StatusCursoEnum Status { get; set; }
    }
}
