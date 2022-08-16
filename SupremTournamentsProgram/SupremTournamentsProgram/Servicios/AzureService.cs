using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremTournamentsProgram.Servicios
{
    /// <summary>
    /// El servicio ServicioAzure nos permite guardar una imagen en un contenedor de Azure
    /// Creado por Sr.J
    /// </summary>
    static class AzureService
    {
        /// <summary>
        /// El metodo GuardarImagen nos permite guardar una imagen en azure
        /// </summary>
        /// <param name="rutaImagen">Le deberemos de pasar como parametro la ruta local de la foto que queremos guardar en Azure</param>
        /// <returns>Nos devuelve un string el cual nos indica la ruta de la imagen en el contenedor de Azure</returns>
        static public string GuardarImagen(string rutaImagen)
        {

            //Obtenemos el cliente del contenedor
            var clienteBlobService = new BlobServiceClient(Properties.Settings.Default.cadenaConexion);
            var clienteContenedor = clienteBlobService.GetBlobContainerClient(Properties.Settings.Default.nombreContenedorBlobs);

            //Leemos la imagen
            Stream streamImagen = File.OpenRead(rutaImagen);
            string nombreImagen = Path.GetFileName(rutaImagen) + DateTime.Now.ToString("hh:mm:ss") + ".png";

            //Subimos
            clienteContenedor.UploadBlob(nombreImagen, streamImagen);

            //Una vez subida, obtenemos la URL para referenciarla
            var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
            return clienteBlobImagen.Uri.AbsoluteUri;
        }



    }
}
