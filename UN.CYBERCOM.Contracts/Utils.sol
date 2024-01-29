// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

library Utils {
     function getNumberOfDigits(uint number) private pure returns (uint) {
        uint digits = 0;
        while (number != 0) {
            number /= 10;
            digits++;
        }
        return digits;
    }
    function getDigitBlock(uint number, uint blockIndex, uint blockLength) public pure returns (uint) {
        require(blockIndex >= 0, "Block index must be non-negative");
        require(blockLength > 0, "Block length must be positive");

        uint divisor = 1;
        uint numberOfDigits = getNumberOfDigits(number);

        // Calculating the starting position of the desired block
        uint startPosition = numberOfDigits - blockIndex * blockLength;
        require(startPosition >= blockLength, "Requested block is out of range");

        for (uint i = 0; i < startPosition - blockLength; i++) {
            divisor *= 10;
        }

        uint modulus = 10**blockLength;
        return (number / divisor) % modulus;
    }

    // Function to get N unique random indices from an array of length M
    function getRandomIndices(uint R, uint N, uint M, uint IDXstart) internal pure returns (uint[] memory, uint) 
    {
        require(M > 0, "M must be greater than 0");
        require(N <= M, "Number of indices must be less than or equal to M");

        uint[] memory indices = new uint[](N);

        for (uint i = 0; i < N; i++) {
            bool isUnique;
            do {
                indices[i] = getDigitBlock(R, IDXstart++, 3) % M;
                isUnique = true;

                // Check for duplicates
                for (uint j = 0; j < i; j++) {
                    if (indices[j] == indices[i]) {
                        isUnique = false;
                        break;
                    }
                }
            } while (!isUnique);
        }

        return (indices, IDXstart);
    }
    function bitLength(uint number) internal pure returns (uint) {
        uint count = 0;
        while (number > 0) {
            count++;
            number >>= 1;
        }
        return count;
    }

    function calculateAverage(int[] memory numbers) internal pure returns (int) {
        require(numbers.length > 0, "Array is empty");
        int sum = 0;
        for (uint i = 0; i < numbers.length; i++) {
            sum += numbers[i];
        }
        return sum / int(numbers.length);
    }

    function multiply(uint a, int b) internal pure returns (int) {
        if (b < 0) {
            return -int(a) * b;
        } else {
            return int(a) * b;
        }
    }
    
    // Add other utility functions here...
}
