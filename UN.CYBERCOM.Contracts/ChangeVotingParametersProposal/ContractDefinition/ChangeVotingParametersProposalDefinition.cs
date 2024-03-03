using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace UN.CYBERCOM.Contracts.ChangeVotingParametersProposal.ContractDefinition
{


    public partial class ChangeVotingParametersProposalDeployment : ChangeVotingParametersProposalDeploymentBase
    {
        public ChangeVotingParametersProposalDeployment() : base(BYTECODE) { }
        public ChangeVotingParametersProposalDeployment(string byteCode) : base(byteCode) { }
    }

    public class ChangeVotingParametersProposalDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526007805460ff1990811690915560098054909116905534801562000026575f80fd5b50604051620029ed380380620029ed83398101604081905262000049916200074f565b600980546001600160a01b0387811661010002610100600160a81b0319909216919091179091558451600c80549183166001600160a01b03199283161790556020860151600d80549184169183169190911790556040860151600e80549184169183169190911790556060860151600f805491841691831691909117905560808601516010805491841691831691909117905560a08601516011805491841691831691909117905560c08601516012805491909316911617905560028381556003805460ff19168217905585908590859085603c81106200012b57806200012e565b603c5b60045550506005805460ff19169055505042600655505f5b8151811015620004ef576013805460010181555f5281518290829081106200017257620001726200085b565b60200260200101515f0151601382815481106200019357620001936200085b565b905f5260205f2090600702015f0181905550818181518110620001ba57620001ba6200085b565b6020026020010151602001515f015160138281548110620001df57620001df6200085b565b5f9182526020909120600790910201600101805460ff191691151591909117905581518290829081106200021757620002176200085b565b60200260200101516020015160200151601382815481106200023d576200023d6200085b565b905f5260205f2090600702016001015f0160016101000a81548160ff0219169083151502179055508181815181106200027a576200027a6200085b565b6020026020010151602001516040015160138281548110620002a057620002a06200085b565b905f5260205f2090600702016001015f0160026101000a81548163ffffffff021916908363ffffffff160217905550818181518110620002e457620002e46200085b565b60200260200101516020015160600151601382815481106200030a576200030a6200085b565b905f5260205f2090600702016001015f0160066101000a81548163ffffffff021916908363ffffffff1602179055508181815181106200034e576200034e6200085b565b60200260200101516020015160800151601382815481106200037457620003746200085b565b905f5260205f209060070201600101600101819055508181815181106200039f576200039f6200085b565b60200260200101516020015160a0015160138281548110620003c557620003c56200085b565b905f5260205f20906007020160010160020181905550818181518110620003f057620003f06200085b565b60200260200101516020015160c00151601382815481106200041657620004166200085b565b905f5260205f209060070201600101600301819055508181815181106200044157620004416200085b565b60200260200101516020015160e00151601382815481106200046757620004676200085b565b905f5260205f209060070201600101600401819055508181815181106200049257620004926200085b565b602002602001015160200151610100015160138281548110620004b957620004b96200085b565b5f9182526020909120600790910201600601805460ff191691151591909117905580620004e6816200086f565b91505062000146565b50505050505062000894565b80516001600160a01b038116811462000512575f80fd5b919050565b634e487b7160e01b5f52604160045260245ffd5b604080519081016001600160401b038111828210171562000550576200055062000517565b60405290565b60405161012081016001600160401b038111828210171562000550576200055062000517565b60405160e081016001600160401b038111828210171562000550576200055062000517565b604051601f8201601f191681016001600160401b0381118282101715620005cc57620005cc62000517565b604052919050565b8051801515811462000512575f80fd5b805163ffffffff8116811462000512575f80fd5b5f82601f83011262000608575f80fd5b815160206001600160401b0382111562000626576200062662000517565b62000636818360051b01620005a1565b828152610140928302850182019282820191908785111562000656575f80fd5b8387015b8581101562000742578089038281121562000673575f80fd5b6200067d6200052b565b8251815261012080601f198401121562000695575f80fd5b6200069f62000556565b9250620006ae888501620005d4565b83526040620006bf818601620005d4565b898501526060620006d2818701620005e4565b8286015260809150620006e7828701620005e4565b818601525060a0808601518286015260c0915081860151818601525060e08086015182860152610100915081860151818601525062000728828601620005d4565b90840152508087019190915284529284019281016200065a565b5090979650505050505050565b5f805f805f85870361016081121562000766575f80fd5b6200077187620004fb565b955060e0601f198201121562000785575f80fd5b50620007906200057c565b6200079e60208801620004fb565b8152620007ae60408801620004fb565b6020820152620007c160608801620004fb565b6040820152620007d460808801620004fb565b6060820152620007e760a08801620004fb565b6080820152620007fa60c08801620004fb565b60a08201526200080d60e08801620004fb565b60c082015261010087015161012088015161014089015192965090945092506001600160401b0381111562000840575f80fd5b6200084e88828901620005f8565b9150509295509295909350565b634e487b7160e01b5f52603260045260245ffd5b5f600182016200088d57634e487b7160e01b5f52601160045260245ffd5b5060010190565b61214b80620008a25f395ff3fe608060405234801562000010575f80fd5b506004361062000128575f3560e01c806383d948b711620000ab578063b80777ea1162000077578063b80777ea146200027a578063ccbac9f51462000284578063d6bfea28146200028e578063ef2d870014620002a5578063fd50aa4414620002be575f80fd5b806383d948b714620002195780638da5cb5b1462000227578063af640d0f1462000259578063b377a8541462000263575f80fd5b80630fb5a6b411620000f75780630fb5a6b41462000198578063200d2ed214620001b1578063348edff714620001ce578063351d9f9614620001e5578063633dfc701462000202575f80fd5b806302484895146200012c5780630b3af7f9146200014f5780630dc9601514620001685780630f7922351462000181575b5f80fd5b6007546200013a9060ff1681565b60405190151581526020015b60405180910390f35b620001666200016036600462000faa565b620002d7565b005b62000172620003cf565b60405162000146919062000fd1565b620001666200019236600462001058565b62000500565b620001a260045481565b60405190815260200162000146565b600554620001bf9060ff1681565b6040516200014691906200109f565b62000166620001df36600462001197565b6200053e565b600354620001f39060ff1681565b60405162000146919062001241565b62000166620002133660046200125e565b620005e6565b6009546200013a9060ff1681565b600954620002409061010090046001600160a01b031681565b6040516001600160a01b03909116815260200162000146565b620001a260025481565b620001666200027436600462001298565b620007c7565b620001a260065481565b620001a260085481565b620001666200029f366004620012b6565b62000955565b620002af62000985565b6040516200014691906200131f565b620002c862000cc3565b6040516200014691906200151c565b600c546001600160a01b03163314801590620002fe5750600d546001600160a01b03163314155b80156200031657506011546001600160a01b03163314155b80156200032e57506010546001600160a01b03163314155b80156200034b575060095461010090046001600160a01b03163314155b156200036957604051621607ef60ea1b815260040160405180910390fd5b6005805482919060ff191660018360048111156200038b576200038b62001074565b02179055506002547f2da7b23ca63c1eb969eee5fae4acb98186abecf5358b0354a82a5183ebca6b2a82604051620003c491906200109f565b60405180910390a250565b600a546060905f9067ffffffffffffffff811115620003f257620003f2620010cd565b6040519080825280602002602001820160405280156200044457816020015b604080516080810182525f8082526020808301829052928201819052606082015282525f19909201910181620004115790505b5090505f5b600a54811015620004fa57600b5f600a83815481106200046d576200046d620015d9565b5f918252602080832091909101546001600160a01b039081168452838201949094526040928301909120825160808101845281549485168152600160a01b90940460ff161515918401919091526001810154918301919091526002015460608201528251839083908110620004e657620004e6620015d9565b602090810291909101015260010162000449565b50919050565b600d546001600160a01b031633146200052b57604051621607ef60ea1b815260040160405180910390fd5b6007805460ff1916911515919091179055565b600c546001600160a01b03163314801590620005655750600d546001600160a01b03163314155b80156200057d57506011546001600160a01b03163314155b80156200059557506010546001600160a01b03163314155b8015620005b2575060095461010090046001600160a01b03163314155b15620005d057604051621607ef60ea1b815260040160405180910390fd5b620005df858585858562000e3c565b5050505050565b600c546001600160a01b031633148015906200060d5750600d546001600160a01b03163314155b80156200062557506011546001600160a01b03163314155b80156200063d57506010546001600160a01b03163314155b156200065b57604051621607ef60ea1b815260040160405180910390fd5b60095460ff166200067f57604051633fd0090160e11b815260040160405180910390fd5b600454421115620006a35760405163335b65a560e11b815260040160405180910390fd5b600280546001600160a01b0383165f908152600b6020526040902090910154146200071357600a80546001810182555f919091527fc65a7bb8d6351c1cf70c95a316cc6a92839c986682d98bc35f958f4883f9d2a80180546001600160a01b0319166001600160a01b0383161790555b604080516080810182526001600160a01b0383811680835285151560208085018281524286880190815260028054606089019081525f878152600b86528a90209851895494511515600160a01b026001600160a81b0319909516981697909717929092178755516001870155935194840194909455915484519182529281019190915290917f5aaa9aad7433112662b9e5ae23b96ed62b00035f413ab908c55607284e0804e2910160405180910390a25050565b600c546001600160a01b03163314801590620007ee5750600d546001600160a01b03163314155b80156200080657506011546001600160a01b03163314155b80156200081e57506010546001600160a01b03163314155b156200083c57604051621607ef60ea1b815260040160405180910390fd5b6009546001600160a01b038281166101009092041614620008a45760405162461bcd60e51b815260206004820152601b60248201527f4f6e6c79206f776e65722063616e20737461727420766f74696e67000000000060448201526064015b60405180910390fd5b60095460ff1615620008f25760405162461bcd60e51b8152602060048201526016602482015275159bdd1a5b99c8185b1c9958591e481cdd185c9d195960521b60448201526064016200089b565b6009805460ff19166001179055600480544291905f906200091590849062001601565b90915550506005805460ff191660011790556002546040517fcf33babc496bb6dc2942b39cb7b75766bbbadf7da50d176ff8c513e991140239905f90a250565b600d546001600160a01b031633146200098057604051621607ef60ea1b815260040160405180910390fd5b600855565b6001546060905f9067ffffffffffffffff811115620009a857620009a8620010cd565b60405190808252806020026020018201604052801562000a0b57816020015b6040805160c0810182526060808252602082018190525f928201839052808201526080810182905260a0810191909152815260200190600190039081620009c75790505b5090505f5b8151811015620004fa575f806001838154811062000a325762000a32620015d9565b905f5260205f200160405162000a4991906200164b565b908152604080519182900360200182205460c0830180835263129e754360e21b905290516001600160a01b03909116925081908390634a79d50c9060c4808501915f918187030181865afa15801562000aa4573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f1916820160405262000acd9190810190620016f9565b8152602001826001600160a01b0316635600f04f6040518163ffffffff1660e01b81526004015f60405180830381865afa15801562000b0e573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f1916820160405262000b379190810190620016f9565b8152602001826001600160a01b03166310c83e536040518163ffffffff1660e01b8152600401602060405180830381865afa15801562000b79573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019062000b9f91906200174b565b8152602001826001600160a01b03166351ff48476040518163ffffffff1660e01b81526004015f60405180830381865afa15801562000be0573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f1916820160405262000c099190810190620016f9565b8152602001826001600160a01b031663238ac9336040518163ffffffff1660e01b8152600401602060405180830381865afa15801562000c4b573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019062000c71919062001763565b6001600160a01b03168152602001826001600160a01b031681525083838151811062000ca15762000ca1620015d9565b6020026020010181905250818062000cb99062001781565b9250505062000a10565b62000ccd62000f40565b60405180610120016040528060025481526020016013805480602002602001604051908101604052809291908181526020015f905b8282101562000dc2575f848152602090819020604080518082018252600786029092018054835281516101208101835260018083015460ff8082161515845261010080830482161515858a015263ffffffff6201000084048116978601979097526601000000000000909204909516606084015260028401546080840152600384015460a0840152600484015460c0840152600584015460e084015260069093015490931615159181019190915282840152908352909201910162000d02565b50505050815260200162000dd5620003cf565b815260048054602083015260055460409092019160ff169081111562000dff5762000dff62001074565b815260075460ff908116151560208301526009549081161515604083015261010090046001600160a01b0316606082015230608090910152919050565b60405183905f90819062000e529084906200179c565b908152604051908190036020019020546001600160a01b03161462000e75575f80fd5b5f30878486888a60405162000e8a9062000f9c565b62000e9b96959493929190620017b9565b604051809103905ff08015801562000eb5573d5f803e3d5ffd5b506001805480820182555f919091529091507fb10e2d527612073b26eecdfd717e6a320cf44b4afac2b0732d9fcbe2b7fa0cf60162000ef5868262001870565b50805f8360405162000f0891906200179c565b90815260405190819003602001902080546001600160a01b03929092166001600160a01b031990921691909117905550505050505050565b6040518061012001604052805f815260200160608152602001606081526020015f81526020015f600481111562000f7b5762000f7b62001074565b81525f60208201819052604082018190526060820181905260809091015290565b6107d8806200193e83390190565b5f6020828403121562000fbb575f80fd5b81356005811062000fca575f80fd5b9392505050565b602080825282518282018190525f9190848201906040850190845b818110156200103757835180516001600160a01b0316845260208082015115159085015260408082015190850152606090810151908401526080830193850193925060010162000fec565b50909695505050505050565b8035801515811462001053575f80fd5b919050565b5f6020828403121562001069575f80fd5b62000fca8262001043565b634e487b7160e01b5f52602160045260245ffd5b600581106200109b576200109b62001074565b9052565b60208101620010af828462001088565b92915050565b6001600160a01b0381168114620010ca575f80fd5b50565b634e487b7160e01b5f52604160045260245ffd5b604051601f8201601f1916810167ffffffffffffffff811182821017156200110d576200110d620010cd565b604052919050565b5f67ffffffffffffffff821115620011315762001131620010cd565b50601f01601f191660200190565b5f82601f8301126200114f575f80fd5b813562001166620011608262001115565b620010e1565b8181528460208386010111156200117b575f80fd5b816020850160208301375f918101602001919091529392505050565b5f805f805f60a08688031215620011ac575f80fd5b8535620011b981620010b5565b9450602086013567ffffffffffffffff80821115620011d6575f80fd5b620011e489838a016200113f565b95506040880135915080821115620011fa575f80fd5b6200120889838a016200113f565b945060608801359350608088013591508082111562001225575f80fd5b5062001234888289016200113f565b9150509295509295909350565b602081016004831062001258576200125862001074565b91905290565b5f806040838503121562001270575f80fd5b6200127b8362001043565b915060208301356200128d81620010b5565b809150509250929050565b5f60208284031215620012a9575f80fd5b813562000fca81620010b5565b5f60208284031215620012c7575f80fd5b5035919050565b5f5b83811015620012ea578181015183820152602001620012d0565b50505f910152565b5f81518084526200130b816020860160208601620012ce565b601f01601f19169290920160200192915050565b5f60208083018184528085518083526040925060408601915060408160051b8701018488015f5b83811015620013e857603f19898403018552815160c081518186526200136f82870182620012f2565b915050888201518582038a870152620013898282620012f2565b915050878201518886015260608083015186830382880152620013ad8382620012f2565b6080858101516001600160a01b03908116918a019190915260a095860151169490970193909352505050938601939086019060010162001346565b509098975050505050505050565b5f8151808452602080850194508084015f5b83811015620014b35781518051885283015180511515848901528084015115156040808a0191909152810151606062001448818b018363ffffffff169052565b82015190506080620014618a82018363ffffffff169052565b82015160a08a81019190915282015160c0808b019190915282015160e0808b0191909152820151610100808b019190915290910151151561012089015250610140909601959082019060010162001408565b509495945050505050565b5f815180845260208085019450602084015f5b83811015620014b357815180516001600160a01b03168852602080820151151590890152604080820151908901526060908101519088015260808701965090820190600101620014d1565b60208152815160208201525f602083015161012080604085015262001546610140850183620013f6565b91506040850151601f19858403016060860152620015658382620014be565b9250506060850151608085015260808501516200158660a086018262001088565b5060a085015180151560c08601525060c085015180151560e08601525060e0850151610100620015c0818701836001600160a01b03169052565b909501516001600160a01b031693019290925250919050565b634e487b7160e01b5f52603260045260245ffd5b634e487b7160e01b5f52601160045260245ffd5b80820180821115620010af57620010af620015ed565b600181811c908216806200162c57607f821691505b602082108103620004fa57634e487b7160e01b5f52602260045260245ffd5b5f8083546200165a8162001617565b600182811680156200167557600181146200168b57620016b9565b60ff1984168752821515830287019450620016b9565b875f526020805f205f5b85811015620016b05781548a82015290840190820162001695565b50505082870194505b50929695505050505050565b5f620016d5620011608462001115565b9050828152838383011115620016e9575f80fd5b62000fca836020830184620012ce565b5f602082840312156200170a575f80fd5b815167ffffffffffffffff81111562001721575f80fd5b8201601f8101841362001732575f80fd5b6200174384825160208401620016c5565b949350505050565b5f602082840312156200175c575f80fd5b5051919050565b5f6020828403121562001774575f80fd5b815162000fca81620010b5565b5f60018201620017955762001795620015ed565b5060010190565b5f8251620017af818460208701620012ce565b9190910192915050565b6001600160a01b0387811682528616602082015260c0604082018190525f90620017e690830187620012f2565b8560608401528281036080840152620018008186620012f2565b905082810360a0840152620018168185620012f2565b9998505050505050505050565b601f8211156200186b57805f5260205f20601f840160051c810160208510156200184a5750805b601f840160051c820191505b81811015620005df575f815560010162001856565b505050565b815167ffffffffffffffff8111156200188d576200188d620010cd565b620018a5816200189e845462001617565b8462001823565b602080601f831160018114620018db575f8415620018c35750858301515b5f19600386901b1c1916600185901b17855562001935565b5f85815260208120601f198616915b828110156200190b57888601518255948401946001909101908401620018ea565b50858210156200192957878501515f19600388901b60f8161c191681555b505060018460011b0185555b50505050505056fe608060405234801561000f575f80fd5b506040516107d83803806107d883398101604081905261002e9161033e565b5f6100398282610483565b5060016100468382610483565b50600283905560036100588582610483565b50600480546001600160a01b038088166001600160a01b0319928316179092556005805492891692909116919091179055426006556002546040515f916100cd916020017f19457468657265756d205369676e6564204d6573736167653a0a3332000000008152601c810191909152603c0190565b60408051601f19818403018152919052805160209091012090505f80806100f48489610156565b919450925090505f82600381111561010e5761010e610542565b14158061012957506004546001600160a01b03848116911614155b1561014757604051638baa579f60e01b815260040160405180910390fd5b50505050505050505050610556565b5f805f835160410361018d576020840151604085015160608601515f1a61017f8882858561019f565b955095509550505050610198565b505081515f91506002905b9250925092565b5f80806fa2a8918ca85bafe22016d0b997e4df60600160ff1b038411156101ce57505f91506003905082610253565b604080515f808252602082018084528a905260ff891692820192909252606081018790526080810186905260019060a0016020604051602081039080840390855afa15801561021f573d5f803e3d5ffd5b5050604051601f1901519150506001600160a01b03811661024a57505f925060019150829050610253565b92505f91508190505b9450945094915050565b80516001600160a01b0381168114610273575f80fd5b919050565b634e487b7160e01b5f52604160045260245ffd5b5f6001600160401b03808411156102a5576102a5610278565b604051601f8501601f19908116603f011681019082821181831017156102cd576102cd610278565b816040528093508581528686860111156102e5575f80fd5b5f92505b858310156103075782850151602084830101526020830192506102e9565b5f602087830101525050509392505050565b5f82601f830112610328575f80fd5b6103378383516020850161028c565b9392505050565b5f805f805f8060c08789031215610353575f80fd5b61035c8761025d565b955061036a6020880161025d565b60408801519095506001600160401b0380821115610386575f80fd5b818901915089601f830112610399575f80fd5b6103a88a83516020850161028c565b95506060890151945060808901519150808211156103c4575f80fd5b6103d08a838b01610319565b935060a08901519150808211156103e5575f80fd5b506103f289828a01610319565b9150509295509295509295565b600181811c9082168061041357607f821691505b60208210810361043157634e487b7160e01b5f52602260045260245ffd5b50919050565b601f82111561047e57805f5260205f20601f840160051c8101602085101561045c5750805b601f840160051c820191505b8181101561047b575f8155600101610468565b50505b505050565b81516001600160401b0381111561049c5761049c610278565b6104b0816104aa84546103ff565b84610437565b602080601f8311600181146104e3575f84156104cc5750858301515b5f19600386901b1c1916600185901b17855561053a565b5f85815260208120601f198616915b82811015610511578886015182559484019460019091019084016104f2565b508582101561052e57878501515f19600388901b60f8161c191681555b505060018460011b0185555b505050505050565b634e487b7160e01b5f52602160045260245ffd5b610275806105635f395ff3fe608060405234801561000f575f80fd5b506004361061007a575f3560e01c806351ff48471161005857806351ff4847146100da5780635600f04f146100e2578063b80777ea146100ea578063ca973727146100f3575f80fd5b806310c83e531461007e578063238ac9331461009a5780634a79d50c146100c5575b5f80fd5b61008760025481565b6040519081526020015b60405180910390f35b6004546100ad906001600160a01b031681565b6040516001600160a01b039091168152602001610091565b6100cd610106565b60405161009191906101ee565b6100cd610191565b6100cd61019e565b61008760065481565b6005546100ad906001600160a01b031681565b5f805461011290610207565b80601f016020809104026020016040519081016040528092919081815260200182805461013e90610207565b80156101895780601f1061016057610100808354040283529160200191610189565b820191905f5260205f20905b81548152906001019060200180831161016c57829003601f168201915b505050505081565b6003805461011290610207565b6001805461011290610207565b5f81518084525f5b818110156101cf576020818501810151868301820152016101b3565b505f602082860101526020601f19601f83011685010191505092915050565b602081525f61020060208301846101ab565b9392505050565b600181811c9082168061021b57607f821691505b60208210810361023957634e487b7160e01b5f52602260045260245ffd5b5091905056fea2646970667358221220fa25b681e2df8404ceb35c87d803e7960ecc7a79060e164c200635d40ca986cd64736f6c63430008170033a2646970667358221220518822537c34e1a42d24188d5b90c35987973306f08b8118fe23c48dbba6213964736f6c63430008170033";
        public ChangeVotingParametersProposalDeploymentBase() : base(BYTECODE) { }
        public ChangeVotingParametersProposalDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("tuple", "_contractAddresses", 2)]
        public virtual ContractAddresses ContractAddresses { get; set; }
        [Parameter("uint256", "_id", 3)]
        public virtual BigInteger Id { get; set; }
        [Parameter("uint256", "_duration", 4)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("tuple[]", "_parameters", 5)]
        public virtual List<ChangeVotingParametersRole> Parameters { get; set; }
    }

    public partial class AddDocumentFunction : AddDocumentFunctionBase { }

    [Function("addDocument")]
    public class AddDocumentFunctionBase : FunctionMessage
    {
        [Parameter("address", "signer", 1)]
        public virtual string Signer { get; set; }
        [Parameter("string", "title", 2)]
        public virtual string Title { get; set; }
        [Parameter("string", "url", 3)]
        public virtual string Url { get; set; }
        [Parameter("bytes32", "docHash", 4)]
        public virtual byte[] DocHash { get; set; }
        [Parameter("bytes", "signature", 5)]
        public virtual byte[] Signature { get; set; }
    }

    public partial class DurationFunction : DurationFunctionBase { }

    [Function("duration", "uint256")]
    public class DurationFunctionBase : FunctionMessage
    {

    }

    public partial class GetChangeResponseFunction : GetChangeResponseFunctionBase { }

    [Function("getChangeResponse", typeof(GetChangeResponseOutputDTO))]
    public class GetChangeResponseFunctionBase : FunctionMessage
    {

    }

    public partial class GetDocumentsFunction : GetDocumentsFunctionBase { }

    [Function("getDocuments", typeof(GetDocumentsOutputDTO))]
    public class GetDocumentsFunctionBase : FunctionMessage
    {

    }

    public partial class GetVotesFunction : GetVotesFunctionBase { }

    [Function("getVotes", typeof(GetVotesOutputDTO))]
    public class GetVotesFunctionBase : FunctionMessage
    {

    }

    public partial class IdFunction : IdFunctionBase { }

    [Function("id", "uint256")]
    public class IdFunctionBase : FunctionMessage
    {

    }

    public partial class IsProcessingFunction : IsProcessingFunctionBase { }

    [Function("isProcessing", "bool")]
    public class IsProcessingFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class ProposalTypeFunction : ProposalTypeFunctionBase { }

    [Function("proposalType", "uint8")]
    public class ProposalTypeFunctionBase : FunctionMessage
    {

    }

    public partial class RandomNumberFunction : RandomNumberFunctionBase { }

    [Function("randomNumber", "uint256")]
    public class RandomNumberFunctionBase : FunctionMessage
    {

    }

    public partial class SetProcessingFunction : SetProcessingFunctionBase { }

    [Function("setProcessing")]
    public class SetProcessingFunctionBase : FunctionMessage
    {
        [Parameter("bool", "processing", 1)]
        public virtual bool Processing { get; set; }
    }

    public partial class SetRandomNumberFunction : SetRandomNumberFunctionBase { }

    [Function("setRandomNumber")]
    public class SetRandomNumberFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "random", 1)]
        public virtual BigInteger Random { get; set; }
    }

    public partial class StartVotingFunction : StartVotingFunctionBase { }

    [Function("startVoting")]
    public class StartVotingFunctionBase : FunctionMessage
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
    }

    public partial class StatusFunction : StatusFunctionBase { }

    [Function("status", "uint8")]
    public class StatusFunctionBase : FunctionMessage
    {

    }

    public partial class TimestampFunction : TimestampFunctionBase { }

    [Function("timestamp", "uint256")]
    public class TimestampFunctionBase : FunctionMessage
    {

    }

    public partial class UpdateStatusFunction : UpdateStatusFunctionBase { }

    [Function("updateStatus")]
    public class UpdateStatusFunctionBase : FunctionMessage
    {
        [Parameter("uint8", "_status", 1)]
        public virtual byte Status { get; set; }
    }

    public partial class VoteFunction : VoteFunctionBase { }

    [Function("vote")]
    public class VoteFunctionBase : FunctionMessage
    {
        [Parameter("bool", "voteCasted", 1)]
        public virtual bool VoteCasted { get; set; }
        [Parameter("address", "member", 2)]
        public virtual string Member { get; set; }
    }

    public partial class VotingStartedFunction : VotingStartedFunctionBase { }

    [Function("votingStarted", "bool")]
    public class VotingStartedFunctionBase : FunctionMessage
    {

    }

    public partial class StatusUpdatedEventDTO : StatusUpdatedEventDTOBase { }

    [Event("StatusUpdated")]
    public class StatusUpdatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("uint8", "newStatus", 2, false )]
        public virtual byte NewStatus { get; set; }
    }

    public partial class VoteCastedEventDTO : VoteCastedEventDTOBase { }

    [Event("VoteCasted")]
    public class VoteCastedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("address", "member", 2, false )]
        public virtual string Member { get; set; }
        [Parameter("bool", "vote", 3, false )]
        public virtual bool Vote { get; set; }
    }

    public partial class VotingCompletedEventDTO : VotingCompletedEventDTOBase { }

    [Event("VotingCompleted")]
    public class VotingCompletedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class VotingStartedEventDTO : VotingStartedEventDTOBase { }

    [Event("VotingStarted")]
    public class VotingStartedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class AuthorizationErrorError : AuthorizationErrorErrorBase { }
    [Error("AuthorizationError")]
    public class AuthorizationErrorErrorBase : IErrorDTO
    {
    }

    public partial class VotingClosedError : VotingClosedErrorBase { }
    [Error("VotingClosed")]
    public class VotingClosedErrorBase : IErrorDTO
    {
    }

    public partial class VotingNotStartedError : VotingNotStartedErrorBase { }
    [Error("VotingNotStarted")]
    public class VotingNotStartedErrorBase : IErrorDTO
    {
    }



    public partial class DurationOutputDTO : DurationOutputDTOBase { }

    [FunctionOutput]
    public class DurationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetChangeResponseOutputDTO : GetChangeResponseOutputDTOBase { }

    [FunctionOutput]
    public class GetChangeResponseOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "", 1)]
        public virtual ChangeVotingParametersResponse ReturnValue1 { get; set; }
    }

    public partial class GetDocumentsOutputDTO : GetDocumentsOutputDTOBase { }

    [FunctionOutput]
    public class GetDocumentsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Doc> ReturnValue1 { get; set; }
    }

    public partial class GetVotesOutputDTO : GetVotesOutputDTOBase { }

    [FunctionOutput]
    public class GetVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Vote> ReturnValue1 { get; set; }
    }

    public partial class IdOutputDTO : IdOutputDTOBase { }

    [FunctionOutput]
    public class IdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class IsProcessingOutputDTO : IsProcessingOutputDTOBase { }

    [FunctionOutput]
    public class IsProcessingOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ProposalTypeOutputDTO : ProposalTypeOutputDTOBase { }

    [FunctionOutput]
    public class ProposalTypeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class RandomNumberOutputDTO : RandomNumberOutputDTOBase { }

    [FunctionOutput]
    public class RandomNumberOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }







    public partial class StatusOutputDTO : StatusOutputDTOBase { }

    [FunctionOutput]
    public class StatusOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class TimestampOutputDTO : TimestampOutputDTOBase { }

    [FunctionOutput]
    public class TimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class VotingStartedOutputDTO : VotingStartedOutputDTOBase { }

    [FunctionOutput]
    public class VotingStartedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}