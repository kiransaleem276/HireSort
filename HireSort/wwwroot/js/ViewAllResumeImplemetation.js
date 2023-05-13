

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
    //resumeShortlist();
});

$('#files').on('change', function () {
    //get the file name
    var fileName = $(this).val();
    //replace the "Choose a file" label
    $(this).next('.custom-file-label').html(fileName);
})


function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;

    var formData = new FormData();
    formData.append("files", files[0]);

    $.ajax(
        {
            //url: "/api/dashboard/uploadfile?jobId=1",
            url: `/api/dashboard/test?jobId=${vacID}`,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                location.reload();
                //getResumeList();
                //alert("Files Uploaded!");
            }
        }
    );
}


//$("#formFile").on('change', function () {
//    const uriUploadedResume = `/api/dashboard/uploadfile?jobId=1`

//    fetch(uriUploadedResume)
//        .then(function (result) {
//            alert("test");
//            console.log(result);
//        })
//        .catch(function (err) {
//            console.error(err);
//        });
//});

    //$("#btUploadResume").click(function () {
    //    const uriUploadedResume = '/api/dashboard/uploadfile?jobId=1';

    //    fetch(uriUploadedResume)
    //        .then(function (result) {
    //            alert("test");
    //            console.log(result);
    //        })
    //        .catch(function (err) {
    //            console.error(err);
    //        });
    //});


function getResumeList() {

    const uriResumeList = `/api/Dashboard/resume-list?departId=${deptID}&vacancyId=${vacID}`
    fetch(uriResumeList)
        .then(response => response.json())
        .then(data => _displayResumeList(data))
        .catch(error => console.error('Unable to get items.', error));
}

function resumeShortlist(resumeId) {

    //const uriCheckCompatibilty = `/api/dashboard/check-resume-compatibility?jobId=${jobID}&resumeId=${resumeID}`
    //fetch(uriCheckCompatibilty)
    //    .then(response => response.json())
    //    .then(data => _displayCompatibilty(data))
    //    .catch(error => console.error('Unable to get items.', error));

    $.ajax(
        {
            //url: "/api/dashboard/uploadfile?jobId=1",
            url: `/api/dashboard/resume-shorlisting?resumeId=${resumeId}`,
            //data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function () {
                getResumeList();
                location.reload();
            }


        }
    );
}



function _displayResumeList(data) {
    const resumeList = document.getElementById('tab-1');

    var jobId = data.successData.vacancyId;
    var status = data.statusCode
    var parsedata = data.successData.resumes
    if (status == 200) {
        parsedata.forEach(item => {

            var resumeId = item.resumeID;
           
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
            divBtnFlx.classList.add('mb-3','d-flex');
            divBtnFlx.setAttribute('style', 'width: 100%');

            //divBtnFlx.classList.add('d-flex', 'mb-3');

            let btnViewDetails = document.createElement('a');
            btnViewDetails.classList.add('btn', 'btn-primary', 'ms-3');
            //let textViewDetails = document.createTextNode("Shortlist");
            //btnViewDetails.href = `javascript:resumeShortlist(${resumeId});`
            //btnViewDetails.appendChild(textViewDetails);


            let btnCheckCompInd = document.createElement('a');
            btnCheckCompInd.classList.add('btn', 'btn-primary');
            //var btnId =  resumeId;
            //btnCheckCompInd.id = btnId;
            let textCheckComp = document.createTextNode("Check Compatibility");
            btnCheckCompInd.href = `/Admin/CheckCompatibiltyIndividual/CheckCompatibiltyIndividual?jobId=${vacID}&resumeId=${resumeId}&departId=${deptID}`
          
            btnCheckCompInd.appendChild(textCheckComp);
            

            let progressMain = document.createElement('div');
            progressMain.classList.add('col-lg-8','mt-2');
            progressMain.setAttribute('style', 'height: 50%');

            let progressBar = document.createElement('div');
            progressBar.classList.add('progress-bar', 'progress-bar-striped', 'bg-success');
            progressBar.setAttribute('style', 'width: 25%');
            progressBar.setAttribute('aria-valuenow', '25');
            progressBar.setAttribute('aria-valuemin', '25');
            progressBar.setAttribute('aria-valuemax', '25');

            progressMain.appendChild(progressBar);


            if (item.isCompatibilityCheck == true) {
                progressMain.classList.add('d-block');
                btnCheckCompInd.classList.add('btn', 'btn-primary', 'd-none','col-lg-8');
                var width = 'width:' + `${item.compatibility}` + '%';
                progressBar.setAttribute('style', width);
                progressBar.textContent = item.compatibility + '%';
               
                
            }

            else {
                progressMain.classList.add('d-none');
                btnCheckCompInd.classList.add('btn', 'btn-primary', 'd-block', 'col-lg-8');
                btnViewDetails.classList.add('d-none');
            }


            if (item.isShortListed == true) {
                let textViewDetails = document.createTextNode("Shortlisted!");
                btnViewDetails.setAttribute('style', 'background-color:#85d6bb');
                btnViewDetails.href = `javascript:void(0);`
                btnViewDetails.appendChild(textViewDetails);

            }

            else {
                let textViewDetails = document.createTextNode("\n Shortlist \n");
                btnViewDetails.classList.add('px-4');               
                btnViewDetails.href = `javascript:resumeShortlist(${resumeId});`
                btnViewDetails.appendChild(textViewDetails);
          

            }
          
            txtEmail.appendChild(iconEmail);
            txtMobile.appendChild(iconMobile);
            divText.appendChild(txtCandidate);
            divText.appendChild(txtEmail);
            divText.appendChild(txtMobile);
                divBtnFlx.appendChild(progressMain);
            divCol.appendChild(icon);
            divCol.appendChild(divText);

          
            divBtnFlx.appendChild(btnCheckCompInd);
            divBtnFlx.appendChild(progressMain);
            divColBtn.appendChild(divBtnFlx);
            divBtnFlx.appendChild(btnViewDetails);
            divCardRow.appendChild(divCol);

            divCardRow.appendChild(divColBtn)

            divCardMain.appendChild(divCardRow);

            resumeList.appendChild(divCardMain);


        });
    }
}



