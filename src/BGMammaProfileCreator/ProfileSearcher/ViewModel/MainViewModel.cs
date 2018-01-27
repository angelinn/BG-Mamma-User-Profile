using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProfileCreator;
using ProfileCreator.Models.Analyzed;
using ProfileSearcher.ProfileSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ProfileSearcher.ViewModel
{
    public class Field
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Field(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class MainViewModel : ViewModelBase
    {
        public List<Field> Fields { get; set; } = new List<Field>
        {
            new Field("Потребител", "user"),
            new Field("Линк към профил", "profile_url")
        };
        public List<AnalyzedUser> Users { get; set; } = new List<AnalyzedUser>();

        private Field selectedField;
        public Field SelectedField
        {
            get
            {
                return selectedField;
            }
            set
            {
                selectedField = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SearchCommand { get; private set; }
        public MainViewModel()
        {
            SearchCommand = new RelayCommand(Search);
        }

        private void Search()
        {
            if (!String.IsNullOrEmpty(query))
            {
                Users = ProfileSearchService.Search(query, selectedField.Value).ToList();
                RaisePropertyChanged("Users");
            }
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