using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Flight_Inspection.controls.Video
{
    class TimeConverterToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int max = (int)value;
            float sec = (float)max / 10.0f;
            TimeSpan time = TimeSpan.FromSeconds(sec);
            return time.ToString(@"mm\:ss");

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            var a = s.Split(':');
            int hr = int.Parse(a[0]) * 3600;
            int mn = int.Parse(a[1]) * 60;
            int sc = int.Parse(a[2]) * 1;
            return (hr + mn + sc) * 10;
        }
    }
}
