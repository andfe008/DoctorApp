namespace API.Errores
{
    public class ApiValidacionErrorResponse : ApiErrorMensaje
    {
        public ApiValidacionErrorResponse() : base(400)
        {

        }
        public IEnumerable<string> Errores { get; set; }
    }
}
