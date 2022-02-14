"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messenger")
    .withAutomaticReconnect()
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var tbody = getElementById("messages");
    var row = createElement("tr");

    fillRow(row, message);

    tbody.appendChild(row);
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
        message.sender = getValueById("user");
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

    return getElementById(id).value;
};

function getElementById(id) {

    return document.getElementById(id);
};

function createElement(name) {

    return document.createElement(name);
};

function fillRow(row, message) {
    fillSender(row, getSender(message));
    fillReceiver(row, getReceiver(message));
    fillTheme(row, message.theme);
    fillMessageBody(row, message.body);
};

function getSender(message) {
    return message.sender === getCurrentUser() ? "Me" : message.sender;
};

function getReceiver(message) {
    return message.receiver === getCurrentUser() ? "Me" : message.receiver;
};

function getCurrentUser() {
    return getValueById("user");
};

function fillSender(row, text) {

    var sender = row.insertCell(0);
    sender.innerHTML = text;
};

function fillReceiver(row, text) {

    var receiver = row.insertCell(1);
    receiver.innerHTML = text;
};

function fillTheme(row, text) {

    var theme = row.insertCell(2);
    theme.innerHTML = text;
};

function fillMessageBody(row, text) {

    var body = row.insertCell(3);
    body.innerHTML = text;
};
