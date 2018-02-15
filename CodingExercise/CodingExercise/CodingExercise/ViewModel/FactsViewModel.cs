using CodingExercise.Model;
using CodingExercise.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CodingExercise.ViewModel
{
    public class FactsViewModel : BindableObject
    {
        public FactsViewModel()
        {

        }

        private string title = string.Empty;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != string.Empty)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Row> FactsCollection { get; set; }
        
        public ICommand LoadCommand
        {
            get
            {
                return new Command(async () => await GetFacts());
            }
        }

        public async Task GetFacts()
        {
            try
            {
                string response = await ServiceHelper.GetFactsFromAPI();

                if (!string.IsNullOrEmpty(response))
                {
                    System.Diagnostics.Debug.WriteLine(response);
                }

            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }            
        }

    }
}
