using RemMe.Models;
using System;

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
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                SetValue(ref _description, value);
            }
        }

        private string _imagePath;
        public string ImagePath {
            get { return _imagePath; }
            set {
                SetValue(ref _imagePath, value);
            }
        }

        private DateTime _date;
        public DateTime Date {
            get { return _date; }
            set {
                SetValue(ref _date, value);
            }
        }

        public int Id { get; set; }


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
            this._date = remFile.Date;
            this._imagePath = remFile.ImagePath;

        }
    }
}
