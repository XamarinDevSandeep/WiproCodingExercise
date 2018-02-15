using CodingExercise.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CodingExercise.View
{
    public class FactsPage : ContentPage
    {
        private FactsViewModel ViewModel
        {
            get { return BindingContext as FactsViewModel; }
        }

        public FactsPage()
        {
            Title = "Facts";
            BindingContext = new FactsViewModel();

            var btnRestApi = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Get Data",
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderRadius = 0
            };
            btnRestApi.SetBinding(Button.CommandProperty, "LoadCommand");

            Content = new StackLayout
            {
                Children = {
                    btnRestApi
                }
            };
        }
    }
}