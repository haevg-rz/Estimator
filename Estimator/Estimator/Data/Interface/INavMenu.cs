namespace Estimator.Data.Interface
{
    public interface INavMenu
    {
        bool collapseNavMenu { get; set; }
        void ToggleNavMenu();
    }
}