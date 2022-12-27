// code to read selected table row cell data (values).
$("#tbl_vacancyList").on('click', '.viewResume', function () {
    // get the current row
    var currentRow = $(this).closest("tr");

    var col1 = currentRow.find("td:eq(0)").html(); // get current row 1st table cell TD value
    var col2 = currentRow.find("td:eq(1)").attr('id'); // get current row 2nd table cell TD value
    var col3 = currentRow.find("td:eq(2)").attr('id'); // get current row 3rd table cell  TD value

    tableValueColDept = col2;
    tableValueColVacancy = col3;

    //alert(col2 + col3);

    var uriViewAllResume = `api/Dashboard/department-and-vacancies-details?departId=${tableValueColDept}&vacancyId=${tableValueColVacancy}`

    fetch(uriViewAllResume)
        .then(response => response.json())
        .then(data => _displayResumeList(data))
        .catch(error => console.error('Unable to get items.', error));
});


function _displayResumeList(data) {

    debugger;
    const tbl_vacancyList = document.getElementById('tab-1');

    $("#tab-1 tr").remove();
    var status = data.statusCode
    var parsedata = data.successData
    if (status == 200) {
        let serialNo = 0;
        parsedata.forEach(item => {


            debugger
            let tr = document.createElement('tr');

            let td1 = document.createElement('td');
            let td2 = document.createElement('td');
            let td3 = document.createElement('td');
            let td4 = document.createElement('td');


            serialNo++;
            td1.textContent = serialNo;
            td2.textContent = "test";

            td3.textContent = "test";

            let btnViewAllResume = document.createElement('a');
            let textViewAllResume = document.createTextNode("View All Resume");
            btnViewAllResume.href = "ViewAllResume/ViewAllResume";
            btnViewAllResume.className = "viewResume";
            btnViewAllResume.appendChild(textViewAllResume);

            td4.appendChild(btnViewAllResume);

            tr.appendChild(td1);
            tr.appendChild(td2);
            tr.appendChild(td3);
            tr.appendChild(td4);
            tbl_vacancyList.appendChild(tr);
        });

    }

    //else if (data.errordata !== null) {
    //    let infoEmptyTable = document.createElement('h7');
    //    infoEmptyTable.classList.add('mb-3');
    //    infoEmptyTable.textContent = "No data available in table";
    //}


}