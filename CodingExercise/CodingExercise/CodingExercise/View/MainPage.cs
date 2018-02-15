using Xamarin.Forms;

namespace CodingExercise.View
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            #region Button Style
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
            #endregion

            #region Controls
            var btnRestApi = new Button
            {
                Text = "REST Api",
                Style = buttonStyle
            };

            var btnFileSystem = new Button
            {
                Text = "File System",
                Style = buttonStyle
            };
            #endregion

            #region Container
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Text = "Get data from -"
                    },
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        Padding = 20,
                        Spacing = 10,
                        Children =
                        {
                            btnRestApi,
                            new Label
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                Text = "OR"
                            },
                            btnFileSystem
                        }
                    }
                }
            };
            #endregion

            #region Events/Gestures
            btnRestApi.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new FactsPage(callRestApi: true));
            };

            btnFileSystem.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new FactsPage(callRestApi: false));
            };
            #endregion
        }
    }
}