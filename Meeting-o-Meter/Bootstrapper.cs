using Caliburn.Micro;
using Caliburn.Micro.Logging;
using mom.Extensions;
using mom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace mom
{
    public class AppBootstrapper : Bootstrapper<IShellViewModel>
    {
        private static CompositionContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Caliburn.Micro.Bootstrapper`1"/> class.
        /// </summary>
        public AppBootstrapper()
        {
            LogManager.GetLog = type => new DebugLogger(type);
        }

        public static T GetInstance<T>()
        {
            var contract = AttributedModelServices.GetContractName(typeof(T));

            var sexports = container.GetExportedValues<object>(contract);
            var enumerable = sexports as object[] ?? sexports.ToArray();
            if (enumerable.Any())
                return enumerable.OfType<T>().First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

        protected override void Configure()
        {
            // Add New ViewLocator Rule
            ViewLocator.NameTransformer.AddRule(
                @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsvm>ViewModels\.)(?<nsafter>([A-Za-z_]\w*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>ViewModel$)",
                @"${nsbefore}Views.${nsafter}${basename}View",
                @"(([A-Za-z_]\w*\.)*)?ViewModels\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*ViewModel$"
                );

            container = new CompositionContainer(
                new AggregateCatalog(
                    new AssemblyCatalog(typeof(IShellViewModel).Assembly),
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>().FirstOrDefault())
                );

            var batch = new CompositionBatch();
            batch.AddExport<IWindowManager>(() => new WindowManager());
            batch.AddExport<IEventAggregator>(() => new EventAggregator());
            container.Compose(batch);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            var contract = AttributedModelServices.GetContractName(serviceType);
            return container.GetExportedValues<object>(contract);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;

            var exports = container.GetExportedValues<object>(contract);
            return exports.FirstOrDefault();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            base.OnExit(sender, e);
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param><param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(sender, e);
        }
    }
}