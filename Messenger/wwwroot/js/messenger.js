"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messenger")
    .withAutomaticReconnect()
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    li.className = "list-group-item";
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `${message.sender} says: theme: ${message.theme}\n message: ${message.body}`;
});

//todo: refactor repeated (example: li)
connection.on("ConfirmMessage", function (message) {
    console.log(message);
    var li = document.createElement("li");
    li.className = "list-group-item";
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `I say to ${message.receiver}: theme: ${message.theme}\n message: ${message.body}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton")
    .addEventListener("click", function (event) {
        var messageBodyElement = getElementById("message");

        var message = {};
        message.sender = getValueById("sender");
        message.receiver = getValueById("receiver");
        message.theme = getValueById("theme");
        message.body = getValueById("message");
        connection.invoke("Send", message)
            .catch(function (err) {
                return console.error(err.toString());
            });
        messageBodyElement.value = "";
        event.preventDefault();
    });

function getValueById(id) {

    return getElementById(id).value;;
}

function getElementById(id) {

    return document.getElementById(id);;
}

