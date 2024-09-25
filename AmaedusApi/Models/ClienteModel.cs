namespace api.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Pais { get; set; }
        public decimal Saldo { get; set; }
        public bool EsActivo { get; set; }
    }
}