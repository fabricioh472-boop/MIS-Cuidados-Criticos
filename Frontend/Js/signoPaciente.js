const API = "https://cuidados-criticos-production.up.railway.app/api/SignoPaciente";

// LISTAR
async function listar() {
    const res = await fetch(API + "/listar-signo-paciente");
    const data = await res.json();

    document.getElementById("lista").innerHTML =
        data.map(x =>
            `<p>${x.paciente} - ${x.signoVital} - ${x.fecha_hora}</p>`
        ).join("");
}

// CREAR
async function crear() {
    const codigopaciente = document.getElementById("codPaciente").value;
    const codigosigno = document.getElementById("codSigno").value;

    await fetch(API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ codigopaciente, codigosigno })
    });

    alert("Relación creada");
    listar();
}