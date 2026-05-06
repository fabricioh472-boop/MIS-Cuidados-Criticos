const API = "https://cuidados-criticos-production.up.railway.app/api/Consultas";

// 1
async function listadoGeneral() {
    const r = await fetch(API + "/listado-general");
    const d = await r.json();
    document.getElementById("tablaGeneral").innerHTML =
        d.map(x => `<tr><td>${JSON.stringify(x)}</td></tr>`).join("");
}

// 2
async function alertasNivel() {
    const r = await fetch(API + "/alertas-por-nivel");
    const d = await r.json();
    document.getElementById("nivel").innerHTML =
        d.map(x => `<li>${x.nivel} - ${x.cantidad}</li>`).join("");
}

// 3
async function sumaFC() {
    const c = document.getElementById("pacienteFC").value;
    const r = await fetch(API + `/frecuencia-cardiaca-por-paciente?codigo=${c}`);
    const d = await r.json();
    document.getElementById("fc").innerText = JSON.stringify(d);
}

// 4
async function buscarAlerta() {
    const c = document.getElementById("codAlerta").value;
    const r = await fetch(API + `/Alerta-por-codigo/${c}`);
    document.getElementById("alerta").innerText = await r.text();
}

// 5
async function signosSinAlerta() {
    const r = await fetch(API + "/Signo-sin-alerta");
    const d = await r.json();
    document.getElementById("sinAlerta").innerHTML =
        d.map(x => `<li>${x.codigoSigno}</li>`).join("");
}

// 6
async function estadoPaciente() {
    const r = await fetch(API + "/estado-actual-paciente");
    document.getElementById("estado").innerText = await r.text();
}

// 7
async function historial() {
    const c = document.getElementById("histPaciente").value;
    const r = await fetch(API + `/historial-signos/${c}`);
    const d = await r.json();
    document.getElementById("historial").innerHTML =
        d.map(x => `<li>${JSON.stringify(x)}</li>`).join("");
}

// 8
async function masAlertas() {
    const r = await fetch(API + "/Paciente-con-mas-alertas");
    document.getElementById("mas").innerText = await r.text();
}

// 9
async function sinAlertas() {
    const r = await fetch(API + "/Paciente-sin-alerta");
    const d = await r.json();
    document.getElementById("sin").innerHTML =
        d.map(x => `<li>${x.codigo}</li>`).join("");
}

// 10
async function oxigenoBajo() {
    const r = await fetch(API + "/Oxigeno%20bajo");
    const d = await r.json();
    document.getElementById("oxi").innerHTML =
        d.map(x => `<li>${x.codigo} - ${x.saturacion_oxigeno}</li>`).join("");
}

// 11
async function evolucion() {
    const c = document.getElementById("evoPaciente").value;
    const r = await fetch(API + `/evolucion-clinica/${c}`);
    const d = await r.json();
    document.getElementById("evo").innerHTML =
        d.map(x => `<li>${JSON.stringify(x)}</li>`).join("");
}

// 12
async function cantidadSignos() {
    const r = await fetch(API + "/Cantidad-signos");
    document.getElementById("cant").innerText = await r.text();
}

// 13
async function ultimasAlertas() {
    const r = await fetch(API + "/Mostrar-ultimas-alertas");
    const d = await r.json();
    document.getElementById("ultimas").innerHTML =
        d.map(x => `<li>${x.codigo}</li>`).join("");
}

// 14
async function promedioFC() {
    const c = document.getElementById("promPaciente").value;
    const r = await fetch(API + `/promedio-fc/${c}`);
    document.getElementById("prom").innerText = await r.text();
}

// 15
async function criticos() {
    const r = await fetch(API + "/Pacientes-criticos-con-alerta");
    const d = await r.json();
    document.getElementById("criticos").innerHTML =
        d.map(x => `<li>${x.pacienteCritico}</li>`).join("");
}

// 16
async function filtrar() {
    const fcMin = document.getElementById("fcMin").value;
    const fcMax = document.getElementById("fcMax").value;
    const satMin = document.getElementById("satMin").value;

    const r = await fetch(API + `/filtrar-pacientes-rango?fcMin=${fcMin}&fcMax=${fcMax}&satMin=${satMin}`);
    const d = await r.json();

    document.getElementById("filtrados").innerHTML =
        d.map(x => `<li>${x.paciente}</li>`).join("");
}

// 17
async function comparar() {
    const p1 = document.getElementById("p1").value;
    const p2 = document.getElementById("p2").value;

    const r = await fetch(API + `/comparar-pacientes?cod1=${p1}&cod2=${p2}`);
    document.getElementById("comp").innerText = await r.text();
}

// 18
async function freqAlertas() {
    const p = document.getElementById("faPaciente").value;
    const fi = document.getElementById("fi").value;
    const ff = document.getElementById("ff").value;

    const r = await fetch(API + `/frecuencia-alertas?codigo=${p}&fechaInicio=${fi}&fechaFin=${ff}`);
    document.getElementById("fa").innerText = await r.text();
}

// 19
async function variabilidad() {
    const fi = document.getElementById("vfi").value;
    const ff = document.getElementById("vff").value;

    const r = await fetch(API + `/variabilidad-signos?fechaInicio=${fi}&fechaFin=${ff}`);
    document.getElementById("var").innerText = await r.text();
}