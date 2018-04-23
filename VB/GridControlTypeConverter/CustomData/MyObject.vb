Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace GridControlTypeConverter
	Public Class MyObject
		Private _Capacity As MyEnum
		Private Shared counter As Integer = 1
		Public Sub New()
			_ID = counter
			_Name = "Object" & counter
			_Location = New Point(If(counter < 5, counter + 3, counter - 2),If(counter > 4, counter - 3, counter + 2))
			If counter Mod 2 = 0 Then
				_Capacity = MyEnum.Small
			ElseIf counter Mod 3 = 0 Then
				_Capacity = MyEnum.Medium
			Else
				_Capacity = MyEnum.Large
			End If
			counter += 1
		End Sub
		<TypeConverter(GetType(LocalizedEnumConverter))> _
		Public Enum MyEnum
		 Large
		 Medium
		 Small
		End Enum
		'// Fields...
		Private _Location As Point
		Private _Name As String
		Private _ID As Integer
		Public Property ID() As Integer
			Get
				Return _ID
			End Get
			Set(ByVal value As Integer)
				_ID = value
			End Set
		End Property
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				_Name = value
			End Set
		End Property
	   <TypeConverter(GetType(MyPointConverter))> _
	   Public Property Location() As Point
			Get
				Return _Location
			End Get
			Set(ByVal value As Point)
				_Location = value
			End Set
	   End Property
		<TypeConverter(GetType(LocalizedEnumConverter))> _
		Public Property Capacity() As MyEnum
			Get
				Return _Capacity
			End Get
			Set(ByVal value As MyEnum)
				_Capacity = value
			End Set
		End Property

	End Class
End Namespace
