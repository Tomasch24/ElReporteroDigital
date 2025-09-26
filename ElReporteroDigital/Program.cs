using System;
using static ElReporteroDigital.Reportero;

namespace ElReporteroDigital
{
    //Clase Reportero en donde se encuentran los metodos
    //Para descargar el articulo, las imagenes y analizar la palabra clave
    public class Reportero 
    {
        //Excepcion personalizada para capturar el error en el try-catch
        public class ServidorNoDisponibleException : Exception
        {
            public ServidorNoDisponibleException(string msj) : base(msj) { }

        }

        //Evento de tipo accion en cargado de hacer la barra de carga
        public event Action<int> ProgresoDescarga;
        
        //Evento de tipo accion encargado de validar que el Articulo del reportero esta lsito
        public event Action ReporteroListo;

        // Metodo asincrono para simular la descarga del texto del articulo
        public async Task<string> DownloadArticleAsync()
        {
            Console.WriteLine("Descaragando articulo....");


            await Task.Delay(2000);

            //Variable random encargada de simular un fallo aleatorio
            Random rnd = new Random();
            if (rnd.Next(0, 5) == 0) // 20% de probabilidad de fallo
                throw new ServidorNoDisponibleException("Error Fatal. Es que ni para ejecutar un simple codigo sirves. Arranca para Psicologia defecto de la sociedad, bachillerato barato 😂😂😂");

            //Retorno del articulo
            return "\n Luego de un bajon en la luz y 2 gotas de agua, se empiezan a volver a dañar los Aires de la Universidad Centrel del Este (UCE) extencion Punta Cana (Y eso que duraron como un mes reparandolos)." ;
        }
        // Metodo asincrono para simular la descarga de las imagenes del articulo
        public async Task<List<string>> DownloadImageUrlsAsync()
        {
            Console.WriteLine("Descaragando Imagenes....");

            await Task.Delay(3000);

            //Lista en donde se almacenan las URLs de las imagenes

            List<string> images = new List<string> {"foto1.jpg", "foto2.jpg" };

           

            return images;
        }

        // Metodo asincrono para simular el trabajo del CPU articulo

        public async Task<string> AnalyzeKeywordsAsync()
        {
            Console.WriteLine("Tabajando en ello....");

           //Bucle for para simular el progrso de carga

            for (int i = 0; i <= 100; i += 25)
            {
                await Task.Delay(300); 
                ProgresoDescarga?.Invoke(i);
            }

            //ReporteroListo?.Invoke("El articulo acerca de la 'Universidad' esta listo para publicar");
            return "\nNoticias de Ultima Hora.";
        }

        //Metodo asincrono encargado de mostrar el mensaje del evento reportero listo
        public async Task ArticuloListo()
        {
            ReporteroListo?.Invoke();
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            //Instancia de la clase reportero
            Reportero r = new Reportero();

            //Captura de error try-catch
            try
            {
                //Suscripcion al evento ReporteroListo
                r.ReporteroListo += () =>
                {
                    Console.WriteLine("\nEl articulo acerca de la 'Universidad' esta listo para publicar");
                };
                Console.WriteLine();
                //Suscripcion al Event Action ProgresoDescarga
                r.ProgresoDescarga += (progreso) =>
                {
                    Console.WriteLine($"\rCargando articulo..... {progreso}%");
                };
                //Variables de tipo var para almacenar el articulo y la lista de la imagenes
                var articulotxt = r.DownloadArticleAsync();

                var imagenes = r.DownloadImageUrlsAsync();

                // El Await Task.WhenAll Para simular ambas descargas al mismo tiempo
                await Task.WhenAll(articulotxt, imagenes);

                //Aqui coloque 2 variables mas para igualarlas a las de arriba .Result, esto es para tomaros resultados almacenados en las variables 
                string art = articulotxt.Result;

                List<string> imgs = imagenes.Result;

                //Variable de tipo string encargada de almacenar la palabra clave
                string keyword = await r.AnalyzeKeywordsAsync();

                //Aqui es donde ensamblo el articulo
                Console.WriteLine($"{art}");

                Console.WriteLine($"{keyword}");

                imgs.ForEach(img => Console.WriteLine($"- {img}"));

                //Llamada del metodo con el evento reportero listo
                r.ArticuloListo();
                
            }
            catch (ServidorNoDisponibleException ex)
            {
                //Aqui se captura el error y la excepcion muestra el error "AMIGABLE"
                Console.WriteLine("No se pudo generar el articulo. " + ex.Message); 
            }
        }
    }
}