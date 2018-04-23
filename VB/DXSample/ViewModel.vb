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

            Dim rules_Renamed = New List(Of FormattingRule)()
            rules_Renamed.Add(New FormattingRule() With { _
                .Expression = "([UnitPrice] * [Quantity] * (1 - [Discount]) - [Freight]) < 0", _
                .ApplyToRow = True, _
                .Type = FormattingType.Background _
            })
            rules_Renamed.Add(New FormattingRule() With { _
                .FieldName = "Discount", _
                .Expression = "[Discount] > 0", _
                .ApplyToRow = False, _
                .Type = FormattingType.Font _
            })
            rules_Renamed.Add(New FormattingRule() With { _
                .FieldName = "Discount", _
                .Expression = "[Discount] > 0", _
                .Type = FormattingType.Icon _
            })
            Return rules_Renamed
        End Function

        Private Shared Function GetOrders() As List(Of Order)
            Dim list As New List(Of Order)()
            list.Add(New Order() With { _
                .City = "Aachen", _
                .UnitPrice = 10, _
                .Quantity = 20, _
                .Discount = 0, _
                .Freight = 30.54 _
            })
            list.Add(New Order() With { _
                .City = "Aachen", _
                .UnitPrice = 6.20, _
                .Quantity = 12, _
                .Discount = 0, _
                .Freight = 30.54 _
            })
            list.Add(New Order() With { _
                .City = "Aachen", _
                .UnitPrice = 14.40, _
                .Quantity = 12, _
                .Discount = 0, _
                .Freight = 30.54 _
            })
            list.Add(New Order() With { _
                .City = "Aachen", _
                .UnitPrice = 4.80, _
                .Quantity = 18, _
                .Discount = 0, _
                .Freight = 4.45 _
            })
            list.Add(New Order() With { _
                .City = "Aachen", _
                .UnitPrice = 21, _
                .Quantity = 20, _
                .Discount = 0.03, _
                .Freight = 33.35 _
            })
            list.Add(New Order() With { _
                .City = "Aachen", _
                .UnitPrice = 6, _
                .Quantity = 7, _
                .Discount = 0, _
                .Freight = 149.74 _
            })

            list.Add(New Order() With { _
                .City = "Barcelona", _
                .UnitPrice = 16.80, _
                .Quantity = 5, _
                .Discount = 0, _
                .Freight = 10.14 _
            })
            list.Add(New Order() With { _
                .City = "Barcelona", _
                .UnitPrice = 6.20, _
                .Quantity = 45, _
                .Discount = 0, _
                .Freight = 10.14 _
            })
            list.Add(New Order() With { _
                .City = "Barcelona", _
                .UnitPrice = 14.40, _
                .Quantity = 5, _
                .Discount = 0.06, _
                .Freight = 18.69 _
            })
            list.Add(New Order() With { _
                .City = "Barcelona", _
                .UnitPrice = 4.80, _
                .Quantity = 17, _
                .Discount = 0, _
                .Freight = 18.69 _
            })
            list.Add(New Order() With { _
                .City = "Barcelona", _
                .UnitPrice = 21, _
                .Quantity = 35, _
                .Discount = 0, _
                .Freight = 6.54 _
            })
            list.Add(New Order() With { _
                .City = "Barcelona", _
                .UnitPrice = 6, _
                .Quantity = 5, _
                .Discount = 0, _
                .Freight = 1.36 _
            })

            list.Add(New Order() With { _
                .City = "London", _
                .UnitPrice = 8, _
                .Quantity = 30, _
                .Discount = 0, _
                .Freight = 22.77 _
            })
            list.Add(New Order() With { _
                .City = "London", _
                .UnitPrice = 26.60, _
                .Quantity = 9, _
                .Discount = 0, _
                .Freight = 22.77 _
            })
            list.Add(New Order() With { _
                .City = "London", _
                .UnitPrice = 3.60, _
                .Quantity = 25, _
                .Discount = 0.05, _
                .Freight = 18.69 _
            })
            list.Add(New Order() With { _
                .City = "London", _
                .UnitPrice = 15.60, _
                .Quantity = 2, _
                .Discount = 0, _
                .Freight = 94.34 _
            })
            list.Add(New Order() With { _
                .City = "London", _
                .UnitPrice = 27.20, _
                .Quantity = 56, _
                .Discount = 0, _
                .Freight = 6.54 _
            })
            list.Add(New Order() With { _
                .City = "London", _
                .UnitPrice = 28.80, _
                .Quantity = 70, _
                .Discount = 0.15, _
                .Freight = 1.36 _
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
