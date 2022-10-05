using System;

namespace Calculadora.Models
{
    /// <summary>
    /// Modelo para uma operação a ser incluída no banco de dados
    /// </summary>
    public class Operation
    {
        public Guid Id { get; set; }
        public string FullOperation { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public Operation(Guid id, string fullOperation, string time)
        {
            Id = id;
            FullOperation = fullOperation;
            Time = time;
        }

        public Operation() { }
    }
}
