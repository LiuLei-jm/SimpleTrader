using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MessageViewModel :ViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                    OnPropertyChanged(nameof(HasMessage));
                }
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}
