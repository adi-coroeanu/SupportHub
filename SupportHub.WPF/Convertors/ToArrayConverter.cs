using System.Globalization;
using System.Windows.Data;

namespace SupportHub.WPF.Convertors;

public class ToArrayConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        // if(values == null)
        //     Console.WriteLine("e null");
        return values.Clone();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}