﻿"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messenger")
    .withAutomaticReconnect()
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    li.className = "list-group-item";
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `${user} says: ${message}`;
});

//todo: refactor repeated (example: li)
connection.on("ConfirmMessage", function (toUser, message) {
    var li = document.createElement("li");
    li.className = "list-group-item";
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `I say to ${toUser}: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton")
    .addEventListener("click", function (event) {
        var user = document.getElementById("user").value;
        var message = document.getElementById("messageInput").value;
        connection.invoke("Send", user, message)
            .catch(function (err) {
                return console.error(err.toString());
            });
        event.preventDefault();
    });


//document.getElementById("sendButton")
//    .addEventListener("click", function (event) {
//        var user = document.getElementById("userInput").value;
//        var message = document.getElementById("messageInput").value;
//        connection.invoke("Send", user, message)
//            .catch(function (err) {
//                return console.error(err.toString());
//            });
//        event.preventDefault();
//    });