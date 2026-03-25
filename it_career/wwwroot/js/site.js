// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function CreatKino() {

    const form = document.createElement("form");
    form.className = "form-group m-2 p-2 d-flex flex-column align-items-start";
    form.method = "POST";
    form.action = "/Home/SaveKino";

    const btnClose = document.createElement("button");
    btnClose.className = "btn btn-close m-2";
    btnClose.addEventListener("click", function(event){
        form.remove();
    });

    const btnCreate = document.createElement("button");
    btnCreate.className = "btn btn-primary m-2";
    btnCreate.type = "submit";
    btnCreate.innerHTML = "Submit";
    btnCreate.addEventListener("click", function (event) {
        form.submit();
    });

    const inputName = document.createElement("input");
    inputName.type = "text";
    inputName.name = "Name";
    inputName.placeholder = "Name";
    inputName.className = "form-control m-2";
    inputName.autocomplete = "name webauthn";

    const inputLocation = document.createElement("input");
    inputLocation.type = "text";
    inputLocation.name = "Location";
    inputLocation.placeholder = "Location";
    inputLocation.className = "form-control m-2";

    const inputCapacity = document.createElement("input");
    inputCapacity.type = "number";
    inputCapacity.name = "Capacity";
    inputCapacity.placeholder = "Capacity";
    inputCapacity.className = "form-control m-2";

    document.getElementById("formContainer").appendChild(form);

    form.appendChild(btnClose);
    form.appendChild(inputName);
    form.appendChild(inputLocation);
    form.appendChild(inputCapacity);
    form.appendChild(btnCreate);
}

function KinoSchedule(kinoName) {
    window.location.href = '/Home/KinoSchedule?kinoName=' + kinoName;
}
