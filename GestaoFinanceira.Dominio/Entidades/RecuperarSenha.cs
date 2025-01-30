using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFinanceira.Dominio.Entidades
{
    public class RecuperarSenha
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Email { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataExpiracao { get; set; }
        public bool Ativo { get; set; }
        public bool SenhaAlterada { get; set; }

        public RecuperarSenha()
        {
            DataEnvio = DateTime.Now;
            DataExpiracao = DataEnvio.AddMinutes(10);
            Ativo = true;
            SenhaAlterada = false;
        }
    }
}
