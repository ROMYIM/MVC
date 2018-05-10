namespace MVC.Models
{
    public class Result
    {
        public int Judgement { get; set; }

        public Result(int code)
        {
            Judgement = code;
        }
    }
}