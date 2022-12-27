
//<script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>

//var script = document.createElement('script');
//script.src = 'https://code.jquery.com/jquery-3.6.0.min.js';
//document.getElementsByTagName('head')[0].appendChild(script);

const uriDept = 'api/Dashboard/departments';
const uriVacancyList = 'api/Dashboard/department-and-vacancies-details';
const uriVacancyCount = 'api/Dashboard/depart-vacancy-count';

const ddl_Dept = document.getElementById('department');

const ddl_Vac = $('#vacancy');

var currentPage = location.pathname.split("/").slice(-1);

var tableValueColDept;
var tableValueColVacancy;

$(document).ready(function () {

    if (currentPage == "") {
        getItemsDept();
        getItemsVacancyList();
        getItemsVacancyCount();
    }

    else if (currentPage == "ViewAllResume") {
        getResume();
    }

    // code to read selected table row cell data (values).
    $("#tbl_vacancyList").on('click','.viewResume',function(){
         // get the current row
         var currentRow=$(this).closest("tr"); 
         
         var col1=currentRow.find("td:eq(0)").html(); // get current row 1st table cell TD value
        var col2 = currentRow.find("td:eq(1)").attr('id'); // get current row 2nd table cell TD value
        var col3 = currentRow.find("td:eq(2)").attr('id'); // get current row 3rd table cell  TD value

        tableValueColDept = col2;
        tableValueColVacancy = col3;
        
        //alert(col2 + col3);

           
    });



    function getResume() {

        var uriViewAllResume = `api/Dashboard/department-and-vacancies-details?departId=${tableValueColDept}&vacancyId=${tableValueColVacancy}`

        fetch(uriViewAllResume)
            .then(response => response.json())
            .then(data => _displayResumeList(data))
            .catch(error => console.error('Unable to get items.', error));
    }
        function _displayResumeList(data) {

            debugger;
            const tbl_vacancyList = document.getElementById('tab-1');

            $("#tab-1 tr").remove();
            var status = data.statuscode
            var parsedata = data.successdata
            alert("hello");
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

            else if (data.errordata !== null) {
                let infoEmptyTable = document.createElement('h7');
                infoEmptyTable.classList.add('mb-3');
                infoEmptyTable.textContent = "No data available in table";
            }


        }

    
});


// Get Department Dropdown
function getItemsDept() {
    fetch(uriDept)
        .then(response => response.json())
        .then(data => _displayItemsDept(data))
        .catch(error => console.error('Unable to get items.', error));

    getItemsVacancy();
}

function _displayItemsDept(data) {

    //debugger;

    var status = data.statusCode
    var parsedata = data.successData
    if (status == 200) {
        parsedata.forEach(item => {

            let option_elem = document.createElement('option');
            option_elem.value = item.departmentId;
            option_elem.textContent = item.departmentName;
            ddl_Dept.appendChild(option_elem);
        });

        //$("option").each(function (index) {
        //	$(this).on("click", function () {
        //		alert("hello")
        //	});
        //});
    }

}

$("#department").change(function () {
    getItemsVacancy();
});

