using MVC.Data;

namespace MVC.Tasks
{
    public interface ITask
    {
        QrCoreContext Context { get; set; }
    }
}