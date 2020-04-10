using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SystemDemonstrator.Commands
{

        class ICAutoMode : ICommand
        {
            /// <summary>
            /// Initializes a new instance of the UpdateCustomerCommand class.
            /// </summary>
            public ICAutoMode(c_MainViewModel viewModel)
            {
                m_ViewModel = viewModel;
            }

            private c_MainViewModel m_ViewModel;

            public event System.EventHandler CanExecuteChanged
            {
                add
                {
                    CommandManager.RequerySuggested += value;
                }
                remove
                {
                    CommandManager.RequerySuggested -= value;
                }
            }

            public bool CanExecute(object parameter)
            {
                //return m_ViewModel.CanUpdate;
                return true;
            }

            public void Execute(object parameter)
            {
                m_ViewModel.SetAutomode();
            }
        }
    }

