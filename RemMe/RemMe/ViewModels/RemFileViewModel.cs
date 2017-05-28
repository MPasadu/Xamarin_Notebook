using RemMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RemMe.ViewModels {
    public class RemFileViewModel : BaseViewModel {

        private string _title;
        public string Title {
            get { return _title; }
            set {
                SetValue(ref _title, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                SetValue(ref _description, value);
                OnPropertyChanged(nameof(Description));
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public RemFileViewModel() { }

        public RemFileViewModel(RemFile remFile) {
            this.Id = remFile.Id;
            this._description = remFile.Description;
            this._title = remFile.Title;
            this.Date = remFile.Date;
        }
    }
}
