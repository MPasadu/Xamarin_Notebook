using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RemMe.ViewModels {

    /// <summary>
    /// PageService class used to navigate and show alerts in MVVM.
    /// </summary>
    public class PageService : IPageService {

        /// <summary>
        /// Displays an alert with one button.
        /// </summary>
        /// <param name="title">Title of alert</param>
        /// <param name="message">Message of alert</param>
        /// <param name="ok">Text of alert button</param>
        /// <returns></returns>
        public async Task DisplayAlert(string title, string message, string ok) {
            await MainPage.DisplayAlert(title, message, ok);
        }

        /// <summary>
        /// Displays an alert with two buttons.
        /// </summary>
        /// <param name="title">Title of alert</param>
        /// <param name="message">Message of alert</param>
        /// <param name="ok">Ok text of alert button</param>
        /// <param name="cancel">Cancel text of alert button</param>
        /// <returns>Task completed</returns>
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel) {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        /// <summary>
        /// Navigates to another page by pushing a page onto the stack.
        /// </summary>
        /// <param name="page">Page to push</param>
        /// <returns>Task completed</returns>
        public async Task PushAsync(Page page) {
            await MainPage.Navigation.PushAsync(page);
        }

        /// <summary>
        /// Navigates to another page by poping the current page from the stack.
        /// </summary>
        /// <returns>Task completed</returns>
        public async Task<Page> PopAsync() {
            return await MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// returns the MainPage
        /// </summary>
        private Page MainPage {
            get { return Application.Current.MainPage; }
        }
    }
}
