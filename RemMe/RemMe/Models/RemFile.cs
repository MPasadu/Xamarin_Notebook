using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RemMe.Models {

    /// <summary>
    /// RemFile model
    /// </summary>
    public class RemFile {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public String ImagePath { get; set; }

    }
}
