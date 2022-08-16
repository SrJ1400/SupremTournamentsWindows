using Newtonsoft.Json;
using RestSharp;
using SupremTournamentsProgram.Objetos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Servicios
{
    /// <summary>
    /// El servicio ApiService nos permite comunicarnos con la Api de Suprem Tornaments.
    /// Creado por Sr.J.
    /// </summary>
    static class ApiService
    {

        static public ElementoSeguridad LastElementoSeguridad { get; set; }

        /////////////////////JWT////////////////////////

        /// <summary>
        /// El metodo GetElementoSeguridad crea una sesión y solicita la ApiKey.
        /// </summary>
        /// <param name="email">Email del gestor que se va a loguear</param>
        /// <param name="contrasenya">Contraseña del gestor que se va a loguear</param>
        /// <returns>Nos debuelve una variable tipo ElementoSeguridad completa</returns>
        static public ElementoSeguridad GetElementoSeguridad(string email, string contrasenya)
        {
            ElementoSeguridad elementoSeguridad = CreateSesion();

            elementoSeguridad.Apikey = Loguin(email, contrasenya, elementoSeguridad.Cookie);

            LastElementoSeguridad = elementoSeguridad;

            return elementoSeguridad;
        }

        /// <summary>
        /// El metodo CreateSesion crea una sesión.
        /// </summary>
        /// <returns>Nos debuelve una variable tipo ElementoSeguridad con la sesion y la Cookie.</returns>
        static public ElementoSeguridad CreateSesion()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/jwt/create-session", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = cliente.Execute(request);


            Dictionary<string, string> jsonReturn = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ToString());

            ElementoSeguridad elementoSeguridad = new ElementoSeguridad();
            elementoSeguridad.Cookie = response.Cookies[0];
            elementoSeguridad.Session = jsonReturn["session"].ToString();


            return elementoSeguridad;
        }

        /// <summary>
        /// El metodo Loguin nos permite Loguearnos en la API.
        /// </summary>
        /// <param name="email">Email del gestor que se va a loguear</param>
        /// <param name="contrasenya">Contraseña del gestor que se va a loguear</param>
        /// <param name="cookie">Cookie la cual indica que somos nosotros</param>
        /// <returns>Nos debuelve un string el cual es la apikey JWT.</returns>
        static public string Loguin(string email, string contrasenya, RestResponseCookie cookie)
        {
            RestClient cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/jwt/auth", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddCookie(cookie.Name, cookie.Value);

            request.AddJsonBody("{" +
                                  $"\u0022email\u0022: \u0022{email}\u0022," +
                                  $"\u0022password\u0022: \u0022{contrasenya}\u0022" +
                                "}");

            IRestResponse response = cliente.Execute(request);


            Dictionary<string, string> jsonReturn = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ToString());

            if (jsonReturn != null)
            {
                return jsonReturn["JWT"].ToString();
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// El metodo CheckApiKey nos servira para verificar si es valida la apikey de un objeto tipo ElementoSeguridad.
        /// </summary>
        /// <param name="elementoSeguridad">ElementoSeguridad variable con cookie y JWT necesaria para correcto funcionamiento</param>
        /// <returns>Nos debuelve un CheckJWT con el que podremops comprobar el estado de la ApiKey.</returns>
        static public CheckJWT CheckApiKey(ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/jwt/check", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            CheckJWT check = JsonConvert.DeserializeObject<CheckJWT>(cliente.Execute(request).Content);

            return check;
        }

        ////////////////Parte de Gestores /////////////////////


        /// <summary>
        /// El metodo GetGestorByEmail nos sirve para obtener una gestor apartir del email.
        /// </summary>
        /// <param name="email">Email del gestor que se solicita</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo Gestor.</returns>
        static public Gestor GetGestorByEmail(string email, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/gestor/{email}", Method.GET);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            return JsonConvert.DeserializeObject<Gestor>(cliente.Execute(request).Content);
        }


        /// <summary>
        /// El metodo CreateGestor nos sirve para crear un gestor.
        /// </summary>
        /// <param name="gestor">gestor es el Gestor a crear</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode CreateGestor(Gestor gestor)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/gestor", Method.POST);
            request.AddJsonBody(gestor.ToJsontFormat());

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }

        ////////////////Parte de Torneos/////////////////////

        /// <summary>
        /// El metodo GetTorneosDisponibles nos obtiene los torneos con una fecha de finalizacion superior a la actual de la API y con plazas libres.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosDisponibles()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/fechaposterioractualyplazaslibres", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetTorneosDisponibles nos obtiene todos los torneos.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetALLTorneos()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var requestGET = new RestRequest("/torneoindividual", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(requestGET).Content);

        }

        /// <summary>
        /// El metodo GetTorneosPlazasLibres nos obtiene los torneos con plazas libres.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosPlazasLibres()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/plazaslibres",Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosNivelDesc nos obtiene los torneos con nivel descendente.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosNivelDesc()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/nivel/desc",Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosNivelDesc nos obtiene los torneos con nivel ascendente.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosNivelAsc()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/nivel/asc", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }


        /// <summary>
        /// El metodo GetTorneosNivel nos obtiene torneos del nivel indicado.
        /// </summary>
        /// <param name="nivel">Nivel de los torneos solicitados</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosNivel(int nivel)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/torneoindividual/nivel/{nivel}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosNombreGestor nos obtiene torneos del nombre de gestor solicitado.
        /// </summary>
        /// <param name="nombre">Nombre del gestor de los torneos solicitados</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosNombreGestor(string nombre)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/torneoindividual/gestor/{nombre}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosNombreGestor nos obtiene torneos del email de gestor solicitado.
        /// </summary>
        /// <param name="email">Email del gestor de los torneos solicitados</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosEmailGestor(string email)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/torneoindividual/gestoremail/{email}", Method.GET);

            try { 
                return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);
            }
            catch(Exception ex)
            {
                return new ObservableCollection<TorneoIndividual>();
            }

        }

        /// <summary>
        /// El metodo GetTorneosFechaFinalizacionDesc nos obtiene los torneos con Fecha de finalizacioc descendente.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosFechaFinalizacionDesc()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/fechafinalizacion/desc", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosFechaFinalizacionAsc nos obtiene los torneos con Fecha de finalizacioc ascendente.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosFechaFinalizacionAsc()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/fechafinalizacion/asc", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosMayorEdadTrue nos obtiene los torneos con en los que solo es necesari ser mayor de edad.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosMenorEdadTrue()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/edad/true", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosMayorEdadTrue nos obtiene los torneos con en los que pueden participar menores de edad.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<TorneoIndividual>.</returns>
        static public ObservableCollection<TorneoIndividual> GetTorneosMenorEdadFalse()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual/edad/false", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<TorneoIndividual>>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo GetTorneosId nos obtiene el torneo con un id en concreto.
        /// </summary>
        /// <param name="id">Id del torneo solicitado</param>
        /// <returns>Devuelve un objeto tipo TorneoIndividual.</returns>
        static public TorneoIndividual GetTorneosId(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/torneoindividual/{id}", Method.GET);

            return JsonConvert.DeserializeObject<TorneoIndividual>(cliente.Execute(request).Content);

        }

        /// <summary>
        /// El metodo CreateTorneo nos sirve para crear torneos con los datos unicamente del torneo.
        /// </summary>
        /// <param name="torneoIndividual">torneoIndividual torneo ha crear con id 0 obligatorio</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode para controlar los fallos.</returns>
        static public HttpStatusCode CreateTorneo(TorneoIndividual torneoIndividual, ElementoSeguridad elementoSeguridad)
        {
            RestClient cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/torneoindividual", Method.POST);

            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(torneoIndividual.ToJsontFormat());

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;

        }

        /// <summary>
        /// El metodo DeleteTorneosId nos sirve para eliminar un torneo con id en concreto.
        /// </summary>
        /// <param name="id">Id del torneo ha eliminar</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode para controlar los fallos.</returns>
        static public HttpStatusCode DeleteTorneosId(int id, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/torneoindividual/{id}", Method.DELETE);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }


        /// <summary>
        /// El metodo CreateTorneo nos sirve para crear torneos con los datos unicamente del torneo.
        /// </summary>
        /// <param name="torneoIndividual">torneoIndividual torneo ha editar</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo TorneoIndividual.</returns>
        static public HttpStatusCode UpdateTorneo(TorneoIndividual torneoIndividual, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/torneoindividual", Method.PUT);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);
            request.AddJsonBody(torneoIndividual.ToJsontFormat());

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }

        ////////////SolicitudesContinuas////////////////////////

        /// <summary>
        /// El metodo GetAllSolicitudesContinuas nos sirve para obtener todas las solicitudes continuas.
        /// </summary>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<SolicitudesContinuas>.</returns>
        static public ObservableCollection<SolicitudesContinuas> GetAllSolicitudesContinuas(ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/solicitudescontinuas", Method.GET);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            return JsonConvert.DeserializeObject<ObservableCollection<SolicitudesContinuas>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetOneSolicitudesContinuas nos sirve para obtener una solicitudes continuas.
        /// </summary>
        /// <param name="id">Id del solicitante continuo que se solicita</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo SolicitudesContinuas.</returns>
        static public SolicitudesContinuas GetOneSolicitudesContinuas(int id, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudescontinuas/{id}", Method.GET);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            return JsonConvert.DeserializeObject<SolicitudesContinuas>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo CreateSolicitudesContinuas nos sirve para crear una solicitud continua.
        /// </summary>
        /// <param name="solicitudContinua">solicitudContinua a crear</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode CreateSolicitudesContinuas(SolicitudesContinuas solicitudContinua, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/solicitudescontinuas", Method.POST);
            request.AddJsonBody(solicitudContinua.ToJsontFormat());
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }


        /// <summary>
        /// El metodo EditarSolicitudesContinuas nos sirve para editar una solicitud continua.
        /// </summary>
        /// <param name="solicitudContinua">solicitudContinua a crear</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode EditarSolicitudesContinuas(SolicitudesContinuas solicitudContinua, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/solicitudescontinuas", Method.PUT);
            request.AddJsonBody(solicitudContinua.ToJsontFormat());
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }

        /// <summary>
        /// El metodo DeleteSolicitudesContinuas nos sirve para eliminar una solicitud continua.
        /// </summary>
        /// <param name="id">Id de la solicitudContinua a eliminar</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode DeleteSolicitudesContinuas(int id, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudescontinuas/{id}", Method.DELETE);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }




        ////////Solicitudes////////////////////////

        /// <summary>
        /// El metodo GetAllSolicitudes nos obtiene todos los Solicitantes.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<Solicitudes>.</returns>
        static public ObservableCollection<Solicitudes> GetAllSolicitudes()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/solicitudes", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<Solicitudes>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetOneSolicitudes nos sirve para obtener una solicitud en concreto.
        /// </summary>
        /// <param name="id">Id del solicitante que se solicita</param>
        /// <returns>Devuelve un objeto tipo Solicitudes.</returns>
        static public Solicitudes GetOneSolicitudes(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudes/{id}", Method.GET);

            return JsonConvert.DeserializeObject<Solicitudes>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetAllSolicitudesFromIdTorneo nos sirve para solicitududes de un torneo en concreto.
        /// </summary>
        /// <param name="idTorneo">Id del torneo del que deseas obtener los solicitantes</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<Solicitudes>.</returns>
        static public ObservableCollection<Solicitudes> GetAllSolicitudesFromIdTorneo(int idTorneo)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudes/idTorneo/{idTorneo}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<Solicitudes>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetAllSolicitudesFromAcceptadasAndIdTorneo nos sirve para solicitududes acceptadas de un torneo en concreto.
        /// </summary>
        /// <param name="idTorneo">Id del torneo del que deseas obtener los solicitantes acceptadas</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<Solicitudes>.</returns>
        static public ObservableCollection<Solicitudes> GetAllSolicitudesFromAcceptadasAndIdTorneo(int idTorneo)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudes/acceptadas/idTorneo/{idTorneo}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<Solicitudes>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetAllSolicitudesFromSolicitadasAndIdTorneo nos sirve para solicitududes solicitas de un torneo en concreto.
        /// </summary>
        /// <param name="idTorneo">Id del torneo del que deseas obtener los solicitantes acceptadas</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<Solicitudes>.</returns>
        static public ObservableCollection<Solicitudes> GetAllSolicitudesFromSolicitadasAndIdTorneo(int idTorneo)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudes/solicitadas/idTorneo/{idTorneo}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<Solicitudes>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo CreateSolicitudes nos sirve para crear una solicitud.
        /// </summary>
        /// <param name="solicitud">solicitud es la solicitud a crear</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode CreateSolicitudes(Solicitudes solicitud)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/solicitudes", Method.POST);
            request.AddJsonBody(solicitud.ToJsontFormat());

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }


        /// <summary>
        /// El metodo EditarSolicitudes nos sirve para editar una solicitud.
        /// </summary>
        /// <param name="solicitud">solicitud es la solicitud a crear</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode EditarSolicitudes(Solicitudes solicitud, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/solicitudes", Method.PUT);
            request.AddJsonBody(solicitud.ToJsontFormat());
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }

        /// <summary>
        /// El metodo DeleteSolicitudes nos sirve para eliminar una solicitud.
        /// </summary>
        /// <param name="id">id de la solicitud a eliminar</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode DeleteSolicitudes(int id, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/solicitudes/{id}", Method.DELETE);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }




        /////////////////CombatesIndividuales///////////////

        /// <summary>
        /// El metodo GetAllCombatesIndividuales nos sirve para obtener todos CombatesIndividuales.
        /// </summary>
        /// <returns>Devuelve un objeto tipo ObservableCollection<CombatesIndividuales>.</returns>
        static public ObservableCollection<CombatesIndividuales> GetAllCombatesIndividuales()
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest("/combatesindividuales", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<CombatesIndividuales>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetOneCombateIndividual nos sirve para obtener un combate individual.
        /// </summary>
        /// <param name="id">Id del combateIndividual a obtener</param>
        /// <returns>Devuelve un objeto tipo CombatesIndividuales.</returns>
        static public CombatesIndividuales GetOneCombateIndividual(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/combatesindividuales/{id}", Method.GET);

            return JsonConvert.DeserializeObject<CombatesIndividuales>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo GetAllCombatesIndividualesFromIdTorneo nos sirve para obtener todas las combate individual continuas de un torneo en concreto.
        /// </summary>
        /// <param name="idTorneo">IdTorneo del torneoIndividual de los combates individuales a obtener</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<CombatesIndividuales>.</returns>
        static public ObservableCollection<CombatesIndividuales> GetAllCombatesIndividualesFromIdTorneo(int idTorneo)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/combatesindividuales/idTorneo/{idTorneo}", Method.GET);
            var respuesta = JsonConvert.DeserializeObject<ObservableCollection<CombatesIndividuales>>(cliente.Execute(request).Content);

            if (respuesta != null)
            {
                return respuesta;
            }
            else
            {
                return new ObservableCollection<CombatesIndividuales>();
            }

        }

        /// <summary>
        /// El metodo GetCombatesIndividualesPerdidosFromIdTorneoFromIdSolicitud nos sirve para obtener los combates perdidos de un torneo de un solicitante en concreto.
        /// </summary>
        /// <param name="idTorneo">IdTorneo del torneoIndividual al que pertenecen</param>
        /// <param name="idSolicitante">IdSolicitante del solicitante al que pertenecen</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<CombatesIndividuales>.</returns>
        static public ObservableCollection<CombatesIndividuales> GetAllCombatesIndividualesFromIdTorneo(int idTorneo, int idSolicitante)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/combatesindividuales/perdidos/{idTorneo}/{idSolicitante}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<CombatesIndividuales>>(cliente.Execute(request).Content);
        }


        /// <summary>
        /// El metodo GetCombatesIndividualesGanadosFromIdTorneoFromIdSolicitud nos sirve para obtener los combates ganados de un torneo de un solicitante en concreto.
        /// </summary>
        /// <param name="idTorneo">IdTorneo del torneoIndividual al que pertenecen</param>
        /// <param name="idSolicitante">IdSolicitante del solicitante al que pertenecen</param>
        /// <returns>Devuelve un objeto tipo ObservableCollection<CombatesIndividuales>.</returns>
        static public ObservableCollection<CombatesIndividuales> GetCombatesIndividualesGanadosFromIdTorneoFromIdSolicitud(int idTorneo, int idSolicitante)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/combatesindividuales/ganados/{idTorneo}/{idSolicitante}", Method.GET);

            return JsonConvert.DeserializeObject<ObservableCollection<CombatesIndividuales>>(cliente.Execute(request).Content);
        }

        /// <summary>
        /// El metodo CreateCombatesindividuales nos sirve para crear combate individual.
        /// </summary>
        /// <param name="comabateIndividual">comabateIndividual a crear</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode CreateCombatesindividuales(CombatesIndividuales comabateIndividual, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/combatesindividuales", Method.POST);
            request.AddJsonBody(comabateIndividual.ToJsontFormat());
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);


            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }

        /// <summary>
        /// El metodo EditarCombatesindividualess nos sirve para editar combate individual.
        /// </summary>
        /// <param name="comabateIndividual">comabateIndividual a editar</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode EditarCombatesindividualess(CombatesIndividuales combatesindividuales, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);
            var request = new RestRequest("/combatesindividuales", Method.PUT);
            request.AddJsonBody(combatesindividuales.ToJsontFormat());
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }

        /// <summary>
        /// El metodo DeleteCombatesindividuales nos sirve para eliminar combate individual.
        /// </summary>
        /// <param name="id">id del comabateIndividual a eliminar</param>
        /// <param name="elementoSeguridad">elementoSeguridad con cookie y apiKey JWT para tener permiso para crear un toneor</param>
        /// <returns>Devuelve un objeto tipo HttpStatusCode.</returns>
        static public HttpStatusCode DeleteCombatesindividuales(int id, ElementoSeguridad elementoSeguridad)
        {
            var cliente = new RestClient(Properties.Settings.Default.baseURL);

            var request = new RestRequest($"/combatesindividuales/{id}", Method.DELETE);
            request.AddCookie(elementoSeguridad.Cookie.Name, elementoSeguridad.Cookie.Value);
            request.AddQueryParameter("apikey", elementoSeguridad.Apikey);

            IRestResponse response = cliente.Execute(request);

            return response.StatusCode;
        }


    }
}
