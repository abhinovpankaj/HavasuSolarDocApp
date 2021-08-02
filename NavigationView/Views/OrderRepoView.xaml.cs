using NavigationView.ViewModels;
using System;
using System.Linq;
using static NavigationView.Models.StateEnum;

namespace NavigationView.Views
{
    public sealed partial class OrderRepoView
    {
        public OrderRepoView()
        {
            this.InitializeComponent();
            this.DataContext = new OrderRepoViewModel();
            var _enumval = Enum.GetValues(typeof(State)).Cast<State>();
            State.ItemsSource = _enumval.ToList();
        }
    }
}
