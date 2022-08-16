using Microsoft.Toolkit.Mvvm.ComponentModel;
using RestSharp;

namespace SupremTournamentsProgram.Objetos
{
    public class ElementoSeguridad:ObservableObject
    {

        public string Apikey { get; set; }

        public RestResponseCookie Cookie { get; set; }

        public string Session { get; set; }

        public ElementoSeguridad()
        {
        }

        public ElementoSeguridad(string apikey, RestResponseCookie cookie, string session)
        {
            Apikey = apikey;
            Cookie = cookie;
            Session = session;
        }

        public override string ToString()
        {
            return $"{{{nameof(Apikey)}={Apikey}, {nameof(Cookie)}={Cookie}, {nameof(Session)}={Session}}}";
        }
    }
}
