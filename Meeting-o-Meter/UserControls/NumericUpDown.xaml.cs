using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mom.UserControls
{
    /// <summary>
    ///     Value Conversion Class for the button height.
    ///     Divides the Hight of the control by two to get the height for one button.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class BtnHeightConverter : IValueConverter
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 2.0;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///     Value Converter for the Button Show Property.
    ///     Converts from <see cref="bool" /> to <see cref="System.Windows.Visibility" />
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BtnShowConverter : IValueConverter
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(value)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///     Value Converter for the Button Show Property.
    ///     Converts from <see cref="bool" /> to <see cref="System.Windows.Visibility" />
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BtnShowGridConverter : IValueConverter
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? 1 : 2;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///     Converter for the Text in the Value Text Box.
    ///     Makes sure that the correct decimation is displayed
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class DecimationConverter : IValueConverter
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dataValue = (double)value;

            return dataValue.ToString("F" + (uint)parameter);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return System.Convert.ToDouble(value);
            }
            catch (Exception)
            {
                return value;
            }
        }
    }

    /// <summary>
    ///     Interactionlogic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown
    {
        /// <summary>
        ///     Dependency Object for the Maximal Value of the UpDown Control
        /// </summary>
        public static readonly DependencyProperty _decimation =
            DependencyProperty.Register("Decimation", typeof(uint), typeof(NumericUpDown), new FrameworkPropertyMetadata(0U, DecimationCallback));

        /// <summary>
        ///     Dependency Object for the Maximal Value of the UpDown Control
        /// </summary>
        public static readonly DependencyProperty _maxvalue =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(NumericUpDown), new FrameworkPropertyMetadata(100.0, MinMaxValueCallback));

        /// <summary>
        ///     Dependency Object for the Minimal Value of the UpDown Control
        /// </summary>
        public static readonly DependencyProperty _minvalue =
            DependencyProperty.Register("MinValue", typeof(double), typeof(NumericUpDown), new FrameworkPropertyMetadata(0.0, MinMaxValueCallback));

        /// <summary>
        ///     Dependency Object for the state of visibility of the UpDown Buttons
        /// </summary>
        public static readonly DependencyProperty _showButtons =
            DependencyProperty.Register("ShowButtons", typeof(bool), typeof(NumericUpDown), new FrameworkPropertyMetadata(true));

        /// <summary>
        ///     Dependency Object for the Step Value of the UpDown Control
        /// </summary>
        public static readonly DependencyProperty _step =
            DependencyProperty.Register("Step", typeof(double), typeof(NumericUpDown), new FrameworkPropertyMetadata(1.0));

        /// <summary>
        ///     Dependency Object for the value of the UpDown Control
        /// </summary>
        public static readonly DependencyProperty _value =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0.0, ValuePropertyChangeCallback), validateValue);

        /// <summary>
        ///     Event Definition Value Change
        /// </summary>
        public static RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NumericUpDown));

        /// <summary>
        ///     Maximum possible value of the control
        /// </summary>
        private static double _maxValue = double.MaxValue;

        /// <summary>
        ///     Minimal possible value of the control
        /// </summary>
        private static double _minValue = double.MinValue;

        /// <summary>
        ///     Reference for static Functions
        /// </summary>
        private static NumericUpDown thisReference = null;

        /// <summary>
        ///     Default Constructor (nothing special here x) )
        /// </summary>
        public NumericUpDown()
        {
            InitializeComponent();
            thisReference = this;
        }

        /// <summary>
        ///     Destroys the Class (sets thispointer null)
        /// </summary>
        ~NumericUpDown()
        {
            thisReference = null;
        }

        /// <summary>
        ///     Event fired when value changes
        /// </summary>
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        /// <summary>
        ///     Specifies / Reads the number of digits shown after the decimal point
        /// </summary>
        public uint Decimation
        {
            get { return (uint)GetValue(_decimation); }
            set
            {
                SetValue(_decimation, value);
                SetDecimationBinding(value);
            }
        }

        /// <summary>
        ///     Gets / Sets the maximal value of the control's value
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(_maxvalue); }
            set { SetValue(_maxvalue, value); }
        }

        /// <summary>
        ///     Gets / Sets the minimal value of the control's value
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(_minvalue); }
            set { SetValue(_minvalue, value); }
        }

        /// <summary>
        ///     Specifies / Reads weather the UpDown Buttons are to be shown
        /// </summary>
        public bool ShowButtons
        {
            get { return (bool)GetValue(_value); }
            set { SetValue(_value, value); }
        }

        /// <summary>
        ///     Gets / Sets the step size (increment / decrement size) of the control's value
        /// </summary>
        public double Step
        {
            get { return (double)GetValue(_step); }
            set { SetValue(_step, value); }
        }

        /// <summary>
        ///     Gets / Sets the value that the control is showing
        /// </summary>
        /// <exception cref="ArgumentException" />
        /// <remarks>
        ///     If Value exceeds <see cref="MaxValue" /> or falls below <see cref="MinValue" />, an
        ///     <see cref="ArgumentException" /> is thrown
        /// </remarks>
        public double Value
        {
            get { return (double)GetValue(_value); }
            set { SetValue(_value, value); }
        }

        /// <summary>
        ///     Decrements the control's value by the value defined by <see cref="Step" />
        /// </summary>
        /// <remarks>The value doesn't increment over MaxValue or under MinValue</remarks>
        public void Decrement()
        {
            btnUp.IsEnabled = true;
            try
            {
                if (Value - Step < MinValue)
                {
                    btnDown.IsEnabled = false;
                    return;
                }
                Value -= Step;
            }
            catch (ArgumentException) { }
        }

        /// <summary>
        ///     Increments the control's value by the value defined by <see cref="Step" />
        /// </summary>
        /// <remarks>The value doesn't increment over MaxValue or under MinValue</remarks>
        public void Increment()
        {
            btnDown.IsEnabled = true;
            try
            {
                if (Value + Step > MaxValue)
                {
                    btnUp.IsEnabled = false;
                    return;
                }
                Value += Step;
            }
            catch (ArgumentException) { }
        }

        /// <summary>
        ///     Event Helper Function when Value is changed
        /// </summary>
        protected virtual void OnValueChanged()
        {
            var args = new RoutedEventArgs();
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        /// <summary>
        ///     Change Event for Decimation
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void DecimationCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //DecimationConverter.decimation = Convert.ToUInt32(e.NewValue);
            if (thisReference != null) thisReference.SetDecimationBinding(Convert.ToUInt32(e.NewValue));

            // Hack to update the control's binding.
            if (thisReference != null)
            {
                thisReference.Value++;
                thisReference.Value--;
            }
        }

        /// <summary>
        ///     Change Event for Min and Max Value
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void MinMaxValueCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == _minvalue) _minValue = Convert.ToDouble(e.NewValue);
            else if (e.Property == _maxvalue) _maxValue = Convert.ToDouble(e.NewValue);
        }

        /// <summary>
        ///     Validation function for the value.
        ///     Checks weather Value is inbetween <see cref="MinValue" /> and <see cref="MaxValue" />
        /// </summary>
        /// <param name="value">The current value of the Dependency Property</param>
        /// <returns>
        ///     <list type="bullet">
        ///         <item>
        ///             <term>true</term>
        ///             <description>The Value is inbetween <see cref="MinValue" /> and <see cref="MaxValue" /></description>
        ///         </item>
        ///         <item>
        ///             <term>false</term><description>The value is out of bounds</description>
        ///         </item>
        ///     </list>
        /// </returns>
        private static bool validateValue(object value)
        {
            var val = Convert.ToDouble(value);
            return (val >= _minValue && val <= _maxValue);
        }

        /// <summary>
        ///     Change Event for Value
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void ValuePropertyChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (thisReference != null) thisReference.OnValueChanged();
        }

        /// <summary>
        ///     Handler for the Down Button Click.
        ///     Decrements the <see cref="Value" /> by <see cref="Step" />
        /// </summary>
        /// <param name="sender">The Down Button Control</param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            Decrement();
        }

        /// <summary>
        ///     Handler for the Up Button Click.
        ///     Increments the <see cref="Value" /> by <see cref="Step" />
        /// </summary>
        /// <param name="sender">The Up Button Control</param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            Increment();
        }

        /// <summary>
        ///     Sets the decimation binding.
        /// </summary>
        /// <param name="decimation">The decimation.</param>
        private void SetDecimationBinding(uint decimation)
        {
            var bindingValue = new Binding("Value")
            {
                Converter = new DecimationConverter(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ElementName = "ucNumericUpDown",
                ConverterParameter = decimation
            };
            bindingValue.ValidationRules.Add(new ExceptionValidationRule());

            tbValue.SetBinding(TextBox.TextProperty, bindingValue);
        }

        /// <summary>
        ///     Handles the Loaded event of the ucNumericUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void ucNumericUpDown_Loaded(object sender, RoutedEventArgs e)
        {
            SetDecimationBinding(Decimation);
        }
    }
}

/*
<!--
	<c> Copyright eisiWare - Tobias Eiseler 2011

	This file is part of NumericUpDown.

    NumericUpDown is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    NumericUpDown is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with NumericUpDown.  If not, see <http://www.gnu.org/licenses/>.

-->
*/