window.web3 = new Web3(ethereum);
window.web3 = new Web3(new Web3.providers.HttpProvider('http://localhost:8545'));
var dotNetObject = undefined;
var connectedAccount = undefined;
const connectWallet = async () => {
    try {
        if (!ethereum) return alert('Please install Metamask')
        const accounts = await ethereum.request({ method: 'eth_requestAccounts' })
        connectedAccount = accounts[0];
        if (connectedAccount !== undefined)
            setAccount(connectedAccount);
    } catch (error) {
        console.log(JSON.stringify(error))
    }
}
const isWalletConnected = async () => {
    try {
        if (!ethereum) return alert('Please install Metamask');
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