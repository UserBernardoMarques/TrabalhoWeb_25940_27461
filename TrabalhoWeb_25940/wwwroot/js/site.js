/* global signalR */

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/workshopHub")
    .withAutomaticReconnect()
    .build();

// Quando o servidor avisa que há novidades...
connection.on("ReceberNotificacao", function (mensagem) {
    console.log("SignalR Recebeu: ", mensagem);

    // 1. Escreve a mensagem que veio do servidor na nossa caixa HTML
    document.getElementById("textoSino").innerText = mensagem;

    // 2. Torna a caixa visível no ecrã
    const caixa = document.getElementById("caixaSino");
    caixa.style.display = "block";

    // 3. Temporizador mágico: Esconde o sino ao fim de 7 segundos (7000 milissegundos)
    setTimeout(function () {
        caixa.style.display = "none";
    }, 7000);
});

connection.start().then(function () {
    console.log("✅ SignalR ligado com sucesso à Azure!");
}).catch(function (err) {
    return console.error("❌ Erro a ligar o SignalR: " + err.toString());
});