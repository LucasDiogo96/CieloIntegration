
using System.ComponentModel;

namespace Domain.Enums
{
    public enum PaymentType
    {
        [Description("Cartão de Crédito")]
        CreditCard,
        [Description("Cartão de Débito")]
        DebitCard,
        [Description("Boleto")]
        Boleto,
        [Description("QR Code")]
        Qrcode,
        [Description("Transferência Eletrônica")]
        EletronicTransfer
    }
}
