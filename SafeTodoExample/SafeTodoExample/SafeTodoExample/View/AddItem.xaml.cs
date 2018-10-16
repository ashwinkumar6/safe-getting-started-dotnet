﻿using Rg.Plugins.Popup.Extensions;
using SafeTodoExample.Helpers;
using SafeTodoExample.Model;
using SafeTodoExample.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafeTodoExample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItem : Rg.Plugins.Popup.Pages.PopupPage
    {
        private AddItemViewModel viewModel;
        private TodoItem _item;
        private bool _edit = false;
        public AddItem()
        {
            InitializeComponent();
        }

        public AddItem(TodoItem item)
        {
            InitializeComponent();
            _edit = true;
            _item = item;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel == null)
            {
                if (_edit)
                {
                    viewModel = new AddItemViewModel(_item, _edit);
                }
                else
                {
                    viewModel = new AddItemViewModel();
                }
            }

            BindingContext = viewModel;
            MessageCenterSubscribe();
        }

        public void MessageCenterUnsubscribe()
        {
            MessagingCenter.Unsubscribe<AddItemViewModel>(this, MessengerConstants.HidePopUp);
        }

        public void MessageCenterSubscribe()
        {
            MessagingCenter.Subscribe<AddItemViewModel>(
               this,
               MessengerConstants.HidePopUp,
               async sender =>
               {
                   await Navigation.PopPopupAsync(true);
               });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessageCenterUnsubscribe();
        }
    }
}