using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Objetos
{
    class TorneoIndividual : ObservableObject
    {
        public int idTorneoIndividual { get; set; }
        public string nombreTorneoIndividual { get; set; }
        public string fotoUrlTorneoIndividual { get; set; }
        public string descripcionCorta { get; set; }
        public string descripcionCompleta { get; set; }
        public int nivel { get; set; }
        public int solicitudesMaximos { get; set; }
        public long fechaFinalizacion { get; set; }
        public long fechaInicio { get; set; }
        public bool menoresEdad { get; set; }
        public Gestor idGestor { get; set; }

        public TorneoIndividual()
        {
        }

        public TorneoIndividual(int idTorneoIndividual, string nombreTorneoIndividual, string fotoUrlTorneoIndividual, string descripcionCorta, string descripcionCompleta, int nivel, int solicitudesMaximos, long fechaFinalizacion, long fechaInicio, bool menoresEdad, Gestor idGestor)
        {
            this.idTorneoIndividual = idTorneoIndividual;
            this.nombreTorneoIndividual = nombreTorneoIndividual;
            this.fotoUrlTorneoIndividual = fotoUrlTorneoIndividual;
            this.descripcionCorta = descripcionCorta;
            this.descripcionCompleta = descripcionCompleta;
            this.nivel = nivel;
            this.solicitudesMaximos = solicitudesMaximos;
            this.fechaFinalizacion = fechaFinalizacion;
            this.fechaInicio = fechaInicio;
            this.menoresEdad = menoresEdad;
            this.idGestor = idGestor;
        }

        public override string ToString()
        {
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(fechaFinalizacion).Date;

            return "id: " + idTorneoIndividual + " nombre: " + nombreTorneoIndividual + " descripcion corta: " + descripcionCorta + " Fecha finalizacion: " + dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + "\nGestor: " + idGestor;
        }



        public string ToJsontFormat()
        {
            return "{" +
                      $"\u0022idTorneoIndividual\u0022: {idTorneoIndividual}," +
                      $"\u0022nombreTorneoIndividual\u0022: \u0022{nombreTorneoIndividual}\u0022," +
                      $"\u0022fotoUrlTorneoIndividual\u0022: \u0022{fotoUrlTorneoIndividual}\u0022," +
                      $"\u0022descripcionCorta\u0022: \u0022{descripcionCorta}\u0022," +
                      $"\u0022descripcionCompleta\u0022: \u0022{descripcionCompleta}\u0022," +
                      $"\u0022nivel\u0022: {nivel}," +
                      $"\u0022solicitudesMaximos\u0022: {solicitudesMaximos}," +
                      $"\u0022fechaFinalizacion\u0022: {fechaFinalizacion}," +
                      $"\u0022fechaInicio\u0022: {fechaInicio}," +
                      $"\u0022menoresEdad\u0022: {menoresEdad.ToString().ToLower()}," +
                      $"\u0022idGestor\u0022: {idGestor.idGestor} " +
                   "}";
        }


    }
}
