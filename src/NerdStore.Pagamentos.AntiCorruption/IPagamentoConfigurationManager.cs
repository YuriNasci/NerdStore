namespace NerdStore.Pagamentos.AntiCorruption
{
    public interface IPagamentoConfigurationManager
    {
        string GetValue(string node);
    }
}