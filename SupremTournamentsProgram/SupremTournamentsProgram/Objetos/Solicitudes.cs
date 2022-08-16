using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Objetos
{
    class Solicitudes : ObservableObject
    {
        public int idSolicitudes { get; set; }
        public string nombreSolicitudes { get; set; }
        public string fotoUrlSolicitudes { get; set; }
        public long fechaNacimiento { get; set; }
        public string datos { get; set; }
        public string datosPublicos { get; set; }
        public string estado { get; set; }
        public TorneoIndividual idTorneoIndividual { get; set; }

        public Solicitudes()
        {
        }

        public Solicitudes(int idSolicitudes, string nombreSolicitudes, string fotoUrlSolicitudes, long fechaNacimiento, string datos, string datosPublicos, string estado, TorneoIndividual idTorneoIndividual)
        {
            this.idSolicitudes = idSolicitudes;
            this.nombreSolicitudes = nombreSolicitudes;
            this.fotoUrlSolicitudes = fotoUrlSolicitudes;
            this.fechaNacimiento = fechaNacimiento;
            this.datos = datos;
            this.datosPublicos = datosPublicos;
            this.estado = estado;
            this.idTorneoIndividual = idTorneoIndividual;
        }

        public Solicitudes(Solicitudes solicitudes)
        {
            this.idSolicitudes = solicitudes.idSolicitudes;
            this.nombreSolicitudes = solicitudes.nombreSolicitudes;
            this.fotoUrlSolicitudes = solicitudes.fotoUrlSolicitudes;
            this.fechaNacimiento = solicitudes.fechaNacimiento;
            this.datos = solicitudes.datos;
            this.datosPublicos = solicitudes.datosPublicos;
            this.estado = solicitudes.estado;
            this.idTorneoIndividual = solicitudes.idTorneoIndividual;
        }

        public Solicitudes(SolicitudesContinuas solicitudesContinuas, int idSolicitudes, string estado, TorneoIndividual torneoIndividual)
        {
            this.idSolicitudes = idSolicitudes;
            this.nombreSolicitudes = solicitudesContinuas.nombreSolicitudesContinuas;
            this.fotoUrlSolicitudes = solicitudesContinuas.fotoUrlSolicitudesContinuas;
            this.fechaNacimiento = solicitudesContinuas.fechaNacimiento;
            this.datos = solicitudesContinuas.datos;
            this.datosPublicos = solicitudesContinuas.datosPublicos;
            this.estado = estado;
            this.idTorneoIndividual = torneoIndividual;

        }

        public override string ToString()
        {
            return $"{{{nameof(idSolicitudes)}={idSolicitudes.ToString()}, {nameof(nombreSolicitudes)}={nombreSolicitudes}, {nameof(fotoUrlSolicitudes)}={fotoUrlSolicitudes}, {nameof(fechaNacimiento)}={fechaNacimiento.ToString()}, {nameof(datos)}={datos}, {nameof(datosPublicos)}={datosPublicos}, {nameof(estado)}={estado}, {nameof(idTorneoIndividual)}={idTorneoIndividual}}}";
        }

        public string ToJsontFormat()
        {
            return "{" +
                      $"\u0022idSolicitudes\u0022: {idSolicitudes}," +
                      $"\u0022nombreSolicitudes\u0022: \u0022{nombreSolicitudes}\u0022," +
                      $"\u0022fotoUrlSolicitudes\u0022: \u0022{fotoUrlSolicitudes}\u0022," +
                      $"\u0022fechaNacimiento\u0022: {fechaNacimiento}," +
                      $"\u0022datos\u0022:\u0022{datos}\u0022," +
                      $"\u0022datosPublicos\u0022: \u0022{datosPublicos}\u0022," +
                      $"\u0022estado\u0022: \u0022{estado}\u0022," +
                      $"\u0022idTorneoIndividual\u0022: {idTorneoIndividual.idTorneoIndividual}" +
                      "}";
        }

        public enum Estado
        {
            Acceptada,
            Rechazada,
            Solicitada
        }

    }
}
