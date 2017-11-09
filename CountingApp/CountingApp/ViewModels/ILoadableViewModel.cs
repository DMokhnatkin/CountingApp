using System.Threading.Tasks;

namespace CountingApp.ViewModels
{
    /// <summary>
    /// View model which has Load method
    /// </summary>
    public interface ILoadableViewModel
    {
        Task Load();
    }
}
