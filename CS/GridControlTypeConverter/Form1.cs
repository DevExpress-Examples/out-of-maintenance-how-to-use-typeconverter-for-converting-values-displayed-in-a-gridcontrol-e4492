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
using System.Globalization;
using System.Threading;

namespace GridControlTypeConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitLocalizibleArea();
            InitGrid();
           
        }
        private TypeConverterHelper helper , helper1;
        BindingList<MyObject> objectList = new BindingList<MyObject>();
        RepositoryItemTextEdit ri = new RepositoryItemTextEdit();
        private void InitLocalizibleArea()
        {
            comboBoxEdit1.EditValue = Thread.CurrentThread.CurrentCulture;
            foreach (var item in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                comboBoxEdit1.Properties.Items.Add(item);
            }
            foreach (var value in Enum.GetValues(typeof(MyObject.MyEnum)))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(value.GetType());
                listBoxControl1.Items.Add(converter.ConvertToString(value));
            }           
            comboBoxEdit1.EditValueChanged += new EventHandler(comboBoxEdit1_EditValueChanged);            
            comboBoxEdit1.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(comboBoxEdit1_CustomDisplayText);
          
        }  
        void comboBoxEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            e.DisplayText = ((CultureInfo)e.Value).DisplayName;  
        }
        void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear();
            CultureInfo culture = comboBoxEdit1.EditValue as CultureInfo;
            if (culture != null)
            {
                Thread.CurrentThread.CurrentCulture = culture;
                foreach (var value in Enum.GetValues(typeof(MyObject.MyEnum)))
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(value.GetType());
                    listBoxControl1.Items.Add(converter.ConvertToString(value));
                }
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    gridView1.RefreshRowCell(i, gridView1.Columns["UnboundLocalizedEnum"]);
                }
            }   
        }
        private void InitGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                objectList.Add(new MyObject());
            }
            gridControl1.DataSource = objectList;
            gridControl2.DataSource = objectList;
            helper = new TypeConverterHelper("UnboundLocation", "Location",gridView2);
            helper1 = new TypeConverterHelper("UnboundLocalizedEnum", "Capacity", gridView1); 
        }
    }
}