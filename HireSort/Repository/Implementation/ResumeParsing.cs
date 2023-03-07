using HireSort.Context;
using HireSort.Entity.DbModels;
using HireSort.Helpers;
using HireSort.Models;
using HireSort.Repository.Interface;
using JacekSzybisz.FuzzyMatch;
using JacekSzybisz.FuzzyMatch.Algorithms.LevenshteinDistance;
using Microsoft.EntityFrameworkCore;
using Sovren;
using Sovren.Models.API.Parsing;

namespace HireSort.Repository.Implementation
{
    public class ResumeParsing : IResumeParsing
    {
        private readonly HRContext _dbContext;
        private int clientId = 1;

        public ResumeParsing(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponseMessage> ResumeUpload(IFormFile file, int jobId)
        {
            try
            {
                string uploads = Path.Combine("C:/Users/HUB360/Desktop/Masters/Practicum/HireSort/HireSort/Resumes/");

                var resume = new Resume();
                resume.JobId = jobId;
                resume.FileExt = System.IO.Path.GetExtension(file.FileName);
                resume.ClientId = clientId;
                _dbContext.Resumes.Add(resume);
                _dbContext.SaveChanges();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + "_" + resume.Id;
                string filePath = Path.Combine(uploads, fileName + resume.FileExt);

                resume.FileName = fileName;
                resume.File = filePath;

                _dbContext.Update(resume).State = EntityState.Modified;
                _dbContext.SaveChanges();

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                return CommonHelper.GetApiSuccessResponse("Success.");

                //return "Success.";
            }
            catch (Exception ex)
            {
                return CommonHelper.GetApiSuccessResponse(ex.Message);

                //return ex.Message.ToString();
            }
        }
        public async Task<ApiResponseMessage> resumeCheckCompatibility(int resumeId, int jobId)
        {
            try
            {
                var resume = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == jobId && w.Id == resumeId && w.IsFileParsed != true).FirstOrDefault();
                if (resume != null)
                {
                    double gpaPer = 30, insPer = 20, expPer = 25, eduPer = 10, skillPer = 15;

                    var clientHighlights = _dbContext.ClientHighlights.Where(w => w.ClientId == clientId && w.IsActive == true).ToList();
                    var gpa = clientHighlights?.Where(w => w.Category == "gpa").Select(s => s.Description).FirstOrDefault();
                    var institute = clientHighlights?.Where(w => w.Category == "institute").Select(s => s.Description).FirstOrDefault();
                    ParseResumeResponse parseResponse = await ResumeParsingHelper.ResumeParse(resume.File);
                    if (parseResponse.Info != null && parseResponse.Value != null && parseResponse != null)
                    {
                        resume.FirstName = parseResponse.EasyAccess().GetCandidateName()?.GivenName + " " + parseResponse.EasyAccess().GetCandidateName()?.MiddleName;
                        resume.LastName = parseResponse.EasyAccess().GetCandidateName()?.FamilyName ?? "";
                        resume.Email = parseResponse.EasyAccess().GetEmailAddresses()?.FirstOrDefault() ?? "";
                        resume.MobileNo = parseResponse.EasyAccess().GetPhoneNumbers()?.FirstOrDefault() ?? "";
                        resume.Cnic = parseResponse.EasyAccess().GetNationalIdentities()?.Select(s => s.Number).FirstOrDefault() ?? "";
                        resume.IsFileParsed = true;

                        double Compatibility = 0;
                        double percentage = 100;
                        var workHistory = new List<Experience>();
                        var educations = new List<Education>();
                        if (parseResponse.Value.ResumeData.Education.EducationDetails.Count > 0)
                        {
                            foreach (var edu in parseResponse.Value.ResumeData.Education.EducationDetails)
                            {
                                educations.Add(new Education()
                                {
                                    ResumeId = resumeId,
                                    InstituteName = edu.SchoolName?.Normalized ?? "",
                                    DegreeName = edu.Degree?.Name?.Normalized ?? "",
                                    Cgpa = edu.GPA?.Score.ToString() ?? "",
                                    StartDate = (edu.LastEducationDate != null) ? edu.LastEducationDate.Date : null,
                                    EndDate = (edu.LastEducationDate != null) ? edu.LastEducationDate.Date : null,
                                    CreatedOn = DateTime.Now
                                });

                                //GPA match
                                if (gpa != null && edu.Degree?.Type == "bachelors" && !String.IsNullOrEmpty(edu.GPA?.Score.ToString()))
                                {
                                    if (Convert.ToDouble(edu.GPA?.Score) >= Convert.ToDouble(gpa))
                                    {
                                        Compatibility += gpaPer;
                                    }
                                    else
                                    {
                                        Compatibility += (Convert.ToDouble(edu.GPA?.Score) / Convert.ToDouble(gpa)) * gpaPer;
                                    }
                                    percentage -= gpaPer;
                                }

                                //Initutte Name Match
                                if (institute != null && edu.Degree?.Type == "bachelors" &&
                                     ((edu.SchoolName?.Normalized.ToLower().Trim().Equals(institute.ToLower().Trim()) ?? false) || (edu.SchoolName?.Normalized.ToLower().Trim().Contains(institute.ToLower().Trim()) ?? false) || Fuzz.TokenInitialismRatio(edu.SchoolName?.Normalized, institute) > 80))
                                {
                                    Compatibility += insPer;
                                    percentage -= insPer;
                                }
                            }
                            if (percentage > 50)
                            {
                                double remainingPer = (percentage - 50) / 3;
                                eduPer += remainingPer;
                                expPer += remainingPer;
                                skillPer += remainingPer;
                            }
                            if (educations.Any(w => w.DegreeName.Equals("computer science") || w.DegreeName.Contains("computer science") || Fuzz.TokenInitialismRatio(w.DegreeName, "computer science") > 80))
                            {
                                Compatibility += eduPer;
                                percentage -= eduPer;
                            }

                            _dbContext.Educations.AddRange(educations);
                            _dbContext.SaveChanges();
                        }
                        if (parseResponse.Value.ResumeData.EmploymentHistory.Positions.Count > 0)
                        {
                            foreach (var job in parseResponse.Value.ResumeData.EmploymentHistory.Positions)
                            {
                                //if(job.StartDate!=null || job.EndDate != null)
                                //years  
                                int Years = job.EndDate.Date.Year - job.StartDate.Date.Year;
                                int month = job.EndDate.Date.Month - job.StartDate.Date.Month;
                                int TotalMonths = (Years * 12) + month;

                                workHistory.Add(new Experience()
                                {
                                    ResumeId = resumeId,
                                    CompanyName = job.Employer?.Name?.Normalized ?? "",
                                    Responsibility = job.Description,
                                    Designation = job.JobTitle?.Normalized ?? "",
                                    TotalExperience = TotalMonths,
                                    StartDate = job.StartDate.Date,
                                    EndDate = job.EndDate.Date,
                                    CreatedOn = DateTime.Now
                                });
                            }
                            int totalExp = workHistory.Sum(s => s.TotalExperience);
                            if (totalExp >= expPer)
                            {
                                Compatibility += percentage;
                            }
                            else
                            {
                                Compatibility += (totalExp / expPer) * percentage;
                            }
                            _dbContext.Experiences.AddRange(workHistory);
                            _dbContext.SaveChanges();
                        }
                        resume.Compatibility = Math.Round(Compatibility, 2).ToString();
                        resume.IsCompatibility = true;
                        _dbContext.Update(resume).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                    return CommonHelper.GetApiSuccessResponse("Success.");
                    //return "Success.";
                }
                return CommonHelper.GetApiSuccessResponse("File Not Found.", 400);

                //return "File Not Found.";

            }
            catch (Exception ex)
            {
                return CommonHelper.GetApiSuccessResponse(ex.Message);

                //return ex.Message;
            }
        }

        public async Task<ApiResponseMessage> ResumeCalculateCompatibility(int resumeId, int jobId)
        {
            string res = String.Empty;
            var resume = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == jobId && w.Id == resumeId && w.IsFileParsed != true).FirstOrDefault();

            var clientHighlights = _dbContext.ClientHighlights.Where(w => w.ClientId == clientId && w.IsActive == true).FirstOrDefault();


            //#region FuzzySharp

            //var a1 = Fuzz.Ratio("mysmilarstring", "myawfullysimilarstirng");
            //res += "a1: " + a1+"\n " ;
            //var a2 = Fuzz.Ratio("mysimilarstring", "mysimilarstring");
            //res += "a2: " + a2 + "\n ";

            //var b1 = Fuzz.PartialRatio("similar", "somewhresimlrbetweenthisstring");
            //res += "b1: " + b1 + "/n ";
            //var c1 = Fuzz.TokenSortRatio("order words out of", "  words out of order");
            //res += "c1: " + c1 + "/n ";
            //var c2 = Fuzz.PartialTokenSortRatio("order words out of", "  words out of order");
            //res += "c2: " + c2 + "/n ";
            //var d1 = Fuzz.TokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear");
            //res += "d1: " + d1 + "/n "; 
            //var d2 = Fuzz.PartialTokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear");
            //res += "d2: " + d2 + "/n ";
            //var e1 = Fuzz.WeightedRatio("The quick brown fox jimps ofver the small lazy dog", "the quick brown fox jumps over the small lazy dog");
            //res += "a1e1 " + e1 + "\n ";
            //var f1 = Fuzz.TokenInitialismRatio("NASA", "National Aeronautics and Space Administration");
            //res += "f1: " + f1 + "\n "; 
            //var f2 = Fuzz.TokenInitialismRatio("NASA", "National Aeronautics Space Administration");
            //res += "f2: " + f2 + "\n ";
            //var f3 = Fuzz.TokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899");
            //res += "f3: " + f3 + "\n "; 
            //var f4 = Fuzz.PartialTokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899");
            //res += "f4: " + f4 + "\n ";
            //var g1 = Fuzz.TokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full);
            //res += "g1: " + g1 + "\n "; 
            //var g2 = Fuzz.PartialTokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full);
            //res += "g2: " + g2 + "\n ";
            //var r = Fuzz.Equals("mysimilarstring", "mysimilarstring");
            //res += "r: " + r + "\n ";
            //#endregion


            #region IFuzzyMatchProvider

            IFuzzyMatchProvider fuzzyMatchProvider = new FuzzyMatchProvider(new LevenshteinDistanceService());

            var isMatch = fuzzyMatchProvider.IsMatch("Visual Studio 2013", "Visual Studio 2024", 1);
            var isMatch1 = fuzzyMatchProvider.IsMatch(".Net Core", ".Net Core", 1);

            res += "      ";
            res += isMatch;
            res += isMatch1;
            Console.WriteLine(isMatch);

            if (isMatch)
            {
                Console.WriteLine("Honda and Hyundai is similar enough to be considered as match");
            }

            #endregion

            //#region
            //string source = ".NET, C#, LINQ, SharePoint 2013, XMLQuery, SQL Server 2017, HTML, Bootstrap 4, JavaScript, jQuery, Visual Studio 2016";
            //string target = " At least 2 years of software development experience Familiarity with the ASP.NET framework, SQL Server and design / architectural patterns Example: Model - View - Controller(MVC)] Knowledge of at least one of the.NET languages Example: C#, Visual Basic .NET) and HTML5/CSS3 Familiarity with architecture styles / APIs(REST, RPC) Understanding of Agile methodologies";
            //var options =
            //            StringComparisonOption.UseSorensenDiceDistance;
            //res += "String Comparison: %" + source.Similarity(target, options) * 100 + Environment.NewLine;
            //res += "Strong compare: " + source.IsSimilar(target, StringComparisonTolerance.Strong, options) + Environment.NewLine;
            //res += "Normal compare: " + source.IsSimilar(target, StringComparisonTolerance.Normal, options) + Environment.NewLine;
            //res += "Weak compare: " + source.IsSimilar(target, StringComparisonTolerance.Weak, options) + Environment.NewLine;
            //res += "RatcliffObershelpSimilarity compare: " + source.RatcliffObershelpSimilarity(target) + Environment.NewLine;
            //#endregion

            //var diffBuilder = new InlineDiffBuilder(new Differ());
            //var diff = diffBuilder.BuildDiffModel(source, target);
            //res += diff.Lines.Select(s=>s.);

            #region Commented Sovren code
            //    SovrenClient client = new SovrenClient("23823569", "6UyMxC3LMYFjnpYQ5azPSta4ZLJmk/SQysBNjrI8", DataCenter.US);
            //    Document doc = new Document(resume.File);
            //    Document doc1 = new Document("C:/Users/HUB360/Desktop/Masters/Practicum/HireSort/HireSort/Resumes/Job.pdf");
            //    ParseRequest request = new ParseRequest(doc, new ParseOptions());
            //    ParseRequest request1 = new ParseRequest(doc1, new ParseOptions());
            //    //ParseResumeResponse parseResponse = await ResumeParsingHelper.ResumeParse(resume.File);
            //    //var client1 = new ResumeParsingClient(accountId, serviceKey, endpoint);
            //    var helper = new ResumeParsingClient("23823569", "6UyMxC3LMYFjnpYQ5azPSta4ZLJmk/SQysBNjrI8", "https://rest.resumeparsing.com");
            //    ParsedJobWithId sourceJob = new ParsedJobWithId
            //    {
            //        Id = helper.CreateNormalizedSkillIds(client.ParseJob(request1).Result.Value.JobData),
            //        JobData = client.ParseJob(request1).Result.Value.JobData
            //    };

            //    List<ParsedResumeWithId> targetResumes = new List<ParsedResumeWithId>
            //            {
            //new ParsedResumeWithId
            //{
            //    Id ="1058",
            //    ResumeData =  ResumeParsingHelper.ResumeParse(resume.File??"").Result.Value.ResumeData
            //}
            //    };

            //    try
            //    {
            //        BimetricScoreResponse response = await client.BimetricScore(sourceJob, targetResumes);
            //        string res = "";
            //        foreach (BimetricScoreResult match in response.Value.Matches)
            //        {
            //            res += match.Id + " " + match.SovScore + "\n";
            //        }
            //    }
            //    catch (SovrenException e)
            //    {
            //        //this was an outright failure, always try/catch for SovrenExceptions when using SovrenClient
            //        Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
            //        return CommonHelper.GetApiSuccessResponse(e.Message);

            //    }
            return CommonHelper.GetApiSuccessResponse(res);

            #endregion
        }
        //public async Task<string> resumeGetCompatibility(int resumeId, int jobId)
        //{

        //    try
        //    {
        //        var resumeCompatibility = new 
        //        var resume = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == jobId && w.Id == resumeId && w.IsFileParsed == true).FirstOrDefault();
        //        if (resume != null)
        //        {

        //            return "Success.";
        //        }
        //        return "File Not Found.";

        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }

}
