using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;

namespace mom.Converters
{
    public class ConvertFileInfoToFileName : MarkupExtension, IValueConverter
    {
        private static ConvertFileInfoToFileName instance;

        //public static ConvertFileInfoToFileName Instance {
        //    get { return instance ?? (instance = new ConvertFileInfoToFileName()); }
        //}

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var info = value as FileInfo;
            if (info != null)
            {
                return Path.GetFileNameWithoutExtension(info.Name).ToUpper();
            }
            var s = value as string;
            if (s != null)
            {
                return Path.GetFileNameWithoutExtension(s).ToUpper();
            }
            return value;
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
            return instance ?? (instance = new ConvertFileInfoToFileName());
        }
    }
}