

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

    const uriResumeList = `/api/Dashboard/resume-list?departId=${deptID}&vacancyId=${vacID}&isShortListedResume=true`
    fetch(uriResumeList)
        .then(response => response.json())
        .then(data => _displayResumeList(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayResumeList(data) {
    const resumeList = document.getElementById('tab-1');

    var status = data.statusCode
    var parsedata = data.successData.resumes
    if (status == 200) {
        parsedata.forEach(item => {

           
            let divCardMain = document.createElement('div');
            divCardMain.classList.add('job-item', 'p-4', 'mb-4');

            let divCardRow = document.createElement('div');
            divCardRow.classList.add('row', 'g-4');

            let divCol = document.createElement('div');
            divCol.classList.add('col-sm-12', 'col-md-8', 'd-flex', 'align-items-center');

            let icon = document.createElement('i');
            icon.classList.add('fa', 'fa-3x', 'fa-user', 'text-primary', 'mb-4');

            let divText = document.createElement('div');
            divText.classList.add('text-start', 'ps-4');

            let txtCandidate = document.createElement('h5');
            txtCandidate.classList.add('mb-3');
            txtCandidate.textContent = item.candidateName;

            let txtEmail = document.createElement('span');
            txtEmail.classList.add('text-truncate', 'me-3');
            txtEmail.textContent = item.emailAddress;

            let iconEmail = document.createElement('i');
            iconEmail.classList.add('fa', 'fa-envelope', 'text-primary', 'me-2');

            let txtMobile = document.createElement('span');
            txtMobile.classList.add('text-truncate', 'me-3');
            txtMobile.textContent = item.mobileNo;

            let iconMobile = document.createElement('i');
            iconMobile.classList.add('fa', 'fa-mobile', 'text-primary', 'me-2');

            let divColBtn = document.createElement('div');
            divColBtn.classList.add('col-sm-12','col-md-4', 'd-flex', 'flex-column', 'align-items-start', 'align-items-md-end', 'justify-content-center');

            let divBtnFlx = document.createElement('div');
            divBtnFlx.classList.add('d-flex', 'mb-3');

           

         

            let btnViewDetails = document.createElement('a');
            btnViewDetails.classList.add('btn', 'btn-primary', 'me-3');
            let textViewDetails = document.createTextNode("View Details");
            btnViewDetails.href = '/ViewJobDetail/ViewJobDetail'
            btnViewDetails.appendChild(textViewDetails);


            let btnCheckCompInd = document.createElement('a');
            btnCheckCompInd.classList.add('btn', 'btn-primary');
            let textCheckComp = document.createTextNode("Check Compatibility");
            btnCheckCompInd.href = '/CheckCompatibiltyIndividual/CheckCompatibiltyIndividual'
            btnCheckCompInd.appendChild(textCheckComp);
            

          
            txtEmail.appendChild(iconEmail);
            txtMobile.appendChild(iconMobile);
            divText.appendChild(txtCandidate);
            divText.appendChild(txtEmail);
            divText.appendChild(txtMobile);

            divCol.appendChild(icon);
            divCol.appendChild(divText);

            divBtnFlx.appendChild(btnViewDetails);
            divBtnFlx.appendChild(btnCheckCompInd);

            divColBtn.appendChild(divBtnFlx);

            divCardRow.appendChild(divCol);

            divCardRow.appendChild(divColBtn)

            divCardMain.appendChild(divCardRow);

            resumeList.appendChild(divCardMain);


        });
    }
}
