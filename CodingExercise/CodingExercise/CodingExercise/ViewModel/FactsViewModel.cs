using CodingExercise.Model;
using CodingExercise.Utils;
using Newtonsoft.Json;
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
        private bool isAscending = false;
        private bool callRestApi = false;

        public FactsViewModel(bool _callRestApi)
        {
            callRestApi = _callRestApi;
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

        ObservableCollection<Row> factsCollection;
        public ObservableCollection<Row> FactsCollection
        {
            get
            {
                return factsCollection;
            }
            set
            {
                factsCollection = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public EventHandler ItemsLoaded;
        private Command loadCommand;
        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public Command LoadCommand
        {
            get { return loadCommand ?? (loadCommand = new Command(async () => await GetFacts())); }
        }

        private Command sortCommand;
        /// <summary>
        /// Command to load sorted items items
        /// </summary>
        public Command SortCommand
        {
            get { return sortCommand ?? (sortCommand = new Command(() => SortCollection())); }
        }

        public async Task GetFacts()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                string response = (callRestApi) ? await ServiceHelper.GetFactsFromAPI() : ServiceHelper.GetFactsFromFileSystem();

                if (!string.IsNullOrEmpty(response))
                {
                    FactsModel model = JsonConvert.DeserializeObject<FactsModel>(response);
                    if (model != null)
                    {
                        Title = model.title;
                        FactsCollection = (model.rows?.Count > 0) ? new ObservableCollection<Row>(model.rows) : new ObservableCollection<Row>();
                    }                    
                }

            }
            catch (System.Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
            this.ItemsLoaded(this, new EventArgs());
        }

        void SortCollection()
        {
            if (FactsCollection?.Count > 0)
            {
                if (isAscending)
                {
                    FactsCollection = new ObservableCollection<Row>(FactsCollection.OrderBy(i => i.title));
                    isAscending = false;
                }
                else
                {
                    FactsCollection = new ObservableCollection<Row>(FactsCollection.OrderByDescending(i => i.title));
                    isAscending = true;
                }
            }
        }

    }

}
