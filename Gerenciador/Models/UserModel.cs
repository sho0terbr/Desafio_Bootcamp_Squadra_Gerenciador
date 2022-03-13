using Gerenciador.Enums;

namespace Gerenciador.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public CargosEnum Cargo { get; set; }
        public UserModel(string usuario, string senha, CargosEnum cargo)
        {
            Usuario = usuario;
            Senha = senha;
            Cargo = cargo;
        }
    }
}
