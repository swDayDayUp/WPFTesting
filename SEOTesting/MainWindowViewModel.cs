using SEOTesting.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOTesting
{
    public class MainWindowViewModel : BindableBase
    {
        public Action CloseAction { get; set; }

        private DelegateCommand _BtnProcessClickCommand;
        public DelegateCommand BtnProcessClickCommand { get { return _BtnProcessClickCommand; } }

        private DelegateCommand _BtnExitClickCommand;
        public DelegateCommand BtnExitClickCommand { get { return _BtnExitClickCommand; } }

        public MainWindowViewModel()
        {
            _BtnProcessClickCommand = new DelegateCommand(HandelBtnProcessClick);
            _BtnExitClickCommand = new DelegateCommand(HandelBtnExitClick);
            
        }


        private SEOSummary _CurrentSEOSummary ;
        public SEOSummary CurrentSEOSummary
        {
            get { return _CurrentSEOSummary; }
            set { this.SetProperty(ref _CurrentSEOSummary, value); }
        }

        private bool _IsLoaded = false;
        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set { this.SetProperty(ref _IsLoaded, value); }
        }

        public void OnLoaded()
        {

            if (!IsLoaded)
            {
                //Do initial setup here
                CurrentSEOSummary = new SEOSummary("https://www.google.com.au/search?num=100&q=conveyancing+software", "(?i)<cite[^>]*>(.*)</cite>", "smokeball.com.au");
                IsLoaded = true;
            }
        }

        public virtual void HandelBtnProcessClick()
        {
            CurrentSEOSummary.ProcessSmokeBallSEO();
        }

        public virtual void HandelBtnExitClick()
        {
            CloseAction();
        }


    }
}
