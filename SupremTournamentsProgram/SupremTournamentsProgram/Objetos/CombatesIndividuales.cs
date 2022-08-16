using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupremTournamentsProgram.Objetos
{
    class CombatesIndividuales : ObservableObject
    {
        public int idCombatesIndividuales { get; set; }
        public Solicitudes idSolicitud1 { get; set; }
        public Solicitudes idSolicitud2 { get; set; }
        public Solicitudes idSolicitudGanador { get; set; }
        public TorneoIndividual idTorneoIndividual { get; set; }

        public CombatesIndividuales()
        {
        }

        public CombatesIndividuales(int idCombatesIndividuales, Solicitudes idSolicitud1, Solicitudes idSolicitud2, Solicitudes idSolicitudGanador, TorneoIndividual idTorneoIndividual)
        {
            this.idCombatesIndividuales = idCombatesIndividuales;
            this.idSolicitud1 = idSolicitud1;
            this.idSolicitud2 = idSolicitud2;
            this.idSolicitudGanador = idSolicitudGanador;
            this.idTorneoIndividual = idTorneoIndividual;
        }

        
        public string ToJsontFormat()
        {
            string jsonFormat = "{" +
                      $"\u0022idCombatesIndividuales\u0022: {idCombatesIndividuales}," +
                      $"\u0022idSolicitud1\u0022: {idSolicitud1.idSolicitudes}," +
                      $"\u0022idSolicitud2\u0022: {idSolicitud2.idSolicitudes},";

            if (idSolicitudGanador != null)
            {
                jsonFormat += $"\u0022idSolicitudGanador\u0022: {idSolicitudGanador.idSolicitudes},";
            }
            else
            {
                jsonFormat += $"\u0022idSolicitudGanador\u0022: null,";

            }

            jsonFormat += $"\u0022idTorneoIndividual\u0022: {idTorneoIndividual.idTorneoIndividual}" +
                   "}";

            return jsonFormat;
        }

        public override string ToString()
        {
            return $"{{{nameof(idCombatesIndividuales)}={idCombatesIndividuales.ToString()}, {nameof(idSolicitud1)}={idSolicitud1}, {nameof(idSolicitud2)}={idSolicitud2}, {nameof(idSolicitudGanador)}={idSolicitudGanador}, {nameof(idTorneoIndividual)}={idTorneoIndividual}}}";
        }
    }
}
