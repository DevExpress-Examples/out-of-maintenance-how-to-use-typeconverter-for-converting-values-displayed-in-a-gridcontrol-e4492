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
    public class TypeConverterHelper
    {       
        GridView colView;
        string unboundColumnFieldName = "UnboundColumn",
            convertedProperty = string.Empty;     
        public TypeConverterHelper(string unboundColumnFieldName,
            string convertedProperty, GridView view)
        {
            
            this.unboundColumnFieldName = unboundColumnFieldName;
            this.convertedProperty = convertedProperty;
            colView = view;
            CreateUnboundColumn(view, unboundColumnFieldName);          
        }
        void SubscribeToEvents()
        {
            colView.CustomUnboundColumnData += OnCustomUnboundColumnData;
        }
        private void CreateUnboundColumn(GridView view, string unboundColumnFieldName)
        {

            view.PopulateColumns();
            GridColumn originColumn = view.Columns[convertedProperty];
            if (originColumn != null)
            {             
                GridColumn column = view.Columns.AddVisible(unboundColumnFieldName);
                column.UnboundType = DevExpress.Data.UnboundColumnType.Object;         
                SubscribeToEvents();
            }
        }
        void OnCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == unboundColumnFieldName)
            {
                if (e.IsGetData)
                {
                    IList dataSource = colView.DataSource as IList;
                    object obj = dataSource[e.ListSourceRowIndex];
                    if (obj == null) return;
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj.GetType())[convertedProperty];
                    if (descriptor == null) return;
                    object value = descriptor.GetValue(obj);
                    TypeConverter converter = descriptor.Converter;
                    if (converter != null && converter.CanConvertFrom(value.GetType()) && value is Enum)
                        e.Value = converter.ConvertFrom(value.ToString());
                    else if (converter != null && converter.CanConvertFrom(value.GetType()))
                    {
                        e.Value = converter.ConvertFrom(value);                      
                    }
                }
                else if (e.IsSetData)
                {
                    IList dataSource = colView.DataSource as IList;
                    object obj = dataSource[e.ListSourceRowIndex];
                    if (obj == null) return;
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj.GetType())[convertedProperty];
                    if (descriptor == null) return;                        
                    TypeConverter converter = descriptor.Converter;
                    object value = descriptor.GetValue(obj);
                    if (converter != null && converter.CanConvertTo(value.GetType()))
                        descriptor.SetValue(obj, converter.ConvertTo(e.Value, value.GetType()));
                }
            }
        }
        void UnsubcribeFromEvents()
        {
            colView.CustomUnboundColumnData -= OnCustomUnboundColumnData;
        }

        public void RemoveUnboundColumn()
        {
            GridColumn column = colView.Columns[unboundColumnFieldName];
            GridColumn originColumn = colView.Columns[convertedProperty];
            if (column != null && originColumn != null)
            {
                colView.Columns.Remove(column);
                originColumn.Visible = true;
                UnsubcribeFromEvents();
            }
        }
    }
}
