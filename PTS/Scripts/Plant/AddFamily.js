document.addEventListener('DOMContentLoaded', function () {
    document.querySelector("#familySearchButton").onclick = family;
});



function family() {
    var familySearch = document.querySelector("#familySearch").value;
    if (familySearch == '') {
        return;
    }
    document.querySelector("#familyForm").hidden = false;

    var familyName = document.querySelector("#familyName");
    var familyCommonName = document.querySelector("#familyCommonName");
    var habitat = document.querySelector("#habitat");
    var familySelect = document.querySelector("#familySelect");
    var familyDelete = document.querySelector("#familyDelete");
    var familyInsert = document.querySelector("#familyInsert");
    
    $.get(`/api/familyapi/?familyName=${familySearch}`, function (data, status) {

        if ($.isEmptyObject(data)) {
            //object is empty
            familyName.value = document.querySelector("#familySearch").value;
            familyCommonName.value = '';
            habitat.value = '';

            familyCommonName.disabled = false;
            habitat.disabled = false;
            familySelect.hidden = true;
            familyDelete.hidden = true;
            familyInsert.hidden = false;

            var message = `
                                <div class="alert alert-danger">
                                    <strong>Note: </strong>Family details does not exist. Enter new details.
                                </div>
                                `;

            $(message).insertBefore('#familyName').delay(3000).fadeOut(); //Insert "message html" before familyName element
        }
        else {
            //found the existing data
            familyId = data["familyId"];
            familyName.value = data["familyName"];
            familyCommonName.value = data["familyCommonName"];
            habitat.value = data["habitat"];
            familyCommonName.disabled = true;
            habitat.disabled = true;

            familySelect.hidden = false;
            familyDelete.hidden = false;
            familyInsert.hidden = true;
            //Add link to AddPlant page 
            familySelect.href = "/Plant/AddPlant/?familyId=" + familyId;//while you are adding plant u need familyid
            familyDelete.href = "/Plant/DeleteFamily/?familyId=" + familyId;
        }
    });
}

//function familySelect() {
//    document.querySelector("#plantSearchBox").hidden = false;
//    // making plant datalist items empty
//    $("#plantList").empty();
//    $.get('/api/plantapi/?familyId=' + familyId,
//        function (data, status) {
//            for (var i = 0; i < data.length; i++) {
//                $("#plantList").append(`<option value="${data[i]}">`);
//            }
//        });
//}




//function familyInsert() {
//    $.post(
//        '/api/familyapi/',
//        {
//            familyName: document.querySelector("#familyName").value,
//            familyCommonName: document.querySelector("#familyCommonName").value,
//            habitat: document.querySelector("#habitat").value
//        },
//        function (data, status) {
//            familyName.disabled = true;
//            familyCommonName.disabled = true;
//            habitat.disabled = true;
//            familyId = data["familyId"];
//        },
//        "json"
//    );
//    familySelect();
//}
