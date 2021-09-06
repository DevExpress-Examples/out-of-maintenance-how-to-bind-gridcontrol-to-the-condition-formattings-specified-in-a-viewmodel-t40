Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Controls

Namespace DXSample
	Public Class ViewModel
		Public Sub New()
			Orders = GetOrders()
			Rules = GetFormattingRules()
		End Sub

		Private Shared Function GetFormattingRules() As List(Of FormattingRule)
'INSTANT VB NOTE: The variable rules was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim rules_Conflict = New List(Of FormattingRule)()
			rules_Conflict.Add(New FormattingRule() With {
				.Expression = "([UnitPrice] * [Quantity] * (1 - [Discount]) - [Freight]) < 0",
				.ApplyToRow = True,
				.Type = FormattingType.Background
			})
			rules_Conflict.Add(New FormattingRule() With {
				.FieldName = "Discount",
				.Expression = "[Discount] > 0",
				.ApplyToRow = False,
				.Type = FormattingType.Font
			})
			rules_Conflict.Add(New FormattingRule() With {
				.FieldName = "Discount",
				.Expression = "[Discount] > 0",
				.Type = FormattingType.Icon
			})
			Return rules_Conflict
		End Function

		Private Shared Function GetOrders() As List(Of Order)
			Dim list As New List(Of Order)()
			list.Add(New Order() With {
				.City = "Aachen",
				.UnitPrice = 10,
				.Quantity = 20,
				.Discount = 0,
				.Freight = 30.54
			})
			list.Add(New Order() With {
				.City = "Aachen",
				.UnitPrice = 6.20,
				.Quantity = 12,
				.Discount = 0,
				.Freight = 30.54
			})
			list.Add(New Order() With {
				.City = "Aachen",
				.UnitPrice = 14.40,
				.Quantity = 12,
				.Discount = 0,
				.Freight = 30.54
			})
			list.Add(New Order() With {
				.City = "Aachen",
				.UnitPrice = 4.80,
				.Quantity = 18,
				.Discount = 0,
				.Freight = 4.45
			})
			list.Add(New Order() With {
				.City = "Aachen",
				.UnitPrice = 21,
				.Quantity = 20,
				.Discount = 0.03,
				.Freight = 33.35
			})
			list.Add(New Order() With {
				.City = "Aachen",
				.UnitPrice = 6,
				.Quantity = 7,
				.Discount = 0,
				.Freight = 149.74
			})

			list.Add(New Order() With {
				.City = "Barcelona",
				.UnitPrice = 16.80,
				.Quantity = 5,
				.Discount = 0,
				.Freight = 10.14
			})
			list.Add(New Order() With {
				.City = "Barcelona",
				.UnitPrice = 6.20,
				.Quantity = 45,
				.Discount = 0,
				.Freight = 10.14
			})
			list.Add(New Order() With {
				.City = "Barcelona",
				.UnitPrice = 14.40,
				.Quantity = 5,
				.Discount = 0.06,
				.Freight = 18.69
			})
			list.Add(New Order() With {
				.City = "Barcelona",
				.UnitPrice = 4.80,
				.Quantity = 17,
				.Discount = 0,
				.Freight = 18.69
			})
			list.Add(New Order() With {
				.City = "Barcelona",
				.UnitPrice = 21,
				.Quantity = 35,
				.Discount = 0,
				.Freight = 6.54
			})
			list.Add(New Order() With {
				.City = "Barcelona",
				.UnitPrice = 6,
				.Quantity = 5,
				.Discount = 0,
				.Freight = 1.36
			})

			list.Add(New Order() With {
				.City = "London",
				.UnitPrice = 8,
				.Quantity = 30,
				.Discount = 0,
				.Freight = 22.77
			})
			list.Add(New Order() With {
				.City = "London",
				.UnitPrice = 26.60,
				.Quantity = 9,
				.Discount = 0,
				.Freight = 22.77
			})
			list.Add(New Order() With {
				.City = "London",
				.UnitPrice = 3.60,
				.Quantity = 25,
				.Discount = 0.05,
				.Freight = 18.69
			})
			list.Add(New Order() With {
				.City = "London",
				.UnitPrice = 15.60,
				.Quantity = 2,
				.Discount = 0,
				.Freight = 94.34
			})
			list.Add(New Order() With {
				.City = "London",
				.UnitPrice = 27.20,
				.Quantity = 56,
				.Discount = 0,
				.Freight = 6.54
			})
			list.Add(New Order() With {
				.City = "London",
				.UnitPrice = 28.80,
				.Quantity = 70,
				.Discount = 0.15,
				.Freight = 1.36
			})
			Return list
		End Function

		' A list of orders displayed within the grid control.   
		Private privateOrders As List(Of Order)
		Public Property Orders() As List(Of Order)
			Get
				Return privateOrders
			End Get
			Private Set(ByVal value As List(Of Order))
				privateOrders = value
			End Set
		End Property

		' A list of conditional formatting rules.   
		Private privateRules As List(Of FormattingRule)
		Public Property Rules() As List(Of FormattingRule)
			Get
				Return privateRules
			End Get
			Private Set(ByVal value As List(Of FormattingRule))
				privateRules = value
			End Set
		End Property
	End Class

	' Corresponds to an order items displayed within grid.   
	Public Class Order
		Public Property City() As String
		Public Property Discount() As Double
		Public Property Freight() As Double
		Public Property Quantity() As Double
		Public Property UnitPrice() As Double
	End Class

	Public Enum FormattingType
		Icon
		Font
		Background
	End Enum

	Public Class FormattingRule
		Public Overridable Property ApplyToRow() As Boolean
		Public Overridable Property Expression() As String
		Public Overridable Property FieldName() As String
		Public Overridable Property Type() As FormattingType
	End Class


	Public Class FormatConditionSelector
		Inherits DataTemplateSelector

		Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
			If Not (TypeOf item Is FormattingRule) Then
				Return Nothing
			End If
			Dim vm = TryCast(item, FormattingRule)
			Select Case vm.Type
				Case FormattingType.Icon
					Return IconTemplate
				Case FormattingType.Font
					Return FontTemplate
				Case FormattingType.Background
					Return BackgroundTemplate
				Case Else
					Return Nothing
			End Select
		End Function

		Public Property BackgroundTemplate() As DataTemplate
		Public Property FontTemplate() As DataTemplate
		Public Property IconTemplate() As DataTemplate
	End Class
End Namespace
