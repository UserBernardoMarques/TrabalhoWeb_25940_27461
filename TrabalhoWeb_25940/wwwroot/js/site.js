/* global signalR */

// 1. Criar a ligação ao Hub do SignalR (O GPS para o servidor)
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/workshopHub")
    .withAutomaticReconnect()
    .build();

// 2. Ficar à escuta do evento (O nome tem de ser idêntico ao invocado no C#)
connection.on("ReceberNotificacao", function (mensagem) {
    // Quando a mensagem chega, disparamos o alerta gráfico
    alert("📢 Atualização: " + mensagem);
    console.log("SignalR Recebeu: ", mensagem);
});

// 3. Inicializar a ligação ao Hub na Azure
connection.start().then(function () {
    console.log("✅ SignalR ligado com sucesso à Azure!");
}).catch(function (err) {
    return console.error("❌ Erro a ligar o SignalR: " + err.toString());
});