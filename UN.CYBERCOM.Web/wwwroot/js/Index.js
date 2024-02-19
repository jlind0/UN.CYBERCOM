
var dotNetObject = undefined;
var connectedAccount = undefined;
const connectWallet = async () => {
    try {
        if (!ethereum) return alert('Please install Coinbase Wallet')
        const accounts = await ethereum.request({ method: 'eth_requestAccounts' })
        connectedAccount = accounts[0];
        if (connectedAccount !== undefined)
            setAccount(connectedAccount);
    } catch (error) {
        console.log(JSON.stringify(error))
    }
}
let web3;
const isWalletConnected = async () => {
    try {
        if (!ethereum) return alert('Please install Coinbase Wallet');
        web3 = new Web3(window.ethereum);
        const accounts = await ethereum.request({ method: 'eth_accounts' });

        window.ethereum.on('chainChanged', (chainId) => {
            window.location.reload();
        });

        window.ethereum.on('accountsChanged', async () => {
            var acctNum = accounts[0].toLowerCase();
            connectedAccount = acctNum;
            setAccount(acctNum);
            await isWalletConnected();
        });

        if (accounts.length) {
            var acctNum = accounts[0].toLowerCase();
            connectedAccount = acctNum;
            setAccount(acctNum);
        } else {
            alert('Please connect wallet.');
            console.log('No accounts found.');
        }
    } catch (error) {
        reportError(error);
    }
}
window.signTransaction = async function(fromAddress,contractAddress, tranData, rtnFunction){
    const nonce = await window.ethereum.request({
        method: 'eth_getTransactionCount',
        params: [fromAddress, 'latest'], // 'latest' for the most recent count
    });

    // Define the transaction object, including the updated nonce
    const tx = {
        from: fromAddress,
        to: contractAddress,
        data: tranData,
        value: '0x0', // It's better to specify the value in hexadecimal format
        gas: '0xe4e1c0', // Also in hexadecimal, equivalent to 15,000,000 gas
        nonce: nonce, // Use the incremented nonce
    };
    const currentGasPrice = await window.ethereum.request({ method: 'eth_gasPrice', params: [] });
    // Convert currentGasPrice from hex to integer, multiply by 1.1, then round it to get an integer
    const increasedGasPrice = "0x" + Math.floor(parseInt(currentGasPrice, 16) * 1.1).toString(16);
    tx.gasPrice = increasedGasPrice; // Set the increased gas price
    // Sign the transaction
    const signedTx = await window.ethereum.request({
        method: 'eth_signTransaction',
        params: [tx],
    });
    dotNetObject.invokeMethodAsync(rtnFunction, signedTx);
}
window.signHash = async function (fromAddress, documentHash, rtnFunction) {
    const signature = await window.ethereum.request({
        method: 'personal_sign',
        params: [documentHash, fromAddress]
    });
    dotNetObject.invokeMethodAsync(rtnFunction, signature);
}
window.setAccount = function (accountNum) {
    if (dotNetObject === undefined)
        setTimeout(() => setAccount(accountNum), 100);
    else
        dotNetObject.invokeMethodAsync("SetAccount", accountNum);
}
window.SetDotNetObject = async function(dno) {
    dotNetObject = dno;
    await connectWallet();
    await isWalletConnected();
}