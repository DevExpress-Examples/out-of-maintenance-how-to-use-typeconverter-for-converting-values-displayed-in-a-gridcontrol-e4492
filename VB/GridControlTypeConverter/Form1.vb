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
Imports System.Globalization
Imports System.Threading

Namespace GridControlTypeConverter
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
			InitLocalizibleArea()
			InitGrid()

		End Sub
		Private helper, helper1 As TypeConverterHelper
		Private objectList As New BindingList(Of MyObject)()
		Private ri As New RepositoryItemTextEdit()
		Private Sub InitLocalizibleArea()
			comboBoxEdit1.EditValue = Thread.CurrentThread.CurrentCulture
			For Each item In CultureInfo.GetCultures(CultureTypes.SpecificCultures)
				comboBoxEdit1.Properties.Items.Add(item)
			Next item
			For Each value In System.Enum.GetValues(GetType(MyObject.MyEnum))
				Dim converter As TypeConverter = TypeDescriptor.GetConverter(value.GetType())
				listBoxControl1.Items.Add(converter.ConvertToString(value))
			Next value
			AddHandler comboBoxEdit1.EditValueChanged, AddressOf comboBoxEdit1_EditValueChanged
			AddHandler comboBoxEdit1.CustomDisplayText, AddressOf comboBoxEdit1_CustomDisplayText

		End Sub
		Private Sub comboBoxEdit1_CustomDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs)
			e.DisplayText = (CType(e.Value, CultureInfo)).DisplayName
		End Sub
		Private Sub comboBoxEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			listBoxControl1.Items.Clear()
			Dim culture As CultureInfo = TryCast(comboBoxEdit1.EditValue, CultureInfo)
			If culture IsNot Nothing Then
				Thread.CurrentThread.CurrentCulture = culture
				For Each value In System.Enum.GetValues(GetType(MyObject.MyEnum))
					Dim converter As TypeConverter = TypeDescriptor.GetConverter(value.GetType())
					listBoxControl1.Items.Add(converter.ConvertToString(value))
				Next value
				For i As Integer = 0 To gridView1.DataRowCount - 1
					gridView1.RefreshRowCell(i, gridView1.Columns("UnboundLocalizedEnum"))
				Next i
			End If
		End Sub
		Private Sub InitGrid()
			For i As Integer = 0 To 9
				objectList.Add(New MyObject())
			Next i
			gridControl1.DataSource = objectList
			gridControl2.DataSource = objectList
			helper = New TypeConverterHelper("UnboundLocation", "Location",gridView2)
			helper1 = New TypeConverterHelper("UnboundLocalizedEnum", "Capacity", gridView1)
		End Sub
	End Class
End Namespace