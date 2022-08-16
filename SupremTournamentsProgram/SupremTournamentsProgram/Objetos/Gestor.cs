using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Objetos
{
    class Gestor : ObservableObject
    {

        public int idGestor { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string contrasenya { get; set; }

        public Gestor()
        {
        }

        public Gestor(int idGestor, string nombre, string email, string contrasenya)
        {
            this.idGestor = idGestor;
            this.nombre = nombre;
            this.email = email;
            this.contrasenya = contrasenya;
        }

        static public string CifrarSha256(string texto)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(texto));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            string hashtext = hash.ToString();

            while (hashtext.Length < 32)
            {
                hashtext = "0" + hashtext;
            }

            return hashtext;
        }
               

        public string ToJsontFormat()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return $"{{{nameof(idGestor)}={idGestor.ToString()}, {nameof(nombre)}={nombre}, {nameof(email)}={email}, {nameof(contrasenya)}={contrasenya}}}";
        }
    }
}
