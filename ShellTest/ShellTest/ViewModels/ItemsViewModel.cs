﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ShellTest.Models;
using ShellTest.Views;

namespace ShellTest.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command ToggleCommand { get; set; }
        public Command ToolbarCommand { get; set; }

        bool _state;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ToggleCommand = new Command(Toggle);
            ToolbarCommand = new Command(async () => await ToolBarAsync(), CanToolbar);

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Toggle()
        {
            _state = !_state;
            ToolbarCommand.ChangeCanExecute();
        }

        private bool CanToolbar()
        {
            return _state;
        }

        private async Task ToolBarAsync()
        {
            await Shell.Current.DisplayAlert("Message", "ToolBar Command Fired OK", "OK");
        }
    }
}