const API = "https://cuidados-criticos-production.up.railway.app/api/SignoVital";

// -------------------- LISTAR --------------------
async function listar() {
    const res = await fetch(API);
    const data = await res.json();

    document.getElementById("lista").innerHTML =
        data.map(x => `
            <p>
                ${x.codigo} | FC: ${x.frecuencia_cardiaca} | 
                Sat: ${x.saturacion_oxigeno} | PA: ${x.presion_arterial}
            </p>
        `).join("");
}

// -------------------- CREAR --------------------
async function crear() {
    const codigo = document.getElementById("codigo").value;
    const fc = document.getElementById("fc").value;
    const sat = document.getElementById("sat").value;
    const pa = document.getElementById("pa").value;

    const url = `${API}?codigo=${codigo}&frecuencia_cardiaca=${fc}&saturacion_oxigeno=${sat}&presion_arterial=${pa}`;

    const res = await fetch(url, {
        method: "POST"
    });

    if (!res.ok) {
        alert("Error al crear signo vital");
        return;
    }

    alert("Creado correctamente");
    listar();
}

// -------------------- BUSCAR --------------------
async function buscar() {
    const codigo = document.getElementById("buscar").value;

    const res = await fetch(`${API}/${codigo}`);

    const out = document.getElementById("resultado");

    if (!res.ok) {
        out.innerHTML = "No encontrado";
        return;
    }

    const x = await res.json();

    out.innerHTML = `
        <p>
            ${x.codigo} | FC: ${x.frecuencia_cardiaca} |
            Sat: ${x.saturacion_oxigeno} | PA: ${x.presion_arterial}
        </p>
    `;
}

// -------------------- ACTUALIZAR --------------------
async function actualizar() {
    const codigo = document.getElementById("codigo").value;
    const fc = document.getElementById("fc").value;
    const sat = document.getElementById("sat").value;
    const pa = document.getElementById("pa").value;

    const url = `${API}/${codigo}?frecuencia_cardiaca=${fc}&saturacion_oxigeno=${sat}&presion_arterial=${pa}`;

    const res = await fetch(url, {
        method: "PUT"
    });

    if (!res.ok) {
        alert("Error al actualizar");
        return;
    }

    alert("Actualizado correctamente");
    listar();
}

// -------------------- ELIMINAR --------------------
async function eliminar() {
    const codigo = document.getElementById("buscar").value;

    const res = await fetch(`${API}/${codigo}`, {
        method: "DELETE"
    });

    if (!res.ok) {
        alert("Error al eliminar");
        return;
    }

    alert("Eliminado correctamente");
    listar();
}