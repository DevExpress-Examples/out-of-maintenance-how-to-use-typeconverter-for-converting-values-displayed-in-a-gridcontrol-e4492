Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports System.Collections

Namespace GridControlTypeConverter
	Public Class TypeConverterHelper
		Private colView As GridView
		Private unboundColumnFieldName As String = "UnboundColumn", convertedProperty As String = String.Empty
		Public Sub New(ByVal unboundColumnFieldName As String, ByVal convertedProperty As String, ByVal view As GridView)

			Me.unboundColumnFieldName = unboundColumnFieldName
			Me.convertedProperty = convertedProperty
			colView = view
			CreateUnboundColumn(view, unboundColumnFieldName)
		End Sub
		Private Sub SubscribeToEvents()
			AddHandler colView.CustomUnboundColumnData, AddressOf OnCustomUnboundColumnData
		End Sub
		Private Sub CreateUnboundColumn(ByVal view As GridView, ByVal unboundColumnFieldName As String)

			view.PopulateColumns()
			Dim originColumn As GridColumn = view.Columns(convertedProperty)
			If originColumn IsNot Nothing Then
				Dim column As GridColumn = view.Columns.AddVisible(unboundColumnFieldName)
				column.UnboundType = DevExpress.Data.UnboundColumnType.Object
				SubscribeToEvents()
			End If
		End Sub
		Private Sub OnCustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs)
			If e.Column.FieldName = unboundColumnFieldName Then
				If e.IsGetData Then
					Dim dataSource As IList = TryCast(colView.DataSource, IList)
					Dim obj As Object = dataSource(e.ListSourceRowIndex)
					If obj Is Nothing Then
						Return
					End If
					Dim descriptor As PropertyDescriptor = TypeDescriptor.GetProperties(obj.GetType())(convertedProperty)
					If descriptor Is Nothing Then
						Return
					End If
					Dim value As Object = descriptor.GetValue(obj)
					Dim converter As TypeConverter = descriptor.Converter
					If converter IsNot Nothing AndAlso converter.CanConvertFrom(value.GetType()) AndAlso TypeOf value Is System.Enum Then
						e.Value = converter.ConvertFrom(value.ToString())
					ElseIf converter IsNot Nothing AndAlso converter.CanConvertFrom(value.GetType()) Then
						e.Value = converter.ConvertFrom(value)
					End If
				ElseIf e.IsSetData Then
					Dim dataSource As IList = TryCast(colView.DataSource, IList)
					Dim obj As Object = dataSource(e.ListSourceRowIndex)
					If obj Is Nothing Then
						Return
					End If
					Dim descriptor As PropertyDescriptor = TypeDescriptor.GetProperties(obj.GetType())(convertedProperty)
					If descriptor Is Nothing Then
						Return
					End If
					Dim converter As TypeConverter = descriptor.Converter
					Dim value As Object = descriptor.GetValue(obj)
					If converter IsNot Nothing AndAlso converter.CanConvertTo(value.GetType()) Then
						descriptor.SetValue(obj, converter.ConvertTo(e.Value, value.GetType()))
					End If
				End If
			End If
		End Sub
		Private Sub UnsubcribeFromEvents()
			RemoveHandler colView.CustomUnboundColumnData, AddressOf OnCustomUnboundColumnData
		End Sub

		Public Sub RemoveUnboundColumn()
			Dim column As GridColumn = colView.Columns(unboundColumnFieldName)
			Dim originColumn As GridColumn = colView.Columns(convertedProperty)
			If column IsNot Nothing AndAlso originColumn IsNot Nothing Then
				colView.Columns.Remove(column)
				originColumn.Visible = True
				UnsubcribeFromEvents()
			End If
		End Sub
	End Class
End Namespace
