using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SupremTournamentsProgram.Servicios
{
    /// <summary>
    /// El servicio DiaologosService nos permite usar los dialogos mas necesitados en nuestro programa
    /// </summary>
    static class DialogosService
    {
        /// <summary>
        /// El metodo AbrirArchivoDialogo nos ayudara a saber que archivo elige el usuario
        /// </summary>
        /// <param name="filtro">Este parametro lo utilizamos para indicarle que tipos de archivos puede elegir el usuario</param>
        /// <returns>Nos retorna la ruta del archivo elegido por el usuario</returns>
        static public string AbrirArchivoDialogo(string filtro)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filtro
            };
            openFileDialog.ShowDialog();

            return openFileDialog.FileName;
        }

        /// <summary>
        /// El metodo MensajeError nos ayudara a indicarle al usuario de una forma simple un error
        /// </summary>
        /// <param name="tituloMessageBox">Este parametro es donde le indicamos que titulo tendra la ventana emergente</param>
        /// <param name="mensajeError">Este parametro es donde le indicaremos que error es el que sucede</param>
        static public void MensajeError(string tituloMessageBox, string mensajeError)
        {
            MessageBox.Show(mensajeError, tituloMessageBox, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// El metodo MensajeInformacion nos ayudara a indicarle al usuario de una forma simple informacion
        /// </summary>
        /// <param name="tituloMessageBox">Este parametro es donde le indicamos que titulo tendra la ventana emergente</param>
        /// <param name="mensajeInformacion">Este parametro es donde le indicaremos que informacion queremos mostrar</param>
        static public void MensajeInformacion(string tituloMessageBox, string mensajeInformacion)
        {
            MessageBox.Show(mensajeInformacion, tituloMessageBox, MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// El metodo MensajeInformacionDosOpciones nos ayudara a indicarle al usuario de una forma simple informacion y que el usuario elija entre acceptar o rechazar devolviendo un booleando que indicara si a pulsado Yes o no
        /// </summary>
        /// <param name="tituloMessageBox">Este parametro es donde le indicamos que titulo tendra la ventana emergente</param>
        /// <param name="mensajeInformacion">Este parametro es donde le indicaremos cual informacion queremos mostrarle</param>
        /// <returns>Nos retorna un booleando indicando si a pulsado si o no</returns>
        static public bool MensajeInformacionDosOpciones(string tituloMessageBox, string mensajeInformacion)
        {
            return MessageBox.Show(mensajeInformacion, tituloMessageBox, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }


    }
}
