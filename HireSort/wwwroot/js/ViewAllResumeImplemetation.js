

var deptID;
var vacID;
$(document).ready(function () {
    //var queryString = window.location.href;
    ////window.location.href.slice(window.location.href.indexOf('?') + 1);
    //const searchParams = queryString.searchParams;
    //var deptID = searchParams.get('departId');
    //var vacID = searchParams.get('vacancyId');
  
    const params = new URLSearchParams(window.location.search);
    deptID = params.get('departId');
    vacID = params.get('vacancyId');

    getResumeList();


});



function getResumeList() {

    const uriResumeList = `/api/Dashboard/resume-list?departId=${deptID}&vacancyId=${vacID}`
    fetch(uriResumeList)
        .then(response => response.json())
        .then(data => _displayResumeList(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayResumeList(data) {

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