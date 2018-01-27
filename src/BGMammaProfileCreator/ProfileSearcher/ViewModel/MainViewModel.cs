using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProfileCreator.Models.Analyzed;
using System.Collections.Generic;
using System.Windows.Input;

namespace ProfileSearcher.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private List<AnalyzedUser> data = new List<AnalyzedUser>()
        {
            new AnalyzedUser
            {
                
            }
        };
        private ICommand SearchCommand { get; set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand(Search);
        }

        private void Search()
        {

        }

        private string query;
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
                RaisePropertyChanged();
            }
        }
    }
}