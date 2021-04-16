using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SwitchPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IActiveAware
    {
        public DelegateCommand<object> UpdateCommand { get; set; }
        public ObservableCollection<SwitchStatusModel> SwitchStatus { get; } = new ObservableCollection<SwitchStatusModel>();

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            UpdateCommand = new DelegateCommand<object>(UpdateCommandHandler);

        }

        public event EventHandler IsActiveChanged;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }

        private async void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive)
            {
                SwitchStatus.Clear();
                GetSwitchStatus();
            }
        }

        private void GetSwitchStatus()
        {
            for (int i = 0; i < 5; i++)
                SwitchStatus.Add(new SwitchStatusModel { State = i % 2 == 0 ? true : false });

        }

        public async void UpdateCommandHandler(object obj)
        {
            // Todo should restrict this event fire when navigate to this page.
            // Its firing because of IsToggled property set.
        }
    }

    public class SwitchStatusModel
    {
        public bool State { get; set; }
    }
}
