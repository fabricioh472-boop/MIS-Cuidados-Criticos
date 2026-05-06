const API = "https://cuidados-criticos-production.up.railway.app/api/AlertaPaciente";

// LISTAR
async function listarRelaciones() {
    const res = await fetch(API);
    const data = await res.json();

    const tbody = document.getElementById("tablaRelaciones");
    tbody.innerHTML = "";

    data.forEach(r => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${r.paciente}</td>
            <td>${r.alerta}</td>
            <td>${r.tipoAlerta}</td>
            <td>${r.nivel}</td>
        `;

        tbody.appendChild(row);
    });
}

// ASIGNAR ALERTA A PACIENTE
async function asignarAlerta() {
    const alerta = document.getElementById("codigoAlerta").value;
    const paciente = document.getElementById("codigoPaciente").value;

    const url = `${API}?CodigoAlerta=${alerta}&CodigoPaciente=${paciente}`;

    const res = await fetch(url, {
        method: "POST"
    });

    const msg = await res.text();
    alert(msg);

    listarRelaciones();
}