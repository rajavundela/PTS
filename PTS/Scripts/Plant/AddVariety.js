document.addEventListener('DOMContentLoaded', function () {
    document.querySelector("#varietySearchButton").onclick = variety;
});


function variety() {
    let varietySearch = document.querySelector("#varietySearch").value;//this is value
    if (varietySearch == '') {
        return;
    }
    document.querySelector("#varietyForm").hidden = false;

    let varietyName = document.querySelector("#varietyName");
    let nature = document.querySelector("#nature");
    let timeOfSetting = document.querySelector("#timeOfSetting");
    let timeOfFlowering = document.querySelector("#timeOfFlowering");
    let rotationPeriod = document.querySelector("#rotationPeriod");
    let propagationMethod = document.querySelector("#propagationMethod");
    let treeHeight = document.querySelector("#treeHeight");
    let trunkColour = document.querySelector("#trunkColour");
    let treeForm = document.querySelector("#treeForm");
    let leafShape = document.querySelector("#leafShape");
    let fragrance = document.querySelector("#fragrance");
    let woodCharacter = document.querySelector("#woodCharacter");
    let fruitType = document.querySelector("#fruitType");
    let barkColour = document.querySelector("#barkColour");
    let barkTexture = document.querySelector("#barkTexture");
    let litterType = document.querySelector("#litterType");
    let longetivity = document.querySelector("#longetivity");
    let growingConditions = document.querySelector("#growingConditions");

    let varietySelect = document.querySelector("#varietySelect");
    let varietyDelete = document.querySelector("#varietyDelete");
    let varietyInsert = document.querySelector("#varietyInsert");
    let plantSelect = document.querySelector("#plantSelect");

    $.get(`/api/VarietyApi/?varietyName=${varietySearch}`, function (data, status) {

        if ($.isEmptyObject(data)) {
            //object is empty
            varietyName.value = varietySearch;
            nature.value = '';
            timeOfSetting.value = '';
            timeOfFlowering.value = '';
            rotationPeriod.value = '';
            propagationMethod.value = '';
            treeHeight.value = '';
            trunkColour.value = '';
            treeForm.value = '';
            leafShape.value = '';
            fragrance.value = '';
            woodCharacter.value = '';
            fruitType.value = '';
            barkColour.value = '';
            barkTexture.value = '';
            litterType.value = '';
            longetivity.value = '';
            growingConditions.value = '';

            nature.disabled = false;
            timeOfSetting.disabled = false;
            timeOfFlowering.disabled = false;
            rotationPeriod.disabled = false;
            propagationMethod.disabled = false;
            treeHeight.disabled = false;
            trunkColour.disabled = false;
            treeForm.disabled = false;
            leafShape.disabled = false;
            fragrance.disabled = false;
            woodCharacter.disabled = false;
            fruitType.disabled = false;
            barkColour.disabled = false;
            barkTexture.disabled = false;
            litterType.disabled = false;
            longetivity.disabled = false;
            growingConditions.disabled = false;
            

            varietySelect.hidden = true;
            varietyDelete.hidden = true;
            plantSelect.hidden = true;
            varietyInsert.hidden = false;

            var message = `
                                <div class="alert alert-danger">
                                    <strong>Note: </strong>Variety details not found for the selected Plant. Enter new details.
                                </div>
                                `;

            $(message).insertBefore('#varietyName').delay(3000).fadeOut();
        }
        else {
            //found the existing data
            plantId = data["plantId"];
            varietyId = data["varietyId"];

            varietyName.value = data["varietyName"];
            nature.value = data["nature"];
            timeOfSetting.value = data["timeOfSetting"];
            timeOfFlowering.value = data["timeOfFlowering"];
            rotationPeriod.value = data["rotationPeriod"];
            propagationMethod.value = data["propagationMethod"];
            treeHeight.value = data["treeHeight"];
            trunkColour.value = data["trunkColour"];
            treeForm.value = data["treeForm"];
            leafShape.value = data["leafShape"];
            fragrance.value = data["fragrance"];
            woodCharacter.value = data["woodCharacter"];
            fruitType.value = data["fruitType"];
            barkColour.value = data["barkColour"];
            barkTexture.value = data["barkTexture"];
            litterType.value = data["litterType"];
            longetivity.value = data["longetivity"];
            growingConditions.value = data["growingConditions"];

            nature.disabled = true;
            timeOfSetting.disabled = true;
            timeOfFlowering.disabled = true;
            rotationPeriod.disabled = true;
            propagationMethod.disabled = true;
            treeHeight.disabled = true;
            trunkColour.disabled = true;
            treeForm.disabled = true;
            leafShape.disabled = true;
            fragrance.disabled = true;
            woodCharacter.disabled = true;
            fruitType.disabled = true;
            barkColour.disabled = true;
            barkTexture.disabled = true;
            litterType.disabled = true;
            longetivity.disabled = true;
            growingConditions.disabled = true;

            varietySelect.hidden = false;
            varietyDelete.hidden = false;
            plantSelect.hidden = false;
            varietyInsert.hidden = true;
            //Add link to AddLocationDate page 
            varietySelect.href += `&plantId=${plantId}&varietyId=${varietyId}`;

            let link = window.location.href; // gets current page link 
            varietyDelete.href = link.replace('AddVariety', 'DeleteVariety') + "&varietyId=" + varietyId;
        }
    });
}