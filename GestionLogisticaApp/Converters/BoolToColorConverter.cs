using System.Globalization;

namespace GestionLogisticaApp.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isError)
            {
                return isError ? Color.FromArgb("#EF4444") : Color.FromArgb("#10B981");
            }
            return Color.FromArgb("#10B981");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
