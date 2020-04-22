document.addEventListener('DOMContentLoaded', function () {
    document.querySelector("#plantSearchButton").onclick = Plant;
});


function Plant() {
    let plantSearch = document.querySelector("#plantSearch").value;//this is value 
    if (plantSearch == '') {
        return;
    }
    document.querySelector("#plantForm").hidden = false;

    let botanicalName = document.querySelector("#botanicalName");
    let commonName = document.querySelector("#commonName");
    let chromosomeNo = document.querySelector("#chromosomeNo");
    let genus = document.querySelector("#genus");
    let species = document.querySelector("#species");
    let uses = document.querySelector("#uses");
    let medicalBenefits = document.querySelector("#medicalBenefits");
    let healthHazards = document.querySelector("#healthHazards");

    let plantSelect = document.querySelector("#plantSelect");
    let plantDelete = document.querySelector("#plantDelete");
    let plantInsert = document.querySelector("#plantInsert");
    let familySelect = document.querySelector("#familySelect");

    $.get(`/api/PlantApi/?botanicalName=${plantSearch}`, function (data, status) {

        if ($.isEmptyObject(data)) {
            //object is empty
            botanicalName.value = plantSearch;
            commonName.value = '';
            chromosomeNo.value = '';
            genus.value = '';
            species.value = '';
            uses.value = '';
            medicalBenefits.value = '';
            healthHazards.value = '';

            commonName.disabled = false;
            chromosomeNo.disabled = false;
            genus.disabled = false;
            species.disabled = false;
            uses.disabled = false;
            medicalBenefits.disabled = false;
            healthHazards.disabled = false;

            plantSelect.hidden = true;
            plantDelete.hidden = true;
            familySelect.hidden = true;
            plantInsert.hidden = false;

            var message = `
                                <div class="alert alert-danger">
                                    <strong>Note: </strong>Plant details not found for the selected Family. Enter new details.
                                </div>
                                `;

            $(message).insertBefore('#botanicalName').delay(3000).fadeOut();
        }
        else {
            //found the existing data
            plantId = data["plantId"];
            familyId = data["familyId"];

            botanicalName.value = data["botanicalName"];
            commonName.value = data["commonName"];
            chromosomeNo.value = data["chromosomeNo"];
            genus.value = data["genus"];
            species.value = data["species"];
            uses.value = data["uses"];
            medicalBenefits.value = data["medicalBenefits"];
            healthHazards.value = data["healthHazards"];

            commonName.disabled = true;
            chromosomeNo.disabled = true;
            genus.disabled = true;
            species.disabled = true;
            uses.disabled = true;
            medicalBenefits.disabled = true
            healthHazards.disabled = true;

            plantSelect.hidden = false;
            plantDelete.hidden = false;
            familySelect.hidden = false;
            plantInsert.hidden = true;
            //Add link to AddVariety page
            plantSelect.href = `/Plant/AddVariety/?familyId=${familyId}&plantId=${plantId}`;

            plantDelete.href = `/Plant/DeletePlant/?familyId=${familyId}&plantId=${plantId}`;
        }
    });
}