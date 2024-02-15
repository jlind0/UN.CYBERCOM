// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

/**
 * @title Simple Storage
 * @dev A simple way to store and retrieve a value.
 * This contract is a basic example of a Solidity contract. 
 * It demonstrates a simple storage pattern where a value can be stored and retrieved.
 */
contract SimpleStorage {
    uint storedData; // A variable to store a number.
    address owner; // Address of the contract owner.

    // Event to emit when a value is stored.
    event DataStored(uint indexed newValue, address indexed updatedBy);

    /**
     * @dev Sets the original `owner` of the contract to the sender account.
     */
    constructor() {
        owner = msg.sender; // Set the contract deployer as the initial owner.
    }

    /**
     * @dev Modifier to limit access to the owner.
     */
    modifier onlyOwner() {
        require(msg.sender == owner, "Not the owner: Caller is not the owner.");
        _;
    }

    /**
     * @dev Stores a new value in the contract.
     * @param x The new value to store.
     * Emits a `DataStored` event.
     * Can only be called by the contract owner.
     */
    function set(uint x) public onlyOwner {
        storedData = x;
        emit DataStored(x, msg.sender); // Triggering event after value change.
    }

    /**
     * @dev Returns the stored value.
     * @return The stored value.
     */
    function get() public view returns (uint) {
        return storedData;
    }

    /**
     * @dev Transfers contract ownership to a new address.
     * @param newOwner The address of the new owner.
     * Requires the provided `newOwner` address to be valid.
     * Can only be called by the current owner.
     */
    function transferOwnership(address newOwner) public onlyOwner {
        require(newOwner != address(0), "Invalid address: New owner address is the zero address.");
        owner = newOwner;
    }
}
