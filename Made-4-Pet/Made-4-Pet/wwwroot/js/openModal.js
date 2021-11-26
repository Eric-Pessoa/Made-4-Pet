var agendado = document.getElementById("id-agendado").innerHTML;
console.log("agendado: " + agendado);
console.log(typeof agendado)

var botao = document.getElementById("botaoModal");
if (agendado == "True") {
    console.log("eai");
    botao.click();
}
