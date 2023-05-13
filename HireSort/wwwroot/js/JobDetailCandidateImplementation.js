


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

    getJobDetails();
    //resumeShortlist();
});



//Apply Now Functionality
function applyNow(fname, lname, email, files, coverLetter) {
    var firstname = document.getElementById(fname).value;
    var lastname = document.getElementById(lname).value;
    var emailAdd = document.getElementById(email).value;
    var cover = document.getElementById(coverLetter).value;
    var fileName = document.getElementById(files);
    var files = fileName.files;

    var formData = new FormData();
    formData.append("files", files[0]);

    $.ajax(
        {
            //url: "/api/dashboard/uploadfile?jobId=1",
            url: `/api/dashboard/apply-now?jobId=${vacID}&firstName="${firstname}"&lastName="${lastname}"&email="${emailAdd}"&coverletter="${cover}"`,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
               // location.reload();
                //getResumeList();
                alert("Files Uploaded!");
            }
        }
    );
}





function getJobDetails() {

    const uriResumeList = `/api/dashboard/job-detail?departId=${deptID}&jobId=${vacID}`
    fetch(uriResumeList)
        .then(response => response.json())
        .then(data => _displayJobDetails(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayJobDetails(data) {
    const jobdetailist = document.getElementById('jobDesc');
    const jobTenureList = document.getElementById('jobSummary');
    const jobResponsibility = document.getElementById('jobResponsibility');
    const jobQualification = document.getElementById('jobQualification');


    var status = data.statusCode
    var parsedata = data.successData
    if (status == 200) {
        parsedata.forEach(item => {

         
        //Job Desc
            let jobDescription = document.createElement('h3');
            jobDescription.classList.add('mb-3');
            jobDescription.textContent = item.jobName;
            jobdetailist.appendChild(jobDescription);

            //Start Date
            let startDate = document.createElement('p');
            let iconArr1 = document.createElement('i');
            iconArr1.classList.add('fa', 'fa-angle-right', 'text-primary', 'me-2');

            startDate.appendChild(iconArr1);
            startDate.textContent = "Start Date:\n" +item.jobStartDate;
            jobTenureList.appendChild(startDate);

            //END Date
            let endDate = document.createElement('p');
            let iconArr2 = document.createElement('i');
            iconArr2.classList.add('fa', 'fa-angle-right', 'text-primary', 'me-2');

            endDate.appendChild(iconArr2);
            endDate.textContent = "End Date:\n" + item.jobEndDate;
            jobTenureList.appendChild(endDate);


            //job Type
            let jobType = document.createElement('p');
            let iconArr3 = document.createElement('i');
            iconArr3.classList.add('fa', 'fa-angle-right', 'text-primary', 'me-2');

            jobType.appendChild(iconArr3);
            jobType.textContent = "Job Type:\n" + item.jobType;
            jobTenureList.appendChild(jobType);


            //Job shift
            let jobShift = document.createElement('p');
            let iconArr4 = document.createElement('i');
            iconArr4.classList.add('fa', 'fa-angle-right', 'text-primary', 'me-2');

            jobShift.appendChild(iconArr4);
            jobShift.textContent = "Job Shift:\n" + item.jobShift;
            jobTenureList.appendChild(jobShift);

            //Job Description in Detail
            var parsedataJobDesc = item.jobDesc
          
            parsedataJobDesc.forEach(item => {
                //Job Responsibility

                let responsibility = document.createElement('h3');
                responsibility.classList.add('mb-3');
                responsibility.textContent = item.jobCode;
                jobResponsibility.appendChild(responsibility);

                let reponsibilityDetail = document.createElement('p');
                reponsibilityDetail.textContent = item.description;

                jobResponsibility.appendChild(reponsibilityDetail);

            });


        });
    }
}



