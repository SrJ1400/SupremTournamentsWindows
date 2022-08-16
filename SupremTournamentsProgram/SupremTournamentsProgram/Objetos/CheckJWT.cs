using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Objetos
{
    class CheckJWT
    {
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string IssuedAt { get; set; }
        public string Expiration { get; set; }
        public string usuario { get; set; }
        public string id_sesion_recibida { get; set; }
        public string id_sesion_actual { get; set; }
        public bool validate_session { get; set; }
        public bool validate_expiration { get; set; }
        public bool validate { get; set; }
        public string resul { get; set; }

        public CheckJWT()
        {
        }

        public CheckJWT(string subject, string issuer, string issuedAt, string expiration, string usuario, string id_sesion_recibida, string id_sesion_actual, bool validate_session, bool validate_expiration, bool validate, string resul)
        {
            Subject = subject;
            Issuer = issuer;
            IssuedAt = issuedAt;
            Expiration = expiration;
            this.usuario = usuario;
            this.id_sesion_recibida = id_sesion_recibida;
            this.id_sesion_actual = id_sesion_actual;
            this.validate_session = validate_session;
            this.validate_expiration = validate_expiration;
            this.validate = validate;
            this.resul = resul;
        }

        public override string ToString()
        {
            return $"{{{nameof(Subject)}={Subject}, {nameof(Issuer)}={Issuer}, {nameof(IssuedAt)}={IssuedAt}, {nameof(Expiration)}={Expiration}, {nameof(usuario)}={usuario}, {nameof(id_sesion_recibida)}={id_sesion_recibida}, {nameof(id_sesion_actual)}={id_sesion_actual}, {nameof(validate_session)}={validate_session.ToString()}, {nameof(validate_expiration)}={validate_expiration.ToString()}, {nameof(validate)}={validate.ToString()}, {nameof(resul)}={resul}}}";
        }
    }
}
