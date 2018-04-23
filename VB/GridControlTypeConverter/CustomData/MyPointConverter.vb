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
	Public Class MyPointConverter
		Inherits System.ComponentModel.TypeConverter

		Public Overrides Overloads Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
			If sourceType Is GetType(Point) Then
				Return True
			Else
				Return MyBase.CanConvertFrom(context, sourceType)
			End If
		End Function

		Public Overrides Overloads Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
			If TypeOf value Is Point Then
				Dim point As Point = CType(value, Point)
				Return String.Format("MyLocation ({0}, {1})", point.X, point.Y)
			Else
				Return MyBase.ConvertFrom(context, culture, value)
			End If
		End Function

		Public Overrides Overloads Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
			If destinationType Is GetType(Point) Then
				Return True
			End If
			Return MyBase.CanConvertTo(context, destinationType)
		End Function
		Public Overrides Overloads Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
			If destinationType Is GetType(Point) Then
				Dim v() As String = (CStr(value)).Split(New Char() { ","c,"X"c,"Y"c,"="c,")"c,"("c,"M"c,"y"c,"L"c,"o"c,"c"c,"a"c,"t"c,"i"c,"n"c," "c},StringSplitOptions.RemoveEmptyEntries)
				Return New Point(Integer.Parse(v(0)), Integer.Parse(v(1)))
			End If
			Return MyBase.ConvertTo(context, culture, value, destinationType)
		End Function
	End Class
End Namespace
