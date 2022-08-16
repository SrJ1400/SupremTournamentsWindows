using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SupremTournamentsProgram.Convertidores
{
    class LongDateToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds((long)value).Date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset datetime = (DateTime)value;

            //Al selecionar una fecha se seleciona la anterior a la indicada asi que es necesaria la siguiente linea para un buen funcionamiento
            datetime = datetime.AddDays(1.0);

            return datetime.ToUnixTimeMilliseconds();
        }
    }
}
