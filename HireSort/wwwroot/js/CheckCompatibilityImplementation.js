

var jobID;
var resumeID;
/*var departID;*/
$(document).ready(function () {
    //var queryString = window.location.href;
    ////window.location.href.slice(window.location.href.indexOf('?') + 1);
    //const searchParams = queryString.searchParams;
    //var deptID = searchParams.get('departId');
    //var vacID = searchParams.get('vacancyId');
    const params = new URLSearchParams(window.location.search);

    jobID = params.get('jobId');
    resumeID = params.get('resumeId');
    departID = params.get('departId');

    getResumeInd();
    getCompatibilty();

});




function resumeShortlist(resumeId) {

    //const uriCheckCompatibilty = `/api/dashboard/check-resume-compatibility?jobId=${jobID}&resumeId=${resumeID}`
    //fetch(uriCheckCompatibilty)
    //    .then(response => response.json())
    //    .then(data => _displayCompatibilty(data))
    //    .catch(error => console.error('Unable to get items.', error));

    $.ajax(
        {
            //url: "/api/dashboard/uploadfile?jobId=1",
            url: `/api/dashboard/resume-shorlisting?resumeId=${resumeID}`,
            //data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function () {
                
                getCompatibilty();
                location.reload();
            }


        }
    );
}

function getCompatibilty() {

    //const uriCheckCompatibilty = `/api/dashboard/check-resume-compatibility?jobId=${jobID}&resumeId=${resumeID}`
    //fetch(uriCheckCompatibilty)
    //    .then(response => response.json())
    //    .then(data => _displayCompatibilty(data))
    //    .catch(error => console.error('Unable to get items.', error));

    $.ajax(
        {
            //url: "/api/dashboard/uploadfile?jobId=1",
            url: `/api/dashboard/check-resume-compatibility?jobId=${jobID}&resumeId=${resumeID}`,
            //data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function () {
                getResumeInd();
                //alert("Files Uploaded!");
            }


        }
    );
}
function getResumeInd() {

    const uriResumeList = `/api/dashboard/resume-compatibitlity?resumeId=${resumeID}&jobId=${jobID}`
    fetch(uriResumeList)
        .then(response => response.json())
        .then(data => _displayResumeInd(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayResumeInd(data) {
    const resumeProfile = document.getElementById('candidateProfile');
    const progressBarInd = document.getElementById('progressBarInd');

    const divCGPAandIns = document.getElementById('divCGPAandIns');
    const resumeExp = document.getElementById('experience');
    const resumeEdu = document.getElementById('education');
    const resumeSkills = document.getElementById('skills');
    const resumeLinks = document.getElementById('links');

  
    var status = data.statusCode
    var parsedata = data.successData
   
    if (status == 200) {
        parsedata.forEach(item => {

            let txtCandidate = document.createElement('h5');
            txtCandidate.classList.add('card-title');
            txtCandidate.textContent = item.candidateName;

            let divCardEmailNo= document.createElement('div');
            divCardEmailNo.classList.add('ps-4');


            let txtEmail = document.createElement('span');
            txtEmail.classList.add('text-truncate', 'me-3');
            txtEmail.textContent = item.email;

            let iconEmail = document.createElement('i');
            iconEmail.classList.add('fa', 'fa-envelope', 'text-primary', 'me-2');

            let txtMobile = document.createElement('span');
            txtMobile.classList.add('text-truncate', 'me-3');
            txtMobile.textContent = item.mobieNo;

            let iconMobile = document.createElement('i');
            iconMobile.classList.add('fa', 'fa-mobile', 'text-primary', 'me-2');

            let btnShortlist = document.createElement('a');
            btnShortlist.classList.add('btn', 'btn-primary', 'ms-3');
            let textShortlist = document.createTextNode("Shortlist");
            btnShortlist.href = `javascript:resumeShortlist(${resumeID});`

            btnShortlist.appendChild(textShortlist);

            txtEmail.appendChild(iconEmail);
            txtMobile.appendChild(iconMobile);
            divCardEmailNo.appendChild(txtEmail);
            divCardEmailNo.appendChild(txtMobile);

            resumeProfile.appendChild(divCardEmailNo);
            resumeProfile.appendChild(txtCandidate);
            resumeProfile.appendChild(btnShortlist)

            let progressMain = document.createElement('div');
            progressMain.setAttribute('style', 'height: 5%');

            var width = 'width: ' + `${item.compatiblePercentage}` + '%';
            let progressBar = document.createElement('div');
            progressBar.classList.add('progress-bar', 'progress-bar-striped', 'bg-success');
            progressBar.textContent = item.compatiblePercentage + '%';
            
            progressBar.setAttribute('style',width);
            progressBar.setAttribute('aria-valuenow', '25');
            progressBar.setAttribute('aria-valuemin', '25');
            progressBar.setAttribute('aria-valuemax', '25');

            progressMain.appendChild(progressBar);

            progressBarInd.appendChild(progressMain);

            let textGPA = document.createElement('p');
            textGPA.textContent = "CGPA: \n" + item.gpa;
            let textIns = document.createElement('p');
            textIns.textContent = "Institute Match: \n" + item.instituteMatch;

            divCGPAandIns.appendChild(textGPA)
            divCGPAandIns.appendChild(textIns)

            var parsedataExp = item.experience
            var parsedataEdu = item.educations

            var parsedataSkills = item.skills
            var parsedataLinks = item.links

            //Experience
            parsedataExp.forEach(item => {



                let txtCompany = document.createElement('p');
                txtCompany.textContent = "Company Name:\n" + item.companyName

                let txtDesignation = document.createElement('p');
                txtDesignation.textContent = "Designation:\n" + item.designation

                let txtResp = document.createElement('p');
                txtResp.textContent = "Responsibility:\n" + item.responsiblility

                let txtSDate = document.createElement('p');
                txtSDate.textContent = "Start Date:\n" + item.startDate

                let txtEdate = document.createElement('p');
                txtEdate.textContent = "End Date:\n" + item.endDate

                let txtExp = document.createElement('p');
                txtExp.textContent = "Experience:\n" + item.totalExperience + "\nMonths"

                let txtHR = document.createElement('hr');

                resumeExp.appendChild(txtCompany);
                resumeExp.appendChild(txtDesignation);
                resumeExp.appendChild(txtResp);
                resumeExp.appendChild(txtSDate);
                resumeExp.appendChild(txtEdate);
                resumeExp.appendChild(txtExp);
                resumeExp.appendChild(txtHR);


            });

            //Education
            parsedataEdu.forEach(item => {



                let txtInstitute = document.createElement('p');
                txtInstitute.textContent = "Institute Name:\n" + item.instituteName

                let txtDegree = document.createElement('p');
                txtDegree.textContent = "Degree:\n" + item.degreeName

                let txtCgpa = document.createElement('p');
                txtCgpa.textContent = "CGPA:\n" + item.cgpa

                let txtSDate = document.createElement('p');
                txtSDate.textContent = "Start Date:\n" + item.startDate

                let txtEdate = document.createElement('p');
                txtEdate.textContent = "End Date:\n" + item.endDate


                let txtHR = document.createElement('hr');

                resumeEdu.appendChild(txtInstitute);
                resumeEdu.appendChild(txtDegree);
                resumeEdu.appendChild(txtCgpa);
                resumeEdu.appendChild(txtSDate);
                resumeEdu.appendChild(txtEdate);
                resumeEdu.appendChild(txtHR);


            });

            //Skills
            parsedataSkills.forEach(item => {

                let txtSkillName = document.createElement('p');
                txtSkillName.textContent = "Skill Name:\n" + item.skillName
                let txtHR = document.createElement('hr');
                resumeSkills.appendChild(txtSkillName);
                resumeSkills.appendChild(txtHR);


            });

            //Links
            parsedataLinks.forEach(item => {

                let txtLinkName = document.createElement('a');
                txtLinkName.textContent = "LinkedIn"; 
                txtLinkName.href = item.link;
                txtLinkName.target = "_blank";
                let txtHR = document.createElement('hr');
                resumeLinks.appendChild(txtLinkName);
                resumeLinks.appendChild(txtHR);


            });

        });


       
    }
}
//function _displayCompatibilty(data) {

//    alert("testing");

//    //const vacancyList = document.getElementById('tab-1');
//    var status = data.statusCode
//    var error = data.error
//    if (status == 200) {

//        const uriResumeList = `/api/Dashboard/resume-list?departId=${deptID}&vacancyId=${vacID}`
//        fetch(uriResumeList)
//            .then(response => response.json())
//            .then(data => _displayResumeList(data))
//            .catch(error => console.error('Unable to get items.', error));



//    }
//    else {
//        alert("File Not Found");
//    }
//}

    //var status = data.statusCode
    //var parsedata = data.successData
    //if (status == 200) {
    //    parsedata.forEach(item => {


    //        let divCardMain = document.createElement('div');
    //        divCardMain.classList.add('job-item', 'p-4', 'mb-4');

    //        let divCardRow = document.createElement('div');
    //        divCardRow.classList.add('row', 'g-4');

    //        let divCol = document.createElement('div');
    //        divCol.classList.add('col-sm-12', 'col-md-8', 'd-flex', 'align-items-center');

    //        let icon = document.createElement('i');
    //        icon.classList.add('fa', 'fa-3x', 'fa-user', 'text-primary', 'mb-4');

    //        let divText = document.createElement('div');
    //        divText.classList.add('text-start', 'ps-4');

    //        let txtCandidate = document.createElement('h5');
    //        txtCandidate.classList.add('mb-3');
    //        txtCandidate.textContent = item.jobName;

    //        let txtEmail = document.createElement('span');
    //        txtEmail.classList.add('text-truncate', 'me-3');
    //        txtEmail.textContent = item.jobStartDate;

    //        let iconEmail = document.createElement('i');
    //        iconEmail.classList.add('far','fa-calendar-alt' ,'text-primary', 'me-2');

    //        let txtMobile = document.createElement('span');
    //        txtMobile.classList.add('text-truncate', 'me-3');
    //        txtMobile.textContent = item.jobEndDate;

    //        let iconMobile = document.createElement('i');
    //        iconMobile.classList.add('far', 'fa-calendar-alt', 'text-primary', 'me-2');

    //        let divColBtn = document.createElement('div');
    //        divColBtn.classList.add('col-sm-12', 'col-md-4', 'd-flex', 'flex-column', 'align-items-start', 'align-items-md-end', 'justify-content-center');

    //        let divBtnFlx = document.createElement('div');
    //        divBtnFlx.classList.add('d-flex', 'mb-3');


    //        let btnViewDetails = document.createElement('a');
    //        btnViewDetails.classList.add('btn', 'btn-primary', 'me-3');
    //        let textViewDetails = document.createTextNode("View");
    //        btnViewDetails.href = '/ViewJobDetail/ViewJobDetail'
    //        btnViewDetails.appendChild(textViewDetails);



    //        txtEmail.appendChild(iconEmail);
    //        txtMobile.appendChild(iconMobile);
    //        divText.appendChild(txtCandidate);
    //        divText.appendChild(txtEmail);
    //        divText.appendChild(txtMobile);

    //        divCol.appendChild(icon);
    //        divCol.appendChild(divText);

    //        divBtnFlx.appendChild(btnViewDetails);


    //        divColBtn.appendChild(divBtnFlx);

    //        divCardRow.appendChild(divCol);

    //        divCardRow.appendChild(divColBtn)

    //        divCardMain.appendChild(divCardRow);

    //        vacancyList.appendChild(divCardMain);


    //    });
    //}

//}


//function getResumeList() {

//    const uriResumeList = `/api/Dashboard/resume-list?departId=${deptID}&vacancyId=${vacID}`
//    fetch(uriResumeList)
//        .then(response => response.json())
//        .then(data => _displayResumeList(data))
//        .catch(error => console.error('Unable to get items.', error));
//}

//function _displayResumeList(data) {
//    const resumeList = document.getElementById('tab-1');

//    var jobId = data.successData.vacancyId;
//    var status = data.statusCode
//    var parsedata = data.successData.resumes
//    if (status == 200) {
//        parsedata.forEach(item => {

//            var resumeId = item.resumeID;

//            let divCardMain = document.createElement('div');
//            divCardMain.classList.add('job-item', 'p-4', 'mb-4');

//            let divCardRow = document.createElement('div');
//            divCardRow.classList.add('row', 'g-4');

//            let divCol = document.createElement('div');
//            divCol.classList.add('col-sm-12', 'col-md-8', 'd-flex', 'align-items-center');

//            let icon = document.createElement('i');
//            icon.classList.add('fa', 'fa-3x', 'fa-user', 'text-primary', 'mb-4');

//            let divText = document.createElement('div');
//            divText.classList.add('text-start', 'ps-4');



//            let txtCandidate = document.createElement('h5');
//            txtCandidate.classList.add('mb-3');
//            txtCandidate.textContent = item.candidateName;

//            let txtEmail = document.createElement('span');
//            txtEmail.classList.add('text-truncate', 'me-3');
//            txtEmail.textContent = item.emailAddress;

//            let iconEmail = document.createElement('i');
//            iconEmail.classList.add('fa', 'fa-envelope', 'text-primary', 'me-2');

//            let txtMobile = document.createElement('span');
//            txtMobile.classList.add('text-truncate', 'me-3');
//            txtMobile.textContent = item.mobileNo;

//            let iconMobile = document.createElement('i');
//            iconMobile.classList.add('fa', 'fa-mobile', 'text-primary', 'me-2');

//            let divColBtn = document.createElement('div');
//            divColBtn.classList.add('col-sm-12', 'col-md-4', 'd-flex', 'flex-column', 'align-items-start', 'align-items-md-end', 'justify-content-center');

//            let divBtnFlx = document.createElement('div');
//            divBtnFlx.classList.add('d-flex', 'mb-3');





//            let btnViewDetails = document.createElement('a');
//            btnViewDetails.classList.add('btn', 'btn-primary', 'me-3');
//            let textViewDetails = document.createTextNode("View Details");
//            btnViewDetails.href = '/ViewJobDetail/ViewJobDetail'
//            btnViewDetails.appendChild(textViewDetails);


//            let btnCheckCompInd = document.createElement('a');
//            btnCheckCompInd.classList.add('btn', 'btn-primary');
//            let textCheckComp = document.createTextNode("Check Compatibility");
//            btnCheckCompInd.href = `/CheckCompatibiltyIndividual/CheckCompatibiltyIndividual?jobId=${jobId}&resumeId=${resumeId}`
//            btnCheckCompInd.appendChild(textCheckComp);



//            txtEmail.appendChild(iconEmail);
//            txtMobile.appendChild(iconMobile);
//            divText.appendChild(txtCandidate);
//            divText.appendChild(txtEmail);
//            divText.appendChild(txtMobile);

//            divCol.appendChild(icon);
//            divCol.appendChild(divText);

//            divBtnFlx.appendChild(btnViewDetails);
//            divBtnFlx.appendChild(btnCheckCompInd);

//            divColBtn.appendChild(divBtnFlx);

//            divCardRow.appendChild(divCol);

//            divCardRow.appendChild(divColBtn)

//            divCardMain.appendChild(divCardRow);

//            resumeList.appendChild(divCardMain);


//        });
//    }
//}