import { connection } from "./hubConnection.js";

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
        if (!isReceiverSelected()) {
            addSelectUserError();
            return;
        }

        var message = getFilledMessage();
        sendMessage(message);
        clearMessageBody();

        saveInDb(message);

        event.preventDefault();
    });

function isReceiverSelected() {
    clearErrors();

    let usersValues = getRegisteredUserNames();
    let receiver = getValueById("receiver");
    return usersValues.includes(receiver);
}

function getRegisteredUserNames() {
    let users = getElementById("receiver").children;
    let usersValues = [];
    for (let user of users) {
        if (user.value.length > 0) {
            usersValues.push(user.value);
        }
    }
    return usersValues;
}

function addSelectUserError() {
    let errorsElement = getElementById("validationErrors");
    let p = createElement("p");
    p.innerHTML = "Select the user!";
    errorsElement.appendChild(p);
}

function clearErrors() {
    let errorsElement = getElementById("validationErrors");
    errorsElement.innerHTML = "";
}

function sendMessage(message) {
    connection.invoke("Send", message)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

function getFilledMessage() {
    let message = {};
    message.sender = getValueById("user");
    message.receiver = getValueById("receiver");
    message.theme = getValueById("theme");
    message.body = getValueById("message");
    message.dateSent = new Date().toISOString();
    return message;
}

function saveInDb(newMessage) {
    $.ajax({
        type: "POST",
        url: "Message/Save",
        data: { message: newMessage }
    });
    return;
}

function clearMessageBody() {

    getElementById("message").value = "";
}

function getValueById(id) {

    return getElementById(id).value;
}

function getElementById(id) {

    return document.getElementById(id);
}

function createElement(name) {

    return document.createElement(name);
}

function fillRow(row, message) {
    fillSender(row, getSender(message));
    fillReceiver(row, getReceiver(message));
    fillTheme(row, message.theme);
    fillMessageBody(row, message.body);
    fillDateSent(row, message.dateSent);
}

function getSender(message) {
    return message.sender === getCurrentUser() ? "Me" : message.sender;
}

function getReceiver(message) {
    return message.receiver === getCurrentUser() ? "Me" : message.receiver;
}

function getCurrentUser() {
    return getValueById("user");
}

function fillSender(row, text) {

    let sender = row.insertCell(0);
    sender.innerHTML = text;
}

function fillReceiver(row, text) {

    let receiver = row.insertCell(1);
    receiver.innerHTML = text;
}

function fillTheme(row, text) {

    let theme = row.insertCell(2);
    let content = text.length === 0 ? "(no theme)" : text;
    theme.innerHTML = content;
}

function fillMessageBody(row, text) {

    let body = row.insertCell(3);
    let pre = createElement("pre");
    pre.innerHTML = text;
    body.appendChild(pre);
}

function fillDateSent(row, text) {

    let date = row.insertCell(4);
    date.innerHTML = new Date(text).toLocaleString();
}