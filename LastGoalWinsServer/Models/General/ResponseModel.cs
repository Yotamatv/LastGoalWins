namespace LastGoalWinsServer.Models.General
{
    public class ResponseModel<T>
    {
        public string Get { get; set; }
        public Parameters Parameters { get; set; }
        public List<string> Errors { get; set; }
        public int Results { get; set; }
        public Paging Paging { get; set; }
        public List<T>? Response { get; set; }

        public ResponseModel()
        {
            Get = "";
            Parameters = new Parameters("", "", "", "","","");
            Errors = new List<string>();
            Results = 0;
            Paging = new Paging(0, 0);
            Response = new List<T>();
        }

        public ResponseModel(string get, Parameters parameters, List<string> errors, int results, Paging paging, List<T> response)
        {
            Get = get;
            Parameters = parameters;
            Errors = errors;
            Results = results;
            Paging = paging;
            Response = response;
        }

        public ResponseModel(List<string> errors)
        {
            Get = "";
            Parameters = new Parameters("", "", "", "", "", "");
            Errors = errors;
            Results = 0;
            Paging = new Paging(0, 0);
            Response = new List<T>();
        }
    }


}
//https://rapidapi.com/api-sports/api/api-football/