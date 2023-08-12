# AspNetCorePolygonSmartContractIntegration

C# (AspNetCore) integration with Polygon Network Smart Contracts
#polygon #web3 #asp.netcore #c

Polygon Network, formerly known as Matic Network, is a Layer 2 scaling solution for Ethereum. It aims to provide faster and cheaper transactions on the Ethereum network while maintaining the security and decentralization of the underlying blockchain. Polygon achieves this by using a sidechain architecture, where transactions are processed off the main Ethereum chain and then periodically committed to it. This enables faster confirmation times and lower fees compared to using the main Ethereum chain directly. Additionally, Polygon supports the deployment of smart contracts and offers a growing ecosystem of decentralized applications (dApps) and services.

-Connect with Polygon Network
 Connect with Alchemy
 You can sign up/log in to Alchemy and create new app. After that you can get polygon network api key.
 Connect with infura
 You can sign up/log in to https://app.infura.io and enable polygon network to get api key.
![p-1](https://github.com/yuzh37/AspNetCorePolygonSmartContractIntegration/assets/103303397/bb807f53-a686-4ded-960f-647c01e08558)

 Connecting to Polygon chain using C#
 C# (pronounced "C sharp") is a modern, general-purpose programming language developed by Microsoft. It was introduced in the early 2000s as part of the .NET framework and has since become one of the most popular programming languages for building various applications. C# provides a package named "Nethereum" which we can use to connect and communicate with Ethereum and Ethereum-based blockchains, such as Polygon. We are using C# to connect to the Polygon Blockchain due to various benefits like, 
 Benefits
 Wide Developer Adoption: C# is a popular programming language with a large community of developers. It has been widely adopted for building various applications, including blockchain-based ones. This means plenty of resources, libraries, and frameworks are available to support C# developers in their blockchain development journey.

 Interoperability: C# is designed to work seamlessly with the .NET framework, which provides a rich set of tools and libraries for application development. It allows you to integrate blockchain functionality into existing C# applications or develop new blockchain applications from scratch, leveraging the power of the .NET ecosystem.

 Smart Contract Support: With the Nethereum library, which provides a C# implementation for interacting with Ethereum-based blockchains, you can easily interact with smart contracts deployed on the Polygon chain. This enables you to read data from smart contracts, write data to them, and execute functions defined within the smart contracts, all using C# code.

 Security and Performance:  C# is a statically typed language that emphasizes strong type-checking and compile-time verification. This can help reduce programming errors and enhance the security and reliability of your blockchain applications. Additionally, C# is known for its performance optimizations and efficient memory management, which can be beneficial when dealing with the complexities of blockchain transactions and data.

 C# Polygon Implementation
 You can check my repository here.
 https://github.com/yuzh37/AspNetCorePolygonSmartContractIntegration
 * You can generate MyNFT.cs file from https://playground.nethereum.com/ by uploading abi and bytecode files.

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
![p-3](https://github.com/yuzh37/AspNetCorePolygonSmartContractIntegration/assets/103303397/2a530fbf-0426-422a-af74-007b442889e3)
![Uploading p-2.png…]()

  Conclusion
  This article provides the source code for developers interested in connecting to Polygon smart contracts using C#. By leveraging C# for Polygon implementations, developers benefit from broad adoption, interoperability, smart contract support, security, and performance. By following the guidelines outlined in the article, developers can seamlessly integrate Polygon features into their C# applications to extend functionality and leverage the Polygon ecosystem.
