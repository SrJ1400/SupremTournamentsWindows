using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Objetos
{
    class SolicitudesContinuas : ObservableObject
    {
        public int idSolicitudesContinuas { get; set; }
        public string nombreSolicitudesContinuas { get; set; }
        public string fotoUrlSolicitudesContinuas { get; set; }
        public long fechaNacimiento { get; set; }
        public string datos { get; set; }
        public string datosPublicos { get; set; }

        public SolicitudesContinuas()
        {
        }

        public SolicitudesContinuas(int idSolicitudesContinuas, string nombreSolicitudesContinuas, string fotoUrlSolicitudesContinuas, long fechaNacimiento, string datos, string datosPublicos)
        {
            this.idSolicitudesContinuas = idSolicitudesContinuas;
            this.nombreSolicitudesContinuas = nombreSolicitudesContinuas;
            this.fotoUrlSolicitudesContinuas = fotoUrlSolicitudesContinuas;
            this.fechaNacimiento = fechaNacimiento;
            this.datos = datos;
            this.datosPublicos = datosPublicos;
        }

        public SolicitudesContinuas(SolicitudesContinuas solicitudesContinuas)
        {
            this.idSolicitudesContinuas = solicitudesContinuas.idSolicitudesContinuas;
            this.nombreSolicitudesContinuas = solicitudesContinuas.nombreSolicitudesContinuas;
            this.fotoUrlSolicitudesContinuas = solicitudesContinuas.fotoUrlSolicitudesContinuas;
            this.fechaNacimiento = solicitudesContinuas.fechaNacimiento;
            this.datos = solicitudesContinuas.datos;
            this.datosPublicos = solicitudesContinuas.datosPublicos;
        }

        public SolicitudesContinuas(Solicitudes solicitudes, int id)
        {
            this.idSolicitudesContinuas = id;
            this.nombreSolicitudesContinuas = solicitudes.nombreSolicitudes;
            this.fotoUrlSolicitudesContinuas = solicitudes.fotoUrlSolicitudes;
            this.fechaNacimiento = solicitudes.fechaNacimiento;
            this.datos = solicitudes.datos;
            this.datosPublicos = solicitudes.datosPublicos;
        }


        public string ToJsontFormat()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return $"{{{nameof(idSolicitudesContinuas)}={idSolicitudesContinuas.ToString()}, {nameof(nombreSolicitudesContinuas)}={nombreSolicitudesContinuas}, {nameof(fotoUrlSolicitudesContinuas)}={fotoUrlSolicitudesContinuas}, {nameof(fechaNacimiento)}={fechaNacimiento.ToString()}, {nameof(datos)}={datos}, {nameof(datosPublicos)}={datosPublicos}}}";
        }
    }
}
