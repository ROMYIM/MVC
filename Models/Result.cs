namespace MVC.Models
{
    public class Result
    {
        public string Judgement { get; set; }

        public Result(int code)
        {

            Judgement = code.ToString();
        }


    }
}