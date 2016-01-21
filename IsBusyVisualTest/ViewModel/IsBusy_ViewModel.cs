using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Threading;

namespace IsBusyVisualTest.ViewModel
{
    public class IsBusy_ViewModel : ViewModelBase
    {
        public ICommand DoWork { get; private set; }
        Timer myTimer;
        private Boolean _isBusy;
        public Boolean isBusy
        {
            get { return _isBusy; }
            set {
                if (_isBusy != value){
                    _isBusy = value;
                    RaisePropertyChanged("isBusy");
                }
            }
        }
        private int _progress;
        public int progress{
            get { return _progress; }
            set{
                if (_progress != value && value >= 0 && value <= 100){
                    _progress = value;
                    RaisePropertyChanged("progress");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public IsBusy_ViewModel()
        {
            Name = "isBusy_VM_Test";
            progress = 0;
            isBusy = false;
            myTimer = new Timer(myTimer_Tick);
            DoWork = new RelayCommand(() => executeDoWork(), () => canExecuteDoWork());
        }

        private int operationsLeft;
        private void executeDoWork()
        {
            myTimer.Change(0, 50);
            operationsLeft = 20;
            isBusy = true;
            progress = 0;
        }

        private Boolean canExecuteDoWork()
        {
            return !isBusy;
        }

        private void myTimer_Tick(object state)
        {
            myTimer.Change(Timeout.Infinite, 50);
            Thread.Sleep(50);
            operationsLeft--;
            progress += 5;
            myTimer.Change(0, 50);
            if (operationsLeft <= 0)
            {
                isBusy = false;
                progress = 0;
                myTimer.Change(Timeout.Infinite, 50); //Detiene timer
            }
        }
    }
}
