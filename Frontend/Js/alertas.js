const API = "https://cuidados-criticos-production.up.railway.app/api/Alerta";

// LISTAR
async function listarAlertas() {
    const res = await fetch(API);
    const data = await res.json();

    const lista = document.getElementById("listaAlertas");
    lista.innerHTML = "";

    data.forEach(a => {
        const li = document.createElement("li");
        li.innerText = `${a.codigo} - ${a.tipo} - ${a.nivel_criticidad}`;
        lista.appendChild(li);
    });
}

// BUSCAR
async function buscarAlerta() {
    const codigo = document.getElementById("codigoBuscar").value;

    const res = await fetch(`${API}/${codigo}`);

    if (!res.ok) {
        document.getElementById("resultadoBuscar").innerText = "No encontrado";
        return;
    }

    const data = await res.json();

    document.getElementById("resultadoBuscar").innerText =
        `${data.codigo} - ${data.tipo} - ${data.nivel_criticidad}`;
}

// CREAR
async function crearAlerta() {
    const codigo = document.getElementById("codigo").value;
    const tipo = document.getElementById("tipo").value;
    const nivel = document.getElementById("nivel").value;

    await fetch(API + `?codigo=${codigo}&tipo=${tipo}&nvcriticidad=${nivel}`, {
        method: "POST"
    });

    alert("Alerta creada");
}

// ACTUALIZAR
async function actualizarAlerta() {
    const codigo = document.getElementById("codigoUpdate").value;
    const tipo = document.getElementById("tipoUpdate").value;
    const nivel = document.getElementById("nivelUpdate").value;

    await fetch(`${API}/${codigo}?tipo=${tipo}&nvcriticidad=${nivel}`, {
        method: "PUT"
    });

    alert("Alerta actualizada");
}

// ELIMINAR
async function eliminarAlerta() {
    const codigo = document.getElementById("codigoDelete").value;

    await fetch(`${API}/${codigo}`, {
        method: "DELETE"
    });

    alert("Alerta eliminada");
}