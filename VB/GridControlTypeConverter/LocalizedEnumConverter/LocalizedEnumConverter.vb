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

Namespace GridControlTypeConverter
	''' <summary>
	''' Defines a type converter for enum types defined in this project
	''' </summary>
	Friend Class LocalizedEnumConverter
		Inherits ResourceEnumConverter
		''' <summary>
		''' Create a new instance of the converter using translations from the given resource manager
		''' </summary>
		''' <param name="type"></param>
		Public Sub New(ByVal type As Type)
			MyBase.New(type, My.Resources.ResourceManager)

		End Sub
		Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
			If sourceType Is GetType(MyObject.MyEnum) Then
				Return True
			End If
			Return MyBase.CanConvertFrom(context, sourceType)
		End Function
	End Class
End Namespace
