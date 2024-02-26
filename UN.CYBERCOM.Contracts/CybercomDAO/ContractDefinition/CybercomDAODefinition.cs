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

namespace UN.CYBERCOM.Contracts.CybercomDAO.ContractDefinition
{


    public partial class CybercomDAODeployment : CybercomDAODeploymentBase
    {
        public CybercomDAODeployment() : base(BYTECODE) { }
        public CybercomDAODeployment(string byteCode) : base(byteCode) { }
    }

    public class CybercomDAODeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052603c6002556009805460ff60401b19169055348015610021575f80fd5b50604051611eaa380380611eaa83398101604081905261004091610069565b60015f55600980546001600160401b0319166001600160401b0392909216919091179055610096565b5f60208284031215610079575f80fd5b81516001600160401b038116811461008f575f80fd5b9392505050565b611e07806100a35f395ff3fe608060405234801561000f575f80fd5b5060043610610111575f3560e01c806379ad2c341161009e578063a217fddf1161006e578063a217fddf14610393578063b31f111c1461039a578063d547741f146103ad578063e93b1111146103c0578063f5ee070f146103d3575f80fd5b806379ad2c341461026057806391d148541461028b578063953874d21461029e5780639696967914610380575f80fd5b806336568abe116100e457806336568abe146101965780633d2f5bda146101a957806360fc5a9a146101bc57806369b8c628146101d95780636c0f79b6146101ec575f80fd5b806301ffc9a714610115578063248a9ca31461013d5780632dba4e0f1461016e5780632f2ff15d14610183575b5f80fd5b6101286101233660046116fa565b6103e6565b60405190151581526020015b60405180910390f35b61016061014b366004611721565b5f908152600160208190526040909120015490565b604051908152602001610134565b61018161017c366004611721565b61041c565b005b61018161019136600461175c565b610aff565b6101816101a436600461175c565b610b2a565b6101816101b7366004611721565b610b62565b6101816009805468ff00000000000000001916600160401b179055565b6101816101e7366004611721565b610d1e565b60035460045460055460065460075460085461021e956001600160a01b03908116958116948116938116928116911686565b604080516001600160a01b03978816815295871660208701529386169385019390935290841660608401528316608083015290911660a082015260c001610134565b61027361026e36600461181b565b610e8e565b6040516001600160a01b039091168152602001610134565b61012861029936600461175c565b61100b565b6103256040805160c0810182525f80825260208201819052918101829052606081018290526080810182905260a0810191909152506040805160c0810182526003546001600160a01b03908116825260045481166020830152600554811692820192909252600654821660608201526007548216608082015260085490911660a082015290565b604051610134919081516001600160a01b03908116825260208084015182169083015260408084015182169083015260608084015182169083015260808084015182169083015260a092830151169181019190915260c00190565b61027361038e3660046118a9565b611035565b6101605f81565b6101816103a83660046119c4565b6110d4565b6101816103bb36600461175c565b6112df565b6101816103ce3660046119eb565b611304565b6101816103e1366004611a06565b61133a565b5f6001600160e01b03198216637965db0b60e01b148061041657506301ffc9a760e01b6001600160e01b03198316145b92915050565b6005546040805163f2bcac3d60e01b815290516001600160a01b03909216915f91839163f2bcac3d9160048082019286929091908290030181865afa158015610467573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f1916820160405261048e9190810190611aaf565b90505f805b82518210156104de576104bf8383815181106104b1576104b1611b50565b60200260200101513361100b565b156104cc575060016104de565b816104d681611b64565b925050610493565b806104fb57604051621607ef60ea1b815260040160405180910390fd5b6006546040516318feeb1560e31b8152600481018790526001600160a01b03909116905f90829063c7f758a890602401602060405180830381865afa158015610546573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019061056a9190611b88565b90506001600160a01b03811661059357604051631dc0650160e31b815260040160405180910390fd5b806002816001600160a01b031663200d2ed26040518163ffffffff1660e01b8152600401602060405180830381865afa1580156105d2573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906105f69190611bb7565b600481111561060757610607611ba3565b1461062557604051633d63c4cd60e01b815260040160405180910390fd5b60048054604051632698c58760e11b81529182018a90526001600160a01b0316905f908290634d318b0e906024016020604051808303815f875af115801561066f573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906106939190611bb7565b905060038160048111156106a9576106a9611ba3565b0361081557604051630b3af7f960e01b81526001600160a01b03841690630b3af7f9906106db90600390600401611bd5565b5f604051808303815f87803b1580156106f2575f80fd5b505af1158015610704573d5f803e3d5ffd5b505f9250610710915050565b836001600160a01b031663351d9f966040518163ffffffff1660e01b8152600401602060405180830381865afa15801561074c573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906107709190611bfb565b600381111561078157610781611ba3565b036107945761078f846113e9565b610abb565b6003836001600160a01b031663351d9f966040518163ffffffff1660e01b8152600401602060405180830381865afa1580156107d2573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906107f69190611bfb565b600381111561080757610807611ba3565b0361078f5761078f846114a2565b600481600481111561082957610829611ba3565b03610abb57604051630b3af7f960e01b81526001600160a01b03841690630b3af7f99061085a906004908101611bd5565b5f604051808303815f87803b158015610871575f80fd5b505af1158015610883573d5f803e3d5ffd5b505f925061088f915050565b836001600160a01b031663351d9f966040518163ffffffff1660e01b8152600401602060405180830381865afa1580156108cb573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906108ef9190611bfb565b600381111561090057610900611ba3565b036109a6575f839050806001600160a01b031663b46a357f6040518163ffffffff1660e01b81526004015f60405180830381865afa158015610944573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f1916820160405261096b9190810190611c3b565b516040516001600160a01b03909116907fa846d52f1d59acce7c23d1e0fc638d46fc821ca5ef7d94f6ee01175db485a687905f90a250610abb565b6003836001600160a01b031663351d9f966040518163ffffffff1660e01b8152600401602060405180830381865afa1580156109e4573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190610a089190611bfb565b6003811115610a1957610a19611ba3565b03610abb575f839050806001600160a01b031663b46a357f6040518163ffffffff1660e01b81526004015f60405180830381865afa158015610a5d573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f19168201604052610a849190810190611c3b565b516040516001600160a01b03909116907fdc6b0ed1742a774ae82e5667826b5a48c80092a61d8e6a6a1c430dc79e538873905f90a2505b897f2dfd36efa3612b4a9efa853af9534e461bbbfc1267193060695ac833416200e782604051610aeb9190611bd5565b60405180910390a250505050505050505050565b5f8281526001602081905260409091200154610b1a816115ce565b610b2483836115d8565b50505050565b6001600160a01b0381163314610b535760405163334bd91960e11b815260040160405180910390fd5b610b5d828261164e565b505050565b6006546040516318feeb1560e31b8152600481018390526001600160a01b03909116905f90829063c7f758a890602401602060405180830381865afa158015610bad573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190610bd19190611b88565b90506001600160a01b038116610bfa57604051631dc0650160e31b815260040160405180910390fd5b5f819050336001600160a01b0316816001600160a01b0316638da5cb5b6040518163ffffffff1660e01b8152600401602060405180830381865afa158015610c44573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190610c689190611b88565b6001600160a01b031614610c8f576040516330cd747160e01b815260040160405180910390fd5b604051632cddea1560e21b81523360048201526001600160a01b0382169063b377a854906024015f604051808303815f87803b158015610ccd575f80fd5b505af1158015610cdf573d5f803e3d5ffd5b50506040513381528692507f8610d91e9f8a63773d7a3c13aa8bb8407203532a61bb703c06fce895f9622d0d915060200160405180910390a250505050565b6005546040805163f2bcac3d60e01b815290516001600160a01b03909216915f91839163f2bcac3d9160048082019286929091908290030181865afa158015610d69573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f19168201604052610d909190810190611aaf565b90505f805b8251821015610dd257610db38383815181106104b1576104b1611b50565b15610dc057506001610dd2565b81610dca81611b64565b925050610d95565b80610def57604051621607ef60ea1b815260040160405180910390fd5b60048054604051630d3718c560e31b81529182018790526001600160a01b03169081906369b8c628906024016020604051808303815f875af1158015610e37573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190610e5b9190611cf2565b5060405186907f167f0215050eb1c53e39e2bca4aef866c9f101a744be78e3d0ceda7218535226905f90a2505050505050565b6005546040805163f2bcac3d60e01b815290515f926001600160a01b0316918391839163f2bcac3d91600480830192869291908290030181865afa158015610ed8573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f19168201604052610eff9190810190611aaf565b90505f805b8251821015610f4157610f228383815181106104b1576104b1611b50565b15610f2f57506001610f41565b81610f3981611b64565b925050610f04565b80610f5e57604051621607ef60ea1b815260040160405180910390fd5b60025486602001511015610f755760025460208701525b336040878101918252600754905163e715c60760e01b815288516001600160a01b03908116600483015260208a01516024830152925183166044820152911690819063e715c607906064016020604051808303815f875af1158015610fdc573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906110009190611b88565b979650505050505050565b5f9182526001602090815260408084206001600160a01b0393909316845291905290205460ff1690565b60085433608083015260025460608301515f926001600160a01b03169111156110615760025460608401525b604051639696967960e01b81526001600160a01b0382169063969696799061108d908690600401611d09565b6020604051808303815f875af11580156110a9573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906110cd9190611b88565b9392505050565b6005546040805163f2bcac3d60e01b815290516001600160a01b03909216915f91839163f2bcac3d9160048082019286929091908290030181865afa15801561111f573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f191682016040526111469190810190611aaf565b90505f805b8251821015611188576111698383815181106104b1576104b1611b50565b1561117657506001611188565b8161118081611b64565b92505061114b565b806111a557604051621607ef60ea1b815260040160405180910390fd5b6006546040516318feeb1560e31b8152600481018890526001600160a01b03909116905f90829063c7f758a890602401602060405180830381865afa1580156111f0573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906112149190611b88565b90506001600160a01b03811661123d57604051631dc0650160e31b815260040160405180910390fd5b604051630633dfc760e41b8152871515600482015233602482015281906001600160a01b0382169063633dfc70906044015f604051808303815f87803b158015611285575f80fd5b505af1158015611297573d5f803e3d5ffd5b50506040518a151581523392508b91507fe71fcdac32df1877c1700e7bda2a03157e20993363a28fc35ac495cefc76e4d49060200160405180910390a3505050505050505050565b5f82815260016020819052604090912001546112fa816115ce565b610b24838361164e565b6008546001600160a01b0316331461132e57604051621607ef60ea1b815260040160405180910390fd5b611337816113e9565b50565b600954600160401b900460ff16156113645760405162dc149f60e41b815260040160405180910390fd5b8051600380546001600160a01b03199081166001600160a01b0393841617909155602083015160048054831691841691909117905560408301516005805483169184169190911790556060830151600680548316918416919091179055608083015160078054831691841691909117905560a090920151600880549093169116179055565b60055460405162ed40bd60e41b81526001600160a01b038381166004830152909116905f9081908390630ed40bd09060240160408051808303815f875af1158015611436573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019061145a9190611da5565b9150915061146881836115d8565b506040516001600160a01b038316907f8dedfed9426ac9fd03a4a7df2f9eb6439a2b8c9376b9bbb0ff8167c1e601b9aa905f90a250505050565b6005546040805163b46a357f60e01b815290516001600160a01b039283169284925f929184169163b46a357f9160048082019286929091908290030181865afa1580156114f1573d5f803e3d5ffd5b505050506040513d5f823e601f3d908101601f191682016040526115189190810190611c3b565b5160405163c4bdfb1f60e01b81526001600160a01b0380831660048301529192505f9185169063c4bdfb1f906024016020604051808303815f875af1158015611563573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906115879190611cf2565b9050611593818361164e565b506040516001600160a01b038316907f6e76fb4c77256006d9c38ec7d82b45a8c8f3c27b1d6766fffc42dfb8de684492905f90a25050505050565b61133781336116b9565b5f6115e3838361100b565b611647575f8381526001602081815260408084206001600160a01b0387168086529252808420805460ff19169093179092559051339286917f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d9190a4506001610416565b505f610416565b5f611659838361100b565b15611647575f8381526001602090815260408083206001600160a01b0386168085529252808320805460ff1916905551339286917ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b9190a4506001610416565b6116c3828261100b565b6116f65760405163e2517d3f60e01b81526001600160a01b03821660048201526024810183905260440160405180910390fd5b5050565b5f6020828403121561170a575f80fd5b81356001600160e01b0319811681146110cd575f80fd5b5f60208284031215611731575f80fd5b5035919050565b6001600160a01b0381168114611337575f80fd5b803561175781611738565b919050565b5f806040838503121561176d575f80fd5b82359150602083013561177f81611738565b809150509250929050565b634e487b7160e01b5f52604160045260245ffd5b60405160a0810167ffffffffffffffff811182821017156117c1576117c161178a565b60405290565b6040805190810167ffffffffffffffff811182821017156117c1576117c161178a565b604051601f8201601f1916810167ffffffffffffffff811182821017156118135761181361178a565b604052919050565b5f6060828403121561182b575f80fd5b6040516060810181811067ffffffffffffffff8211171561184e5761184e61178a565b604052823561185c81611738565b815260208381013590820152604083013561187681611738565b60408201529392505050565b5f67ffffffffffffffff82111561189b5761189b61178a565b50601f01601f191660200190565b5f60208083850312156118ba575f80fd5b823567ffffffffffffffff808211156118d1575f80fd5b9084019060a082870312156118e4575f80fd5b6118ec61179e565b82356118f781611738565b81528284013582811115611909575f80fd5b83016040818903121561191a575f80fd5b6119226117c7565b813561192d81611738565b8152818601358481111561193f575f80fd5b82019350601f84018913611951575f80fd5b8335915061196661196183611882565b6117ea565b8281528987848701011115611979575f80fd5b82878601888301375f8784830101528087830152508086840152505060408301356040820152606083013560608201526119b56080840161174c565b60808201529695505050505050565b5f80604083850312156119d5575f80fd5b823591506020830135801515811461177f575f80fd5b5f602082840312156119fb575f80fd5b81356110cd81611738565b5f60c08284031215611a16575f80fd5b60405160c0810181811067ffffffffffffffff82111715611a3957611a3961178a565b6040528235611a4781611738565b81526020830135611a5781611738565b60208201526040830135611a6a81611738565b60408201526060830135611a7d81611738565b60608201526080830135611a9081611738565b608082015260a0830135611aa381611738565b60a08201529392505050565b5f6020808385031215611ac0575f80fd5b825167ffffffffffffffff80821115611ad7575f80fd5b818501915085601f830112611aea575f80fd5b815181811115611afc57611afc61178a565b8060051b9150611b0d8483016117ea565b8181529183018401918481019088841115611b26575f80fd5b938501935b83851015611b4457845182529385019390850190611b2b565b98975050505050505050565b634e487b7160e01b5f52603260045260245ffd5b5f60018201611b8157634e487b7160e01b5f52601160045260245ffd5b5060010190565b5f60208284031215611b98575f80fd5b81516110cd81611738565b634e487b7160e01b5f52602160045260245ffd5b5f60208284031215611bc7575f80fd5b8151600581106110cd575f80fd5b6020810160058310611bf557634e487b7160e01b5f52602160045260245ffd5b91905290565b5f60208284031215611c0b575f80fd5b8151600481106110cd575f80fd5b5f5b83811015611c33578181015183820152602001611c1b565b50505f910152565b5f6020808385031215611c4c575f80fd5b825167ffffffffffffffff80821115611c63575f80fd5b9084019060408287031215611c76575f80fd5b611c7e6117c7565b8251611c8981611738565b81528284015182811115611c9b575f80fd5b80840193505086601f840112611caf575f80fd5b82519150611cbf61196183611882565b8281528785848601011115611cd2575f80fd5b611ce183868301878701611c19565b938101939093525090949350505050565b5f60208284031215611d02575f80fd5b5051919050565b602081525f60018060a01b03808451166020840152602084015160a060408501528181511660c08501526020810151915050604060e0840152805180610100850152610120611d5e8282870160208601611c19565b604086015160608601526060860151608086015260808601519250611d8e60a08601846001600160a01b03169052565b80601f19601f840116860101935050505092915050565b5f8060408385031215611db6575f80fd5b8251611dc181611738565b602093909301519294929350505056fea26469706673582212203a1df55e722f9f26f915b06b6edf00c9112defe6ab254a0a6cdf0899390d800a64736f6c63430008170033";
        public CybercomDAODeploymentBase() : base(BYTECODE) { }
        public CybercomDAODeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("uint64", "_subscriptionId", 1)]
        public virtual ulong SubscriptionId { get; set; }
    }

    public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

    [Function("DEFAULT_ADMIN_ROLE", "bytes32")]
    public class DefaultAdminRoleFunctionBase : FunctionMessage
    {

    }

    public partial class AcceptMemberExtFunction : AcceptMemberExtFunctionBase { }

    [Function("acceptMemberExt")]
    public class AcceptMemberExtFunctionBase : FunctionMessage
    {
        [Parameter("address", "proposalAddress", 1)]
        public virtual string ProposalAddress { get; set; }
    }

    public partial class CloseInitializationFunction : CloseInitializationFunctionBase { }

    [Function("closeInitialization")]
    public class CloseInitializationFunctionBase : FunctionMessage
    {

    }

    public partial class CompleteVotingFunction : CompleteVotingFunctionBase { }

    [Function("completeVoting")]
    public class CompleteVotingFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class ContractsFunction : ContractsFunctionBase { }

    [Function("contracts", typeof(ContractsOutputDTO))]
    public class ContractsFunctionBase : FunctionMessage
    {

    }

    public partial class GetContractAddressesFunction : GetContractAddressesFunctionBase { }

    [Function("getContractAddresses", typeof(GetContractAddressesOutputDTO))]
    public class GetContractAddressesFunctionBase : FunctionMessage
    {

    }

    public partial class GetRoleAdminFunction : GetRoleAdminFunctionBase { }

    [Function("getRoleAdmin", "bytes32")]
    public class GetRoleAdminFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
    }

    public partial class GrantRoleFunction : GrantRoleFunctionBase { }

    [Function("grantRole")]
    public class GrantRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class HasRoleFunction : HasRoleFunctionBase { }

    [Function("hasRole", "bool")]
    public class HasRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class InitializeFunction : InitializeFunctionBase { }

    [Function("initialize")]
    public class InitializeFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "_contracts", 1)]
        public virtual ContractAddresses Contracts { get; set; }
    }

    public partial class PerformVoteFunction : PerformVoteFunctionBase { }

    [Function("performVote")]
    public class PerformVoteFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("bool", "voteCast", 2)]
        public virtual bool VoteCast { get; set; }
    }

    public partial class PrepareTallyFunction : PrepareTallyFunctionBase { }

    [Function("prepareTally")]
    public class PrepareTallyFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class RenounceRoleFunction : RenounceRoleFunctionBase { }

    [Function("renounceRole")]
    public class RenounceRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "callerConfirmation", 2)]
        public virtual string CallerConfirmation { get; set; }
    }

    public partial class RevokeRoleFunction : RevokeRoleFunctionBase { }

    [Function("revokeRole")]
    public class RevokeRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class StartVotingFunction : StartVotingFunctionBase { }

    [Function("startVoting")]
    public class StartVotingFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class SubmitMembershipProposalFunction : SubmitMembershipProposalFunctionBase { }

    [Function("submitMembershipProposal", "address")]
    public class SubmitMembershipProposalFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "request", 1)]
        public virtual MembershipProposalRequest Request { get; set; }
    }

    public partial class SubmitMembershipRemovalProposalFunction : SubmitMembershipRemovalProposalFunctionBase { }

    [Function("submitMembershipRemovalProposal", "address")]
    public class SubmitMembershipRemovalProposalFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "request", 1)]
        public virtual MembershipRemovalRequest Request { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
    }

    public partial class MemberAcceptedEventDTO : MemberAcceptedEventDTOBase { }

    [Event("MemberAccepted")]
    public class MemberAcceptedEventDTOBase : IEventDTO
    {
        [Parameter("address", "memberId", 1, true )]
        public virtual string MemberId { get; set; }
    }

    public partial class MemberKeptEventDTO : MemberKeptEventDTOBase { }

    [Event("MemberKept")]
    public class MemberKeptEventDTOBase : IEventDTO
    {
        [Parameter("address", "memberId", 1, true )]
        public virtual string MemberId { get; set; }
    }

    public partial class MemberRejectedEventDTO : MemberRejectedEventDTOBase { }

    [Event("MemberRejected")]
    public class MemberRejectedEventDTOBase : IEventDTO
    {
        [Parameter("address", "memberId", 1, true )]
        public virtual string MemberId { get; set; }
    }

    public partial class MemberRemovedEventDTO : MemberRemovedEventDTOBase { }

    [Event("MemberRemoved")]
    public class MemberRemovedEventDTOBase : IEventDTO
    {
        [Parameter("address", "memberId", 1, true )]
        public virtual string MemberId { get; set; }
    }

    public partial class RoleAdminChangedEventDTO : RoleAdminChangedEventDTOBase { }

    [Event("RoleAdminChanged")]
    public class RoleAdminChangedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("bytes32", "previousAdminRole", 2, true )]
        public virtual byte[] PreviousAdminRole { get; set; }
        [Parameter("bytes32", "newAdminRole", 3, true )]
        public virtual byte[] NewAdminRole { get; set; }
    }

    public partial class RoleGrantedEventDTO : RoleGrantedEventDTOBase { }

    [Event("RoleGranted")]
    public class RoleGrantedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true )]
        public virtual string Account { get; set; }
        [Parameter("address", "sender", 3, true )]
        public virtual string Sender { get; set; }
    }

    public partial class RoleRevokedEventDTO : RoleRevokedEventDTOBase { }

    [Event("RoleRevoked")]
    public class RoleRevokedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true )]
        public virtual string Account { get; set; }
        [Parameter("address", "sender", 3, true )]
        public virtual string Sender { get; set; }
    }

    public partial class TallyPreparedEventDTO : TallyPreparedEventDTOBase { }

    [Event("TallyPrepared")]
    public class TallyPreparedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class VoteCastEventDTO : VoteCastEventDTOBase { }

    [Event("VoteCast")]
    public class VoteCastEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("address", "voter", 2, true )]
        public virtual string Voter { get; set; }
        [Parameter("bool", "vote", 3, false )]
        public virtual bool Vote { get; set; }
    }

    public partial class VoteStartedEventDTO : VoteStartedEventDTOBase { }

    [Event("VoteStarted")]
    public class VoteStartedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("address", "startedBy", 2, false )]
        public virtual string StartedBy { get; set; }
    }

    public partial class VotingCompletedEventDTO : VotingCompletedEventDTOBase { }

    [Event("VotingCompleted")]
    public class VotingCompletedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("uint8", "status", 2, false )]
        public virtual byte Status { get; set; }
    }

    public partial class AccessControlBadConfirmationError : AccessControlBadConfirmationErrorBase { }
    [Error("AccessControlBadConfirmation")]
    public class AccessControlBadConfirmationErrorBase : IErrorDTO
    {
    }

    public partial class AccessControlUnauthorizedAccountError : AccessControlUnauthorizedAccountErrorBase { }

    [Error("AccessControlUnauthorizedAccount")]
    public class AccessControlUnauthorizedAccountErrorBase : IErrorDTO
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
        [Parameter("bytes32", "neededRole", 2)]
        public virtual byte[] NeededRole { get; set; }
    }

    public partial class AlreadyInitializedError : AlreadyInitializedErrorBase { }
    [Error("AlreadyInitialized")]
    public class AlreadyInitializedErrorBase : IErrorDTO
    {
    }

    public partial class AuthorizationErrorError : AuthorizationErrorErrorBase { }
    [Error("AuthorizationError")]
    public class AuthorizationErrorErrorBase : IErrorDTO
    {
    }

    public partial class InvalidProposalError : InvalidProposalErrorBase { }
    [Error("InvalidProposal")]
    public class InvalidProposalErrorBase : IErrorDTO
    {
    }

    public partial class NotOwnerError : NotOwnerErrorBase { }
    [Error("NotOwner")]
    public class NotOwnerErrorBase : IErrorDTO
    {
    }

    public partial class ProposalNotReadyForTallyError : ProposalNotReadyForTallyErrorBase { }
    [Error("ProposalNotReadyForTally")]
    public class ProposalNotReadyForTallyErrorBase : IErrorDTO
    {
    }

    public partial class ReentrancyGuardReentrantCallError : ReentrancyGuardReentrantCallErrorBase { }
    [Error("ReentrancyGuardReentrantCall")]
    public class ReentrancyGuardReentrantCallErrorBase : IErrorDTO
    {
    }

    public partial class DefaultAdminRoleOutputDTO : DefaultAdminRoleOutputDTOBase { }

    [FunctionOutput]
    public class DefaultAdminRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }







    public partial class ContractsOutputDTO : ContractsOutputDTOBase { }

    [FunctionOutput]
    public class ContractsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "daoAddress", 1)]
        public virtual string DaoAddress { get; set; }
        [Parameter("address", "votingAddress", 2)]
        public virtual string VotingAddress { get; set; }
        [Parameter("address", "councilManagementAddress", 3)]
        public virtual string CouncilManagementAddress { get; set; }
        [Parameter("address", "proposalStorageAddress", 4)]
        public virtual string ProposalStorageAddress { get; set; }
        [Parameter("address", "membershipRemovalAddress", 5)]
        public virtual string MembershipRemovalAddress { get; set; }
        [Parameter("address", "membershipManagerAddress", 6)]
        public virtual string MembershipManagerAddress { get; set; }
    }

    public partial class GetContractAddressesOutputDTO : GetContractAddressesOutputDTOBase { }

    [FunctionOutput]
    public class GetContractAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "", 1)]
        public virtual ContractAddresses ReturnValue1 { get; set; }
    }

    public partial class GetRoleAdminOutputDTO : GetRoleAdminOutputDTOBase { }

    [FunctionOutput]
    public class GetRoleAdminOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }



    public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

    [FunctionOutput]
    public class HasRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

















    public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

    [FunctionOutput]
    public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
