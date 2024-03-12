using Microsoft.AspNetCore.Http;

namespace API.Errores
{
    public class ApiErrorMensaje
    {
       

        public ApiErrorMensaje(int statusCode, string mensaje= null) 
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeStatusCode(statusCode) ;
        }
 

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }


        public string GetMensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"Se ha realizado una solicitud no valida",
                401=>"No estas Autorizado para este recurso",
                404=>"Recurso no encontrado",
                500=>"Error interno del servidor",
                _ => null
            };



        }

    }
}
