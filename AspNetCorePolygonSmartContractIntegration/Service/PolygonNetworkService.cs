using Microsoft.Extensions.Configuration;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;
using static AspNetCorePolygonSmartContractIntegration.Contracts.MyNFT;

namespace AspNetCorePolygonSmartContractIntegration.Service
{
    public class PolygonNetworkService : IPolygonNetworkService
    {
        private readonly IConfiguration _configuration;
        
        private string _alchemyApiKey;
        private string _infuraApiKey;
        private string _contractPrivateKey;
        private string _contractAddress;
        
        public PolygonNetworkService(IConfiguration configuration)
        {
            _configuration = configuration;
            _alchemyApiKey = _configuration.GetValue<string>("AlchemyApiKey");
            _contractPrivateKey = _configuration.GetValue<string>("ContractPrivateKey");
            _contractAddress = _configuration.GetValue<string>("ContractAddress");
            _infuraApiKey = _configuration.GetValue<string>("InfuraApiKey");
        }
        public bool ConnectWeb3()
        {
            string rpcUrl = $"https://polygon-mumbai.g.alchemy.com/v2/{_alchemyApiKey}";
            var web3 = new Web3(rpcUrl);
            if (web3.Eth.ChainId.SendRequestAsync().Result.ToString() == "80001")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task ConnectSmartContractUsingAlchemy()
        {
            string rpcUrl = $"https://polygon-mumbai.g.alchemy.com/v2/{_alchemyApiKey}";
            var account = new Account(_contractPrivateKey);        // Instance of sender's account using sender's private key
            var Accountweb3 = new Web3(account, rpcUrl);

            if (ConnectWeb3())
            {
                var myNFTDeployment = new MyNFTDeployment();

                var transactionReceiptDeployment = await Accountweb3.Eth.GetContractDeploymentHandler<MyNFTDeployment>().SendRequestAndWaitForReceiptAsync(myNFTDeployment);
                var contractAddress = transactionReceiptDeployment.ContractAddress;
                var contractHandler = Accountweb3.Eth.GetContractHandler(_contractAddress);

                var mintNFTFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync<MintNFTFunction>();

                var getNFTDetailFunction = new GetNFTDetailFunction();
                getNFTDetailFunction.ItemId = 1;
                var getNFTDetailOutputDTO = await contractHandler.QueryDeserializingToObjectAsync<GetNFTDetailFunction, GetNFTDetailOutputDTO>(getNFTDetailFunction);
            }
        }

        public async Task ConnectSmartContractUsingNethereumTestChain()
        {
            var url = "http://testchain.nethereum.com:8545";
            var account = new Nethereum.Web3.Accounts.Account(_contractPrivateKey);
            var web3 = new Web3(account, url);

            var myNFTDeployment = new MyNFTDeployment();

            var transactionReceiptDeployment = await web3.Eth.GetContractDeploymentHandler<MyNFTDeployment>().SendRequestAndWaitForReceiptAsync(myNFTDeployment);
            var contractAddress = transactionReceiptDeployment.ContractAddress;

            var contractHandler = web3.Eth.GetContractHandler(contractAddress);
            var mintNFTFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync<MintNFTFunction>();

            var totalNFTsMintedFunctionReturn = await contractHandler.QueryAsync<TotalNFTsMintedFunction, BigInteger>();
        }

        public async Task ConnectSmartContractUsingInfura()
        {
            var url = $"https://polygon-mumbai.infura.io/v3/{_infuraApiKey}";
            var account = new Nethereum.Web3.Accounts.Account(_contractPrivateKey);
            var web3 = new Web3(account, url);

            var contractHandler = web3.Eth.GetContractHandler(_contractAddress);
            var mintNFTFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync<MintNFTFunction>();

            var getNFTDetailFunction = new GetNFTDetailFunction();
            getNFTDetailFunction.ItemId = 1;
            var getNFTDetailOutputDTO = await contractHandler.QueryDeserializingToObjectAsync<GetNFTDetailFunction, GetNFTDetailOutputDTO>(getNFTDetailFunction);
        }
    }
}
