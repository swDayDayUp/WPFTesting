using SEOTesting.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SEOTesting
{
    public class SEOSummary : BindableBase
    {
        //Property Definitions : for future expantion, these properties can be read from a system configuration file : for example , a confiruration XML file

        //UI Related Definitions
        private string _TargetURL;// = "https://www.google.com.au/search?num=100&q=conveyancing+software";
        public string TargetURL
        {
            get { return _TargetURL; }
            set { this.SetProperty(ref _TargetURL, value); }
        }

        private string _SEOResultDisplay = string.Empty; //default output if no match
        public string SEOResultDisplay
        {
            get { return _SEOResultDisplay; }
            set { this.SetProperty(ref _SEOResultDisplay, value); }
        }

        //Search Result Extraction Definitions
        private string _TargetHTMLRegexPattern; // = @"(?i)<cite[^>]*>(.*)</cite>";
        public string TargetHTMLRegexPattern
        {
            get { return _TargetHTMLRegexPattern; }
            set { this.SetProperty(ref _TargetHTMLRegexPattern, value); }
        }

        private string _TargetKeyWordRegexPattern; // = "smokeball.com.au";
        public string TargetKeyWordRegexPattern
        {
            get { return _TargetKeyWordRegexPattern; }
            set { this.SetProperty(ref _TargetKeyWordRegexPattern, value); }
        }

        //hold all smokeball match for further process and final display
        private List<string> _SmokeballRankList;

        //get from target web site
        public string ResponseText { get; set; }

        

        public SEOSummary(List<string> smokeballRankList)
        {
            _SmokeballRankList = smokeballRankList;
        }

        public SEOSummary(string targetURL, string targetHTMLRegexPattern, string targetKeyWordRegexPattern)
        {
            TargetURL = targetURL;
            TargetHTMLRegexPattern = targetHTMLRegexPattern;
            TargetKeyWordRegexPattern = targetKeyWordRegexPattern;
            _SmokeballRankList = new List<string>();
        }


        private async Task<string> GetResponseText()
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(TargetURL);
            }
        }

        public override string ToString()
        {
            string result = string.Empty;
            if (_SmokeballRankList.Count == 0)
            {
                return "0";
            }

            result = string.Join(",", _SmokeballRankList);            
            return result;
        }

        public void FindAllMatches()
        {
            //this will be the ranking number
            int citeIndex = 0;

            //this is the target string to look at in the above matched cite text
            Regex RegExp = new Regex(TargetKeyWordRegexPattern);

            // for each cite extracted, look into it and see if we can find the target "smokeball"  match
            //if yes, store the info for further process
            foreach (Match citeMatch in Regex.Matches(ResponseText, TargetHTMLRegexPattern))
            {
                citeIndex++;
                Match smokeballMatch = RegExp.Match(citeMatch.Value);
                if (smokeballMatch.Success)
                {
                    _SmokeballRankList.Add(citeIndex.ToString());
                }
            }

        }


        public async void ProcessSmokeBallSEO()
        {
            SEOResultDisplay = string.Empty; //reset

            ResponseText = await GetResponseText();

            FindAllMatches();

            SEOResultDisplay = this.ToString();
        }
    }
}
