﻿using System.ComponentModel;

namespace Domain.Enums
{
    public enum Status
    {
        [Description("Aguardando atualização de status")]
        NotFinished = 0,
        [Description("Pagamento Pendente")]
        Authorized = 1,
        [Description("Pagamento confirmado")]
        PaymentConfirmed = 2,
        [Description("Transação negada")]
        Denied = 3,
        [Description("Pagamento cancelado")]
        Voided = 10,
        [Description("Pagamento Cancelado/Estornado")]
        Refunded = 11,
        [Description("Esperando retorno da instituição financeira")]
        Pending = 12,
        [Description("Pagamento cancelado por falha no processamento")]
        Aborted = 13,
        [Description("Recorrência agendada")]
        Scheduled = 20
    }
}
