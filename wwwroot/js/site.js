var connection = new signalR.HubConnectionBuilder()
    .withUrl("/sampleHub")
    .withAutomaticReconnect()
    .build();

function callRestApi() {
    var username = document.getElementById('username').value;
    if(!username)
    {
        document.getElementById("status").innerText = "Please provide a username";
        return;
    }
    document.getElementById("status").innerText = "Starting long-running process for " + username;
    
    connection.send('StartMonitoring', username);
    
    axios
        .get('/sample/' + username)
        .then(function (response) {
            console.log(response);
        })
        .catch(function (error) {
            console.log(error);
        });
}

connection.on('serverSideProcessComplete', (username) => 
{
    document.getElementById("status").innerText = "Process Complete for " + username;
});

connection.onreconnecting((error) => {
    disableUi(true);
    document.getElementById("status").innerText = 
        `Connection lost due to error "${error}". Reconnecting.`;
});

connection.onreconnected((connectionId) => {
    disableUi(false);
    document.getElementById("status").innerText = 
        `Connection reestablished. Connected.`;
});

function disableUi(isEnabled) {
    document.getElementById("sendmessage").disabled = isEnabled;
}

connection.start();