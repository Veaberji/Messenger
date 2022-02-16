import { connection } from "./hubConnection.js"; //todo: dell

connection.on("Notify", function (message) {
    let text = `${message.sender} says:\n
        Theme: ${message.theme}\n
        Message: ${message.body}`;
    alertify.notify(text, "success", 10);
});
connection.start();