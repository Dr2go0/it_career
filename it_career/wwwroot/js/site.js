// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function CreatKino() {

    const form = document.createElement("form");
    form.ClassName = "form-group";
    form.ClassName = "horizontal";
    form.method = "POST";

    form.action = "/Home/SaveKino";

    const btnClose = document.createElement("button");
    btnClose.ClassName = "btn-close";
    btnClose.innerHTML = "X";

    const btnCreate = document.createElement("button");
    btnCreate.ClassName = "btn btn-primary";
    btnCreate.type = "submit";
    btnCreate.innerHTML = "Submit";

    const btnSchedules = document.createElement("button");
    btnSchedules.ClassName = "btn btn-secondary";
    btnSchedules.type = "button";
    btnSchedules.innerHTML = "Schedules";
    btnCreate.addEventListener("click", function (event) {
        form.submit();
        document.getElementById("kinosContainer").InnerHTML = "<p> Name:" + form.elements["Name"].value + "</p><p> Location:" + from.elements["Location"].value + "</p>";
        document.getElementById("kinosContainer").appendChild(btnSchedules);
        form.reset();
    });

    const inputName = document.createElement("input");
    inputName.type = "text";
    inputName.name = "Name";
    inputName.placeholder = "Name";
    inputName.className = "form-control";

    const inputLocation = document.createElement("input");
    inputLocation.type = "text";
    inputLocation.name = "Location";
    inputLocation.placeholder = "Location";
    inputLocation.className = "form-control";

    const inputCapacity = document.createElement("input");
    inputCapacity.type = "number";
    inputCapacity.name = "Capacity";
    inputCapacity.placeholder = "Capacity";
    inputCapacity.className = "form-control";

    document.getElementById("kinosContainer").appendChild(form);

    form.appendChild(btnClose);
    form.appendChild(inputName);
    form.appendChild(inputLocation);
    form.appendChild(inputCapacity);
    form.appendChild(btnCreate);
}