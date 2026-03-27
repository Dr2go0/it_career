// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function CreatKino() {
    if (!document.getElementById("kinoForm")) {
        const form = document.createElement("form");
        form.className = "form-group m-2 p-2 d-flex flex-column align-items-start";
        form.id = "kinoForm";
        form.method = "POST";
        form.action = "/Home/SaveKino";

        const btnClose = document.createElement("button");
        btnClose.className = "btn btn-close m-2";
        btnClose.addEventListener("click", function (event) {
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
        inputLocation.autocomplete = "name webauthn";

        const inputCapacity = document.createElement("input");
        inputCapacity.type = "number";
        inputCapacity.name = "Capacity";
        inputCapacity.placeholder = "Capacity";
        inputCapacity.className = "form-control m-2";
        inputCapacity.autocomplete = "name webauthn";

        document.getElementById("formContainer").appendChild(form);

        form.appendChild(btnClose);
        form.appendChild(inputName);
        form.appendChild(inputLocation);
        form.appendChild(inputCapacity);
        form.appendChild(btnCreate);
    }
}

function CreatFilm() {
    if (!document.getElementById("filmForm")) {
        const form = document.createElement("form");
        form.className = "form-group m-2 p-2 d-flex flex-column align-items-start";
        form.id = "filmForm";
        form.method = "POST";
        form.action = "/Films/Save";

        const btnClose = document.createElement("button");
        btnClose.className = "btn btn-close m-2";
        btnClose.addEventListener("click", function (event) {
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

        const inputGenre = document.createElement("input");
        inputGenre.type = "text";
        inputGenre.name = "Genre";
        inputGenre.placeholder = "Genre";
        inputGenre.className = "form-control m-2";

        const inputDuration = document.createElement("input");
        inputDuration.type = "number";
        inputDuration.name = "Duration";
        inputDuration.placeholder = "Duration in hours";
        inputDuration.className = "form-control m-2";
        inputDuration.autocomplete = "name webauthn";

        const inputReleased = document.createElement("input");
        inputReleased.type = "date";
        inputReleased.name = "ReleaseDate";
        inputReleased.placeholder = "Released:";
        inputReleased.className = "form-control m-2";
        inputReleased.autocomplete = "name webauthn";

        document.getElementById("formContainer").appendChild(form);

        form.appendChild(btnClose);
        form.appendChild(inputName);
        form.appendChild(inputGenre);
        form.appendChild(inputDuration);
        form.appendChild(inputReleased);
        form.appendChild(btnCreate);
    }
}

function AddProjection(kinoId) {
    if (!document.getElementById("ScheduleForm")) {
        const form = document.createElement("form");
        form.className = "form-group m-2 p-2 d-flex flex-column align-items-start";
        form.id = "ScheduleForm";
        form.method = "POST";
        form.action = "/Home/SaveSchedule";

        const btnClose = document.createElement("button");
        btnClose.className = "btn btn-close m-2";
        btnClose.addEventListener("click", function (event) {
            form.remove();
        });

        const btnCreate = document.createElement("button");
        btnCreate.className = "btn btn-primary m-2";
        btnCreate.type = "submit";
        btnCreate.innerHTML = "Submit";
        btnCreate.addEventListener("click", function (event) {
            form.submit();
        });

        const inputKinoId = document.createElement("input");
        inputKinoId.type = "hidden";
        inputKinoId.name = "KinoId";
        inputKinoId.value = kinoId;

        const inputSelectFilm = document.createElement("select");
        inputSelectFilm.name = "FilmId";
        inputSelectFilm.className = "form-control m-2";
        inputSelectFilm.Id = "FilmSelect";

        FilmsRaw.forEach(film => {
            const option = document.createElement("option");
            option.value = film.Id;
            option.textContent = film.Name;
            option.innerHTML = film.Name;
            inputSelectFilm.appendChild(option);
        });

        const inputDate = document.createElement("input");
        inputDate.type = "date";
        inputDate.name = "ProjectionDate";
        inputDate.placeholder = "Released:";
        inputDate.className = "form-control m-2";
        inputDate.autocomplete = "name webauthn";

        document.getElementById("formContainer").appendChild(form);

        form.appendChild(btnClose);
        form.appendChild(inputSelectFilm);
        form.appendChild(inputDate);
        form.appendChild(btnCreate);
        form.appendChild(inputKinoId);
    }
}
function KinoSchedule(kinoId) {
    window.location.href = '/Home/KinoSchedule?kinoId=' + kinoId;
}
function Book(filmScheduleId) {
    const url = `/Home/Booked?filmScheduleId=${filmScheduleId}`;
    window.location.href = url;
}