$("#btn_Search").click(function myfunction() {

    var dpt_id = ddl_Dept.value;
    var vac_Id = ddl_Vac.val();

    //alert("Handler for .change() called." + ` with ids dept: ${dpt_id} and vacId: ${vac_Id}`);


    //fetch(uri, {
    //    method: 'POST',
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json'
    //    },
    //    body: JSON.stringify(item)
    //})
    var filteredVacList = `api/Dashboard/department-and-vacancies-details?departId=${dpt_id}&vacancyId=${vac_Id}`

    fetch(filteredVacList)
        .then(response => response.json())
        .then(data => _displayItemsVacancyList(data))
        .catch(error => console.error('Unable to get items.', error));

});
// Get Vacancy Dropdown
function getItemsVacancy() {

    var deptid = ddl_Dept.value;
    console.log(ddl_Dept.options[ddl_Dept.selectedIndex].text);
    console.log(ddl_Dept.value);
    if (deptid == "")
        deptid = '1';

    const uriVacancy = `api/Dashboard/vacancies-department-wise?departId=${deptid}`;
    fetch(uriVacancy)
        .then(response => response.json())
        .then(data => _displayItemsVacancy(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayItemsVacancy(data) {

    const ddl_Vac = document.getElementById('vacancy');

    if (data.statusCode == 200) {
        var parsedata = data.successData;

        parsedata.forEach(item => {

            let option_elem = document.createElement('option');
            option_elem.value = item.vacancyId;
            option_elem.textContent = item.vacancyName;
            ddl_Vac.appendChild(option_elem);
        });

    }

}

//for finding vacancies according to the department
function myFunction() {
    var input, filter, table, tr, td, i;
    input = document.getElementById("department");
    filter = input.value;
    //alert(filter);
    table = document.getElementById("tbl_vacancyList");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            if (td.innerHTML.indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}


//Get Department and Vacancy Count
function getItemsVacancyCount() {
    fetch(uriVacancyCount)
        .then(response => response.json())
        .then(data => _displayItemsVacancyCount(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayItemsVacancyCount(data) {

    //debugger;
    const vacancyCount = document.getElementById('vacancy_Count');

    var status = data.statusCode
    var parsedata = data.successData
    if (status == 200) {
        parsedata.forEach(item => {

            let divCard = document.createElement('div');
            divCard.classList.add('col-lg-4', 'col-sm-6', 'wow', 'fadeInUp');
            divCard.setAttribute('style', 'visibility: visible; animation - delay: 0.7s; animation - name: fadeInUp;');

            let divMain = document.createElement('div');
            divMain.classList.add('cat-item', 'rounded', 'p-4');

            let icon = document.createElement('i');
            //icon.classList.add('fa', 'fa-3x','fa-tasks', 'text-primary','mb-4');

            if (item.departmentName == "Project Management") {
                icon.classList.add('fa', 'fa-3x', 'fa-tasks', 'text-primary', 'mb-4');
            }

            else if (item.departmentName == "Human Resource") {
                icon.classList.add('fa', 'fa-3x', 'fa-user-tie', 'text-primary', 'mb-4');
            }

            else if (item.departmentName == "Information Technology") {
                icon.classList.add('fa', 'fa-3x', 'fa-chart-line', 'text-primary', 'mb-4');
            }


            else if (item.departmentName == "Finance") {
                icon.classList.add('fa', 'fa-3x', 'fa-money-check-alt', 'text-primary', 'mb-4');
            }



            let departmentTitle = document.createElement('h6');
            departmentTitle.classList.add('mb-3');
            departmentTitle.textContent = item.departmentName;

            let btnVacancyCount = document.createElement('a');
            btnVacancyCount.href = "ViewDeptVacancy/ViewDeptVacancy";

            let lblVacancyCount = document.createElement('p');
            lblVacancyCount.classList.add('mb-0');
            if (item.vacancyCounts <= 1) {
                lblVacancyCount.textContent = item.vacancyCounts + "\nVacancy";
            }
            else {
                lblVacancyCount.textContent = item.vacancyCounts + "\nVacancies";
            }

            btnVacancyCount.appendChild(lblVacancyCount);

            divMain.appendChild(icon);
            divMain.appendChild(departmentTitle);
            divMain.appendChild(btnVacancyCount);
            divCard.appendChild(divMain);
            vacancyCount.appendChild(divCard);

        });
    }
}


//Get Department and Vacancy List
function getItemsVacancyList() {
    fetch(uriVacancyList)
        .then(response => response.json())
        .then(data => _displayItemsVacancyList(data))
        .catch(error => console.error('Unable to get items.', error));
}
function _displayItemsVacancyList(data) {

    debugger;
    const tbl_vacancyList = document.getElementById('tbl_vacancyList');

    $("#tbl_vacancyList tr").remove();
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
            td2.textContent = item.departmentName;
            td2.id = item.depertId;
            td3.textContent = item.vacancyName;
            td3.id = item.vacancyId;

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

    else if (data.errordata !== null) {
        let infoEmptyTable = document.createElement('h7');
        infoEmptyTable.classList.add('mb-3');
        infoEmptyTable.textContent = "No data available in table"; 
    }


}