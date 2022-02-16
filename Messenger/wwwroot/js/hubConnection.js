export let connection = new signalR.HubConnectionBuilder()
    .withUrl("/messenger")
    .withAutomaticReconnect()
    .build();