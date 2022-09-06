using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiInegi
{
    public class InegiApi
    {
        public InegiApi() { }

        public void ConsultaDatos()
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://www.inegi.org.mx//app/api/denue/v1/consulta/buscar/restaurantes/21.85717833,-102.28487238/200/74b573ec-deab-43fc-a19e-eadd5feb3133");
            ////Establecemos el TimeOut para obtener la respuesta del servidor
            //client.Timeout = TimeSpan.FromSeconds(60);

            //Se compone el mensaje a enviar
            // XX, YY y ZZ se corresponden con los valores de identificación del usuario en el sistema.
            //var postData = new List<KeyValuePair<string, string>>();
            //postData.Add(new KeyValuePair<string, string>("apiKey", "22"));                                         //Identificador del cliente
            //postData.Add(new KeyValuePair<string, string>("carrier", DomainID));                                    //Operador al que pertenece el destinatario. Opcional
            //postData.Add(new KeyValuePair<string, string>("country", Login));                                       //Identificador del PAIS ISO2
            //postData.Add(new KeyValuePair<string, string>("dial", Pass));                                           //Dial numero de marcacion que se utilizara para enviar el mensaje
            //postData.Add(new KeyValuePair<string, string>("message", "Lo logre con Concepto Movil"));               //Mensaje a enviar
            //postData.Add(new KeyValuePair<string, string>("msisdns", "[522211502238]"));                              //Numero de 10 Digitos más LADA 
            //postData.Add(new KeyValuePair<string, string>("tag", "Tag prueba"));                                   //Texto que sirve para identificar la peticion
                                                                                                                   //postData.Add(new KeyValuePair<string, string>("mask", "MASCARA"));                                   //Enmascaramiento. Opcional
                                                                                                                   //postData.Add(new KeyValuePair<string, string>("schedule", "2019-09-03T11:49:00-06:00"));                //Fecha y hora en que se enviara el mensaje con zona horario. Si no se incluye se envia inmediatamente. Opcional
                                                                                                                   //No es posible utilizar el remitente en América pero sí en España y Europa
                                                                                                                   //Descomentar la línea solo si se cuenta con un remitente autorizado por Altiria
                                                                                                                   //postData.Add(new KeyValuePair<string, string>("senderId", "remitente"));


            //HttpContent content = new FormUrlEncodedContent(postData);
            String err = "";
            String resp = "";
            try
            {
                //Se fija la URL sobre la que enviar la petición POST
                string url = "https://www.inegi.org.mx/app/api/denue/v1/consulta/buscar/restaurantes/21.85717833,-102.28487238/200/74b573ec-deab-43fc-a19e-eadd5feb3133";
                HttpWebRequest request = HttpWebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentLength = 0;
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                request.Referer = "https://www.inegi.org.mx/";
                //request.Content = content;
                //content.Headers.ContentType.CharSet = "UTF-8";
                //request.Content.Headers.ContentType =
                //  new MediaTypeHeaderValue("application/json");
                //HttpResponseMessage response = client.SendAsync(request).Result;
                //var responseString = response.Content.ReadAsStringAsync();
                //resp = responseString.Result;

                // Optionally, set properties of the HttpWebRequest, such as:
                //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                //request.ContentType = "application/x-www-form-urlencoded";
                // Could also set other HTTP headers such as Request.UserAgent, Request.Referer,
                // Request.Accept, or other headers via the Request.Headers collection.

                // Set the POST request body data. In this example, the POST data is in 
                // application/x-www-form-urlencoded format.
                //string postData = "myparam1=myvalue1&myparam2=myvalue2";
                //using (var writer = new StreamWriter(request.GetRequestStream()))
                //{
                //    writer.Write(postData);
                //}

                // Submit the request, and get the response body from the remote server.
                string responseFromRemoteServer;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseFromRemoteServer = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            finally
            {
                if (err != "")
                    System.Windows.Forms.MessageBox.Show(err);
                else
                    System.Windows.Forms.MessageBox.Show(resp);
            }
        }

        public void ConsultaDatosGet()
        {
            string requestUrl = "https://www.inegi.org.mx/app/api/denue/v1/consulta/buscar/restaurantes/21.85717833,-102.28487238/200/74b573ec-deab-43fc-a19e-eadd5feb3133";
            HttpWebRequest request = HttpWebRequest.CreateHttp(requestUrl);

            // Optionally, set properties of the HttpWebRequest, such as:
            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 2 * 60 * 1000; // 2 minutes, in milliseconds

            // Submit the request, and get the response body.
            string responseBodyFromRemoteServer;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseBodyFromRemoteServer = reader.ReadToEnd();
                }
            }
        }

        public void ConsultaMicro()
        {
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create("https://www.inegi.org.mx/app/api/denue/v1/consulta/buscar/");
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = "restaurantes/21.85717833,-102.28487238/200/74b573ec-deab-43fc-a19e-eadd5feb3133";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            // Close the response.
            response.Close();
        }
    }
}
