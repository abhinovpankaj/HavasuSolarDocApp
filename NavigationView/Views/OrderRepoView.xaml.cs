using NavigationView.ViewModels;

namespace NavigationView.Views
{
    public sealed partial class OrderRepoView
    {
        public OrderRepoView()
        {
            this.InitializeComponent();
            this.DataContext = new OrderRepoViewModel();
        }
    }
}
