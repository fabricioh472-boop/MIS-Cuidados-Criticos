const API = "https://cuidados-criticos-production.up.railway.app/api/Paciente";

// LISTAR
async function listarPacientes() {
    const res = await fetch(API);
    const data = await res.json();

    document.getElementById("lista").innerHTML =
        data.map(p => `<p>${p.codigo} - ${p.nomre}</p>`).join("");
}

// BUSCAR
async function buscarPaciente() {
    let codigo = document.getElementById("codigoBuscar").value;

    const res = await fetch(`${API}/${codigo}`);

    if (!res.ok) {
        document.getElementById("resultadoBuscar").innerHTML = "No encontrado";
        return;
    }

    const data = await res.json();

    document.getElementById("resultadoBuscar").innerHTML =
        `${data.codigo} - ${data.nomre}`;
}

// CREAR
async function crearPaciente() {
    let codigo = document.getElementById("codigo").value;
    let nombre = document.getElementById("nombre").value;

    await fetch(API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ codigo, nombre })
    });

    alert("Paciente creado");
    listarPacientes();
}

// ACTUALIZAR
async function actualizarPaciente() {
    let codigo = document.getElementById("codigoUp").value;
    let nombre = document.getElementById("nombreUp").value;

    await fetch(`${API}/${codigo}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ nombre })
    });

    alert("Paciente actualizado");
    listarPacientes();
}

// ELIMINAR (INACTIVAR)
async function eliminarPaciente() {
    let codigo = document.getElementById("codigoDel").value;

    await fetch(`${API}/${codigo}`, {
        method: "DELETE"
    });

    alert("Paciente eliminado");
    listarPacientes();
}