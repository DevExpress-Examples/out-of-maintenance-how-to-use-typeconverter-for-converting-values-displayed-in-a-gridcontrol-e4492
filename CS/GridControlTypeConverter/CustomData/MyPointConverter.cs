using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Collections;

namespace GridControlTypeConverter
{
    public class MyPointConverter : System.ComponentModel.TypeConverter
    {

        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(Point))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is Point)
            {
                Point point = (Point)value;
                return string.Format("MyLocation ({0}, {1})", point.X, point.Y);
            }         
            else
                return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Point))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);//
        }
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(Point))
            {
                string[] v = ((string)value).Split(new char[] { ',','X','Y','=',')','(','M','y','L','o','c','a','t','i','n',' '},StringSplitOptions.RemoveEmptyEntries);
                return new Point(int.Parse(v[0]), int.Parse(v[1]));
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
