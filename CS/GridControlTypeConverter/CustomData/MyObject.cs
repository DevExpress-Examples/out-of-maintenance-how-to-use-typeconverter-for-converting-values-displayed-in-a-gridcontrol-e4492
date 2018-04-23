using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridControlTypeConverter
{
    public class MyObject
    {
        private MyEnum _Capacity;
        static int counter = 1;
        public MyObject()
        {
            _ID = counter;
            _Name = "Object" + counter;
            _Location = new Point(counter < 5 ? counter + 3 : counter - 2, counter > 4 ? counter - 3 : counter + 2);
            if (counter % 2 == 0)
                _Capacity = MyEnum.Small;
            else if (counter % 3 == 0)
                _Capacity = MyEnum.Medium;
            else
            {
                _Capacity = MyEnum.Large;
            }
            counter++;
        }
        [TypeConverter(typeof(LocalizedEnumConverter))]
        public enum MyEnum
        {
         Large,
         Medium,
         Small
        }
        //// Fields...
        private Point _Location;
        private string _Name;
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
       [TypeConverter(typeof(MyPointConverter))]
        public Point Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        [TypeConverter(typeof(LocalizedEnumConverter))]
        public MyEnum Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }

    }
}
