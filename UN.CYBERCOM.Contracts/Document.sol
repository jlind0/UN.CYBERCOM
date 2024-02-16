// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

// Importing external contracts and libraries for functionality
import "./MembershipManagement.sol";
import "@openzeppelin/contracts/utils/cryptography/ECDSA.sol";

/**
 * @title Document Contract
 * @dev Manages a single document, including its digital signature verification
 * This contract stores document details and verifies the signer using ECDSA.
 */

contract Document {

    using ECDSA for bytes32; // Using OpenZeppelin's ECDSA library for cryptographic functions

    // State variables for storing document details
    string  public title; // Title of the document
    string  public url; // URL of the document
    bytes32 public dochash; // Hash of the document
    bytes   public signature; // Digital signature of the document
    address public signer; // Address of the signer of the document
    address public owningContract; // Address of the contract that owns this document
    uint    public timestamp; // Timestamp when the document was created

    /**
     * @dev Modifier to ensure function calls come from the owning contract
     */

    modifier isFromOwningContract() {
        require(msg.sender == owningContract, "Caller is not the owning contract");
        _;
    }

    /**
     * @dev Constructs the Document contract.
     * @param _owningContract Address of the contract that owns this document.
     * @param _signer Address of the signer of the document.
     * @param _signature Digital signature of the document.
     * @param _dochash Hash of the document.
     * @param _url URL of the document.
     * @param _title Title of the document.
     */

    constructor(
        address _owningContract,
        address _signer,
        bytes   memory _signature,
        bytes32 _dochash,
        string  memory _url,
        string  memory _title
    ) {
        title          = _title;
        url            = _url;
        dochash        = _dochash;
        signature      = _signature;
        signer         = _signer;
        owningContract = _owningContract;
        timestamp      = block.timestamp;

        // Verifying the digital signature
        bytes32 ethSignedHash = keccak256(abi.encodePacked("\x19Ethereum Signed Message:\n32", dochash));
        (address recoveredSigner, ECDSA.RecoverError err, bytes32 something) = ethSignedHash.tryRecover(_signature);
        // Reverting transaction if the signature is invalid
        if (err != ECDSA.RecoverError.NoError || recoveredSigner != signer) {
            revert("Invalid signature");
        }
    }
}

/**
 * @title Documents Holder Contract
 * @dev Abstract contract for managing a collection of Document contracts
 * This contract allows adding and retrieving documents.
 */

abstract contract DocumentsHolder {
    // Mapping to store the address of Document contracts by URL
    mapping(string => address) internal urlToDocument;
    string[] internal urls; // Array to store all document URLs

    // Event to log the addition of a new document
    event DocumentAdded(string url, address documentAddress);

    /**
     * @dev Adds a new document to the collection.
     * @param signer Address of the signer of the document.
     * @param title Title of the document.
     * @param url URL of the document.
     * @param docHash Hash of the document.
     * @param signature Digital signature of the document.
     */

    function addDocument(

        address signer,
        string  memory title,
        string  memory url,
        bytes32 docHash,
        bytes   memory signature

    ) public virtual {
        // Ensuring the URL is unique and not already used
        require(urlToDocument[url] == address(0), "Document with this URL already exists");

        // Creating a new Document contract instance
        Document d = new Document(address(this), signer, signature, docHash, url, title);
        urls.push(url);
        urlToDocument[url] = address(d);

        // Emitting event to log the addition of the document
        emit DocumentAdded(url, address(d));
    }

    /**
     * @dev Retrieves all documents in the collection.
     * @return An array of MembershipManagement.Doc structs representing each document.
     */
    function getDocuments() external virtual returns(MembershipManagement.Doc[] memory) {
        MembershipManagement.Doc[] memory docs = new MembershipManagement.Doc[](urls.length);
        for (uint i = 0; i < docs.length; i++) {
            Document d = Document(urlToDocument[urls[i]]);
            docs[i] = MembershipManagement.Doc(d.title(), d.url(), d.dochash(), d.signature(), d.signer(), address(d));
        }
        return docs;
    } 
}
