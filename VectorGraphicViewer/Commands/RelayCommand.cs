using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VectorGraphicViewer.Commands
{
    /// <summary>
    /// Implementation of ICommand for View commands implementation in View Models
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action? _function;
        private Func<bool>? _predicate;

        public RelayCommand(Action function)
        {
            _function = function;
        }

        public RelayCommand(Action function, Func<bool> predicate)
        {
            _function = function;
            _predicate = predicate;
        }

        public bool CanExecute(object? parameter)
        {
            if (_predicate != null)
            {
                return _predicate();
            }

            return true;
        }

        public void Execute(object? parameter)
        {
            if (_function != null)
            {
                _function();
            }
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
