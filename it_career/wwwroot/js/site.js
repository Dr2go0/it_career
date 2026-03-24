// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function CreatKino() {
    const form = document.Create.createElement("form");
    form.ClassName = "form-group";
    form.ClassName = "horizontal";

    const btnCreate = document.Create.createElement("button");
    btnCreate.ClassName = "btn btn-primary";
    btnCreate.type = "submit";
    btnCreate.innerHTML = "Submit";

    const inputName = document.Create.createElement("input");
    inputName.type = "text";
    inputName.name = "Name";
    inputName.placeholder = "Name";
    inputName.className = "form-control";

    const inputLocation = document.Create.createElement("input");
    inputLocation.type = "text";
    inputLocation.name = "Location";
    inputLocation.placeholder = "Location";
    inputLocation.className = "form-control";

    form.apendChild(btnCreate);
    form.apendChild(inputName);
    form.apendChild(inputLocation);
    document.getElementById("kinosContainer").appendChild(form);
}