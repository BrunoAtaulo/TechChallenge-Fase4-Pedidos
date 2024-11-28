﻿using System.ComponentModel;

namespace App.Application.ViewModels.Enuns
{
    public enum EnumPedidoPagamento
    {
        [Description("1 - Pendente")]
        Pendente = 1,

        [Description("2 - Pago")]
        Pago = 2,

        [Description("3 - Cancelado")]
        Cancelado = 3
    }
}
