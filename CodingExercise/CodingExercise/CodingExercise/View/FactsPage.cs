using CodingExercise.CustomControl;
using CodingExercise.Model;
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
        private ListView myListView;
        private LoadingView loadingView;

        private FactsViewModel ViewModel
        {
            get { return BindingContext as FactsViewModel; }
        }

        public FactsPage()
        {
            this.SetBinding(TitleProperty, "Title");
            BackgroundColor = Color.WhiteSmoke;
            BindingContext = new FactsViewModel();

            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter { Property = Button.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.Green },
                    new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) },
                    new Setter { Property = Button.BorderRadiusProperty, Value = 0 }
                }
            };


            #region ListFacts
            myListView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(FactsItemTemplate))
            };
            myListView.SetBinding(ListView.ItemsSourceProperty, "FactsCollection");
            myListView.ItemSelected += MyListView_ItemSelected;
            #endregion

            #region Loading layout
            loadingView = new LoadingView
            {
                IsVisible = false
            };
            #endregion

            #region Container
            StackLayout stkContainer = new StackLayout
            {
                Spacing = 0,
                Padding = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var stackList = new StackLayout
            {
                Spacing = 0,
                Padding = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { myListView }
            };

            Label lblNoRecords = new Label
            {
                IsVisible = false,
                Text = "No Records Found",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(50)
            };

            StackLayout listContainer = new StackLayout
            {
                Spacing = 0,
                Padding = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { stackList, lblNoRecords }
            };

            stkContainer.Children.Add(listContainer);

            var btnRefresh = new Button
            {
                Style = buttonStyle,
                Text = "Refresh"
            };
            btnRefresh.SetBinding(Button.CommandProperty, "LoadCommand");

            var btnSort = new Button
            {
                Style = buttonStyle,
                Text = "Sort"
            };
            btnSort.SetBinding(Button.CommandProperty, "SortCommand");

            var stackButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 10,
                Spacing = 10,
                VerticalOptions = LayoutOptions.End,
                Children =
                {
                    btnRefresh,
                    btnSort
                }
            };
            stkContainer.Children.Add(stackButtons);

            RelativeLayout mainContainer = new RelativeLayout();
            mainContainer.Children.Add(
                stkContainer,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; })
                );

            mainContainer.Children.Add(
                loadingView,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; })
                );

            Content = mainContainer;
            #endregion

            ViewModel.PropertyChanged += IsBusyChanged;

            ViewModel.ItemsLoaded += new EventHandler((sender, e) =>
            {
                if (ViewModel.FactsCollection?.Count > 0)
                {
                    lblNoRecords.IsVisible = false;
                    stackList.IsVisible = true;
                }
                else
                {
                    stackList.IsVisible = false;
                    lblNoRecords.IsVisible = true;
                    btnRefresh.IsVisible = false;
                    btnSort.IsVisible = false;
                }
            });

        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void IsBusyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsBusy"))
            {
                if (ViewModel.IsBusy)
                    loadingView.IsVisible = true;
                else
                    loadingView.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadCommand.Execute(null);
        }
    }

    public class FactsItemTemplate : ViewCell
    {
        Image img;
        public FactsItemTemplate()
        {
            Grid myGrid = new Grid
            {
                Padding = 16,
                ColumnSpacing = 2,
                BackgroundColor = Color.White,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto},
                    new RowDefinition { Height = GridLength.Auto}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.7, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star)}
                }
            };

            var lblCaption = new Label
            {
                Margin = new Thickness(5, 5, 5, 0),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Blue,
                LineBreakMode = LineBreakMode.WordWrap
            };
            lblCaption.SetBinding(Label.TextProperty, "title");
            myGrid.Children.Add(lblCaption, 0, 2, 0, 1);

            var lblDescription = new Label
            {
                Margin = new Thickness(5, 5, 5, 0),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.Blue,
                LineBreakMode = LineBreakMode.WordWrap
            };
            lblDescription.SetBinding(Label.TextProperty, "description");
            myGrid.Children.Add(lblDescription, 0, 1, 1, 2);

            var imgPlaceholder = new Image
            {
                WidthRequest = 100,
                HeightRequest = 100,
                Aspect = Aspect.AspectFit,
                Source = "placeholder.png"
            };
            myGrid.Children.Add(imgPlaceholder, 1, 2, 1, 2);

            img = new Image
            {
                WidthRequest = 100,
                HeightRequest = 100,
                Aspect = Aspect.AspectFill
            };
            myGrid.Children.Add(img, 1, 2, 1, 2);

            View = myGrid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var item = BindingContext as Row;
            if (item == null)
                return;

            img.Source = (!string.IsNullOrEmpty(item.imageHref)) ? new UriImageSource { Uri = new Uri(item.imageHref), CachingEnabled = true, CacheValidity = new TimeSpan(24, 0, 0) } : null;
        }
    }
}