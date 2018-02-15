using Xamarin.Forms;

namespace CodingExercise.CustomControl
{
    public class LoadingView : ContentView
    {
        public LoadingView()
        {
            Label lblLoading = new Label
            {
                Text = "Loading...",
                TextColor = Color.Gray,
                FontSize = 12
            };

            Content = new StackLayout
            {
                BackgroundColor = Color.FromRgba(0, 0, 0, 0.5),
                Children =
                {
                    new Frame
                    {
                        Padding = new Thickness(20, 15),
                        HasShadow = false,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Content = new StackLayout
                        {
                            Children =
                            {
                                new ActivityIndicator { IsRunning = true },
                                lblLoading
                            }
                        }
                    }
                }
            };
        }
    }
}