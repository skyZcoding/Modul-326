using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Battleship_SolitaireUI.ViewModels;
using Caliburn.Micro;

namespace Battleship_SolitaireUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            container.Singleton<IWindowManager, WindowManager>();
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
