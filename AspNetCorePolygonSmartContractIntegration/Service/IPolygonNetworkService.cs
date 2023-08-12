namespace AspNetCorePolygonSmartContractIntegration.Service
{
    public interface IPolygonNetworkService
    {
        bool ConnectWeb3();
        Task ConnectSmartContractUsingAlchemy();
        Task ConnectSmartContractUsingNethereumTestChain();
        Task ConnectSmartContractUsingInfura();
    }
}
