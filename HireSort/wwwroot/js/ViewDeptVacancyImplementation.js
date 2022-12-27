

var deptID;
$(document).ready(function () {
    //var queryString = window.location.href;
    ////window.location.href.slice(window.location.href.indexOf('?') + 1);
    //const searchParams = queryString.searchParams;
    //var deptID = searchParams.get('departId');
    //var vacID = searchParams.get('vacancyId');

    const params = new URLSearchParams(window.location.search);
    deptID = params.get('departId');


    getVacancyList();

});



function getVacancyList() {

    const uriVacancyList = `api/Dashboard/depart-vacancy-list?departId=${deptID}`
    fetch(uriVacancyList)
        .then(response => response.json())
        .then(data => _displayVacancyList(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayVacancyList(data) {

    alert("testing");
    //const ddl_Vac = document.getElementById('vacancy');

    //if (data.statusCode == 200) {
    //    var parsedata = data.successData;

    //    parsedata.forEach(item => {

    //        let option_elem = document.createElement('option');
    //        option_elem.value = item.vacancyId;
    //        option_elem.textContent = item.vacancyName;
    //        ddl_Vac.appendChild(option_elem);
    //    });

    //}

}