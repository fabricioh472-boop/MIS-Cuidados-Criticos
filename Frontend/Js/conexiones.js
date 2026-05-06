const API = "https://cuidados-criticos-production.up.railway.app/api/Conexiones";

// ENFERMERAS DISPONIBLES
async function cargarEnfermeras() {
    const res = await fetch(`${API}/enfermeras-disponibles`);
    const data = await res.json();

    const lista = document.getElementById("listaEnfermeras");
    lista.innerHTML = "";

    data.forEach(e => {
        const li = document.createElement("li");
        li.innerText = JSON.stringify(e);
        lista.appendChild(li);
    });
}

// RECIBIR PACIENTE
async function recibirPaciente() {
    const codigo = document.getElementById("codigo").value;
    const nombre = document.getElementById("nombre").value;

    const url = `${API}/recibir-paciente-logistica?codigo=${codigo}&nombre=${nombre}`;

    const res = await fetch(url, {
        method: "POST"
    });

    const data = await res.json();

    document.getElementById("respuesta").innerText =
        `${data.mensaje} - ${data.codigo} - ${data.nomre}`;
}