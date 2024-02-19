using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using UN.CYBERCOM.Contracts.CouncilManager.ContractDefinition;

namespace UN.CYBERCOM.Contracts.CouncilManager
{
    public partial class CouncilManagerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CouncilManagerDeployment councilManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CouncilManagerDeployment>().SendRequestAndWaitForReceiptAsync(councilManagerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CouncilManagerDeployment councilManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CouncilManagerDeployment>().SendRequestAsync(councilManagerDeployment);
        }

        public static async Task<CouncilManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CouncilManagerDeployment councilManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, councilManagerDeployment, cancellationTokenSource);
            return new CouncilManagerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CouncilManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public CouncilManagerService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> BrokerRoleQueryAsync(BrokerRoleFunction brokerRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BrokerRoleFunction, byte[]>(brokerRoleFunction, blockParameter);
        }

        
        public Task<byte[]> BrokerRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BrokerRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> CentralRoleQueryAsync(CentralRoleFunction centralRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CentralRoleFunction, byte[]>(centralRoleFunction, blockParameter);
        }

        
        public Task<byte[]> CentralRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CentralRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> EmergingRoleQueryAsync(EmergingRoleFunction emergingRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EmergingRoleFunction, byte[]>(emergingRoleFunction, blockParameter);
        }

        
        public Task<byte[]> EmergingRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EmergingRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> GeneralRoleQueryAsync(GeneralRoleFunction generalRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GeneralRoleFunction, byte[]>(generalRoleFunction, blockParameter);
        }

        
        public Task<byte[]> GeneralRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GeneralRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> IndustryRoleQueryAsync(IndustryRoleFunction industryRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IndustryRoleFunction, byte[]>(industryRoleFunction, blockParameter);
        }

        
        public Task<byte[]> IndustryRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IndustryRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> LesserRoleQueryAsync(LesserRoleFunction lesserRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LesserRoleFunction, byte[]>(lesserRoleFunction, blockParameter);
        }

        
        public Task<byte[]> LesserRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LesserRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> PowerRoleQueryAsync(PowerRoleFunction powerRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PowerRoleFunction, byte[]>(powerRoleFunction, blockParameter);
        }

        
        public Task<byte[]> PowerRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PowerRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<string> AcceptNewMemberRequestAsync(AcceptNewMemberFunction acceptNewMemberFunction)
        {
             return ContractHandler.SendRequestAsync(acceptNewMemberFunction);
        }

        public Task<TransactionReceipt> AcceptNewMemberRequestAndWaitForReceiptAsync(AcceptNewMemberFunction acceptNewMemberFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptNewMemberFunction, cancellationToken);
        }

        public Task<string> AcceptNewMemberRequestAsync(string proposalAddress)
        {
            var acceptNewMemberFunction = new AcceptNewMemberFunction();
                acceptNewMemberFunction.ProposalAddress = proposalAddress;
            
             return ContractHandler.SendRequestAsync(acceptNewMemberFunction);
        }

        public Task<TransactionReceipt> AcceptNewMemberRequestAndWaitForReceiptAsync(string proposalAddress, CancellationTokenSource cancellationToken = null)
        {
            var acceptNewMemberFunction = new AcceptNewMemberFunction();
                acceptNewMemberFunction.ProposalAddress = proposalAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptNewMemberFunction, cancellationToken);
        }

        public Task<byte[]> CouncilGroupsQueryAsync(CouncilGroupsFunction councilGroupsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CouncilGroupsFunction, byte[]>(councilGroupsFunction, blockParameter);
        }

        
        public Task<byte[]> CouncilGroupsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var councilGroupsFunction = new CouncilGroupsFunction();
                councilGroupsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<CouncilGroupsFunction, byte[]>(councilGroupsFunction, blockParameter);
        }

        public Task<byte[]> CouncilRolesQueryAsync(CouncilRolesFunction councilRolesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CouncilRolesFunction, byte[]>(councilRolesFunction, blockParameter);
        }

        
        public Task<byte[]> CouncilRolesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var councilRolesFunction = new CouncilRolesFunction();
                councilRolesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<CouncilRolesFunction, byte[]>(councilRolesFunction, blockParameter);
        }

        public Task<CouncilsOutputDTO> CouncilsQueryAsync(CouncilsFunction councilsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CouncilsFunction, CouncilsOutputDTO>(councilsFunction, blockParameter);
        }

        public Task<CouncilsOutputDTO> CouncilsQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var councilsFunction = new CouncilsFunction();
                councilsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CouncilsFunction, CouncilsOutputDTO>(councilsFunction, blockParameter);
        }

        public Task<string> DaoAddressQueryAsync(DaoAddressFunction daoAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DaoAddressFunction, string>(daoAddressFunction, blockParameter);
        }

        
        public Task<string> DaoAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DaoAddressFunction, string>(null, blockParameter);
        }

        public Task<bool> DoesCouncilGroupExistQueryAsync(DoesCouncilGroupExistFunction doesCouncilGroupExistFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DoesCouncilGroupExistFunction, bool>(doesCouncilGroupExistFunction, blockParameter);
        }

        
        public Task<bool> DoesCouncilGroupExistQueryAsync(BigInteger groupId, BlockParameter blockParameter = null)
        {
            var doesCouncilGroupExistFunction = new DoesCouncilGroupExistFunction();
                doesCouncilGroupExistFunction.GroupId = groupId;
            
            return ContractHandler.QueryAsync<DoesCouncilGroupExistFunction, bool>(doesCouncilGroupExistFunction, blockParameter);
        }

        public Task<bool> DoesNationExistQueryAsync(DoesNationExistFunction doesNationExistFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DoesNationExistFunction, bool>(doesNationExistFunction, blockParameter);
        }

        
        public Task<bool> DoesNationExistQueryAsync(string memberId, BlockParameter blockParameter = null)
        {
            var doesNationExistFunction = new DoesNationExistFunction();
                doesNationExistFunction.MemberId = memberId;
            
            return ContractHandler.QueryAsync<DoesNationExistFunction, bool>(doesNationExistFunction, blockParameter);
        }

        public Task<GetCouncilOutputDTO> GetCouncilQueryAsync(GetCouncilFunction getCouncilFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilFunction, GetCouncilOutputDTO>(getCouncilFunction, blockParameter);
        }

        public Task<GetCouncilOutputDTO> GetCouncilQueryAsync(byte[] role, BlockParameter blockParameter = null)
        {
            var getCouncilFunction = new GetCouncilFunction();
                getCouncilFunction.Role = role;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilFunction, GetCouncilOutputDTO>(getCouncilFunction, blockParameter);
        }

        public Task<GetCouncilForGroupIdOutputDTO> GetCouncilForGroupIdQueryAsync(GetCouncilForGroupIdFunction getCouncilForGroupIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilForGroupIdFunction, GetCouncilForGroupIdOutputDTO>(getCouncilForGroupIdFunction, blockParameter);
        }

        public Task<GetCouncilForGroupIdOutputDTO> GetCouncilForGroupIdQueryAsync(BigInteger groupId, BlockParameter blockParameter = null)
        {
            var getCouncilForGroupIdFunction = new GetCouncilForGroupIdFunction();
                getCouncilForGroupIdFunction.GroupId = groupId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilForGroupIdFunction, GetCouncilForGroupIdOutputDTO>(getCouncilForGroupIdFunction, blockParameter);
        }

        public Task<GetCouncilForNationOutputDTO> GetCouncilForNationQueryAsync(GetCouncilForNationFunction getCouncilForNationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilForNationFunction, GetCouncilForNationOutputDTO>(getCouncilForNationFunction, blockParameter);
        }

        public Task<GetCouncilForNationOutputDTO> GetCouncilForNationQueryAsync(string nationId, BlockParameter blockParameter = null)
        {
            var getCouncilForNationFunction = new GetCouncilForNationFunction();
                getCouncilForNationFunction.NationId = nationId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilForNationFunction, GetCouncilForNationOutputDTO>(getCouncilForNationFunction, blockParameter);
        }

        public Task<byte[]> GetCouncilRoleForGroupQueryAsync(GetCouncilRoleForGroupFunction getCouncilRoleForGroupFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCouncilRoleForGroupFunction, byte[]>(getCouncilRoleForGroupFunction, blockParameter);
        }

        
        public Task<byte[]> GetCouncilRoleForGroupQueryAsync(BigInteger groupId, BlockParameter blockParameter = null)
        {
            var getCouncilRoleForGroupFunction = new GetCouncilRoleForGroupFunction();
                getCouncilRoleForGroupFunction.GroupId = groupId;
            
            return ContractHandler.QueryAsync<GetCouncilRoleForGroupFunction, byte[]>(getCouncilRoleForGroupFunction, blockParameter);
        }

        public Task<GetCouncilVotesOutputDTO> GetCouncilVotesQueryAsync(GetCouncilVotesFunction getCouncilVotesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilVotesFunction, GetCouncilVotesOutputDTO>(getCouncilVotesFunction, blockParameter);
        }

        public Task<GetCouncilVotesOutputDTO> GetCouncilVotesQueryAsync(List<Vote> vs, BlockParameter blockParameter = null)
        {
            var getCouncilVotesFunction = new GetCouncilVotesFunction();
                getCouncilVotesFunction.Vs = vs;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilVotesFunction, GetCouncilVotesOutputDTO>(getCouncilVotesFunction, blockParameter);
        }

        public Task<GetCouncilsOutputDTO> GetCouncilsQueryAsync(GetCouncilsFunction getCouncilsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilsFunction, GetCouncilsOutputDTO>(getCouncilsFunction, blockParameter);
        }

        public Task<GetCouncilsOutputDTO> GetCouncilsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilsFunction, GetCouncilsOutputDTO>(null, blockParameter);
        }

        public Task<string> NationAddressesQueryAsync(NationAddressesFunction nationAddressesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NationAddressesFunction, string>(nationAddressesFunction, blockParameter);
        }

        
        public Task<string> NationAddressesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var nationAddressesFunction = new NationAddressesFunction();
                nationAddressesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<NationAddressesFunction, string>(nationAddressesFunction, blockParameter);
        }

        public Task<NationsOutputDTO> NationsQueryAsync(NationsFunction nationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<NationsFunction, NationsOutputDTO>(nationsFunction, blockParameter);
        }

        public Task<NationsOutputDTO> NationsQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var nationsFunction = new NationsFunction();
                nationsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<NationsFunction, NationsOutputDTO>(nationsFunction, blockParameter);
        }

        public Task<byte[]> NationsCouncilQueryAsync(NationsCouncilFunction nationsCouncilFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NationsCouncilFunction, byte[]>(nationsCouncilFunction, blockParameter);
        }

        
        public Task<byte[]> NationsCouncilQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var nationsCouncilFunction = new NationsCouncilFunction();
                nationsCouncilFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<NationsCouncilFunction, byte[]>(nationsCouncilFunction, blockParameter);
        }

        public Task<BigInteger> TotalCouncilGroupsQueryAsync(TotalCouncilGroupsFunction totalCouncilGroupsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalCouncilGroupsFunction, BigInteger>(totalCouncilGroupsFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalCouncilGroupsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalCouncilGroupsFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> TotalNationsQueryAsync(TotalNationsFunction totalNationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalNationsFunction, BigInteger>(totalNationsFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalNationsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalNationsFunction, BigInteger>(null, blockParameter);
        }
    }
}
