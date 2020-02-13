namespace Solar.Domain.Enums
{
    public enum CustomerIdentityType
    {
        [Description(null)]
        Default,
        [Description("RG")]
        RG,
        [Description("CPF")]
        CPF,
        [Description("CNPJ")]
        CNPJ
    }
}
