using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.ViewModels;
using Caliburn.Micro;

namespace Battleship_SolitaireUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Sets the startup view
        /// </summary>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        /// <summary>
        /// Initalize the dependency injection
        /// </summary>
        protected override void Configure()
        {
            // Initialize Caliburn Features
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            // Initialize ViewModels
            _container.Singleton<ShellViewModel>();
            _container.Singleton<PlayfieldViewModel>();
            _container.Singleton<OptionViewModel>();

            // Initialize Models
            _container.Singleton<Option>();
            _container.Singleton<Playfield>();
        }

        /// <summary>
        /// Returns the instance
        /// </summary>
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        /// <summary>
        /// Returns all instances
        /// </summary>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
    }
}
