using RemMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RemMe.ViewModels {

    /// <summary>
    /// Viewmodel associated with RemFile Model for MVVM.
    /// </summary>
    public class RemFileViewModel : BaseViewModel {

        #region Fields

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

        #endregion

        public RemFileViewModel() { }

        /// <summary>
        /// Constructor using RemFile Model for association
        /// </summary>
        /// <param name="remFile">Model for association</param>
        public RemFileViewModel(RemFile remFile) {
            this.Id = remFile.Id;
            this._description = remFile.Description;
            this._title = remFile.Title;
            this.Date = remFile.Date;


        }
    }
}
