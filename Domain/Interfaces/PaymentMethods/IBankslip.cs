namespace Domain.Interfaces
{
    public interface IBankslip
    {
        string Type { get; set; }
        string Provider { get; set; }
        string Address { get; set; }
        string BoletoNumber { get; set; }
        string Assignor { get; set; }
        string Demonstrative { get; set; }
        string ExpirationDate { get; set; }
        string Identification { get; set; }
        string Instructions { get; set; }
    }
}
