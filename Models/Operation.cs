namespace Calculadora.Models
{
    /// <summary>
    /// Modelo para uma operação a ser incluída no banco de dados
    /// </summary>
    internal class Operation
    {
        public string FullOperation { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;
    }
}
