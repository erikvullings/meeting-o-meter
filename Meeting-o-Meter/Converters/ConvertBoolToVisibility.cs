using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace mom.Converters
{
    public class ConvertBoolToVisibility : MarkupExtension, IValueConverter
    {
        private static ConvertBoolToVisibility instance;

        /// <summary>
        /// Converts a value to a Visibility: in case parameter is absent, or true, a true value is Visible, otherwise Collapsed.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) return value;
            var isVisibleValue = true; // default case
            if (parameter != null)
            {
                bool parameterValue;
                if (bool.TryParse((string)parameter, out parameterValue)) isVisibleValue = parameterValue;
            }

            return (bool)value == isVisibleValue
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return instance ?? (instance = new ConvertBoolToVisibility());
        }
    }
}