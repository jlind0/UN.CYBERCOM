// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;
import "./MembershipManagement.sol";
import "@openzeppelin/contracts/utils/cryptography/ECDSA.sol";
contract Document{
    using ECDSA for bytes32;
    string public title;
    string public url;
    bytes32 public dochash;
    bytes public signature;
    address public signer;
    address public owningContract;
    uint public timestamp;
    error AuthorizationError();
    error InvalidSignature();
    modifier isFromOwningContract() {
        if(msg.sender != owningContract)revert AuthorizationError();
        _;
    }
    constructor(address _owningContract, address _signer, bytes memory _signature, bytes32 _dochash, string memory _url, string memory _title){
        title = _title;
        url = _url;
        dochash = _dochash;
        signature = _signature;
        signer = _signer;
        owningContract = _owningContract;
        timestamp = block.timestamp;
        bytes32 ethSignedHash = keccak256(abi.encodePacked("\x19Ethereum Signed Message:\n32", dochash));
         (address recoveredSigner, ECDSA.RecoverError err, bytes32 something) = ethSignedHash.tryRecover(_signature);

        // Check if there was an error during recovery or if the recovered address does not match the signer
        if (err != ECDSA.RecoverError.NoError || recoveredSigner != signer) {
            revert InvalidSignature();
        }
    }
}
abstract contract DocumentsHolder{
    mapping(bytes => address) internal urlToDocument;
    string[] internal urls;
    function addDocument(address signer, string memory title, string memory url, bytes32 docHash, bytes memory signature) public virtual{
        bytes memory urlBytes = bytes(url);
        if(urlToDocument[urlBytes] != address(0))
            revert();
        Document d = new Document(address(this), signer, signature, docHash, url, title);
        urls.push(url);
        urlToDocument[urlBytes] = address(d);
    }
    function getDocuments() public virtual view returns(MembershipManagement.Doc[] memory){
        MembershipManagement.Doc[] memory docs = new MembershipManagement.Doc[](urls.length);
        uint i = 0;
        while(i < docs.length){
            Document d = Document(urlToDocument[bytes(urls[i])]);
            docs[i] = MembershipManagement.Doc(d.title(), d.url(), d.dochash(), d.signature(), d.signer(), address(d));
            i++;
        }
        return docs;
    } 
}