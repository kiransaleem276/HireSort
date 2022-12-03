
using Sovren;
using Sovren.Models;
using Sovren.Models.API.Parsing;
using Sovren.Models.Resume.ContactInfo;

namespace HireSort.Helpers
{
    public class ResumeParsingHelper
    {
        public async static Task<ParseResumeResponse> ResumeParse()
        {
            SovrenClient client = new SovrenClient("20835745", "yFiS3NpVVHhDgxOBYcVKN0oaL3INlIu3L2lwrPwa", DataCenter.US);

            //A Document is an unparsed File (PDF, Word Doc, etc)
            Document doc = new Document("resume.pdf");

            //when you create a ParseRequest, you can specify many configuration settings
            //in the ParseOptions. See https://sovren.com/technical-specs/latest/rest-api/resume-parser/api/
            ParseRequest request = new ParseRequest(doc, new ParseOptions());
            try
            {
                ParseResumeResponse response = await client.ParseResume(request);
                //if we get here, it was 200-OK and all operations succeeded

                //now we can use the response from Sovren to output some of the data from the resume
                return response;
                //return PrintBasicResumeInfo(response);
            }
            catch (SovrenException e)
            {
                //the document could not be parsed, always try/catch for SovrenExceptions when using SovrenClient
                Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
            }
            return null;
        }

        private static string PrintBasicResumeInfo(ParseResumeResponse response)
        {
            return PrintContactInfo(response);
            //PrintPersonalInfo(response);
            //PrintWorkHistory(response.Value);
            //PrintEducation(response.Value);
        }

        private static string PrintContactInfo(ParseResumeResponse response)
        {
            string contactInfo = string.Empty;
            //general contact information (only some examples listed here, there are many others)
            //PrintHeader("CONTACT INFORMATION");
            contactInfo += "Name: " + response.EasyAccess().GetCandidateName()?.FormattedName;
            contactInfo += "Email: " + response.EasyAccess().GetEmailAddresses()?.FirstOrDefault();
            contactInfo += "Phone: " + response.EasyAccess().GetPhoneNumbers()?.FirstOrDefault();
            contactInfo += "Location: " + response.EasyAccess().GetContactInfo()?.Location?.Municipality;
            contactInfo += "Region: " + response.EasyAccess().GetContactInfo()?.Location?.Regions?.FirstOrDefault();
            contactInfo += "Country: " + response.EasyAccess().GetContactInfo()?.Location?.CountryCode;
            contactInfo += "LinkedIn: " + response.EasyAccess().GetWebAddress(WebAddressType.LinkedIn);
            return contactInfo;
        }
    }
}
