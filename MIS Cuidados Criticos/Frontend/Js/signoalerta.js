const API = "https://cuidados-criticos-production.up.railway.app/api/SignoAlerta";

// LISTAR
async function listar() {
    const res = await fetch(API);
    const data = await res.json();

    document.getElementById("lista").innerHTML =
        data.map(x => `<p>${x.signosVitales} → ${x.alerta}</p>`).join("");
}

// CREAR
async function crear() {
    const codigoSigno = document.getElementById("codigoSigno").value;
    const codigoAlerta = document.getElementById("codigoAlerta").value;

    await fetch(API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ codigoSigno, codigoAlerta })
    });

    alert("Relación creada");
    listar();
}