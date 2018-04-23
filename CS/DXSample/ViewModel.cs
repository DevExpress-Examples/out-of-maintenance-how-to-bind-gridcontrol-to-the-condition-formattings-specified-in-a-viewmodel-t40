using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DXSample {
    public class ViewModel
    {
        public ViewModel()
        {
            Orders = GetOrders();
            Rules = GetFormattingRules();
        }

        private static List<FormattingRule> GetFormattingRules()
        {
            var rules = new List<FormattingRule>();
            rules.Add(new FormattingRule() {
                Expression = "([UnitPrice] * [Quantity] * (1 - [Discount]) - [Freight]) < 0",
                ApplyToRow = true,
                Type = FormattingType.Background
            });
            rules.Add(new FormattingRule() {
                FieldName = "Discount",
                Expression = "[Discount] > 0",
                ApplyToRow = false,
                Type = FormattingType.Font
            });
            rules.Add(new FormattingRule() {
                FieldName = "Discount",
                Expression = "[Discount] > 0",
                Type = FormattingType.Icon
            });
            return rules;
        }

        private static List<Order> GetOrders()
        {
            List<Order> list = new List<Order>();
            list.Add(new Order() { City = "Aachen", UnitPrice = 10, Quantity = 20, Discount = 0, Freight = 30.54 });
            list.Add(new Order() { City = "Aachen", UnitPrice = 6.20, Quantity = 12, Discount = 0, Freight = 30.54 });
            list.Add(new Order() { City = "Aachen", UnitPrice = 14.40, Quantity = 12, Discount = 0, Freight = 30.54 });
            list.Add(new Order() { City = "Aachen", UnitPrice = 4.80, Quantity = 18, Discount = 0, Freight = 4.45 });
            list.Add(new Order() { City = "Aachen", UnitPrice = 21, Quantity = 20, Discount = 0.03, Freight = 33.35 });
            list.Add(new Order() { City = "Aachen", UnitPrice = 6, Quantity = 7, Discount = 0, Freight = 149.74 });

            list.Add(new Order() { City = "Barcelona", UnitPrice = 16.80, Quantity = 5, Discount = 0, Freight = 10.14 });
            list.Add(new Order() { City = "Barcelona", UnitPrice = 6.20, Quantity = 45, Discount = 0, Freight = 10.14 });
            list.Add(new Order() { City = "Barcelona", UnitPrice = 14.40, Quantity = 5, Discount = 0.06, Freight = 18.69 });
            list.Add(new Order() { City = "Barcelona", UnitPrice = 4.80, Quantity = 17, Discount = 0, Freight = 18.69 });
            list.Add(new Order() { City = "Barcelona", UnitPrice = 21, Quantity = 35, Discount = 0, Freight = 6.54 });
            list.Add(new Order() { City = "Barcelona", UnitPrice = 6, Quantity = 5, Discount = 0, Freight = 1.36 });

            list.Add(new Order() { City = "London", UnitPrice = 8, Quantity = 30, Discount = 0, Freight = 22.77 });
            list.Add(new Order() { City = "London", UnitPrice = 26.60, Quantity = 9, Discount = 0, Freight = 22.77 });
            list.Add(new Order() { City = "London", UnitPrice = 3.60, Quantity = 25, Discount = 0.05, Freight = 18.69 });
            list.Add(new Order() { City = "London", UnitPrice = 15.60, Quantity = 2, Discount = 0, Freight = 94.34 });
            list.Add(new Order() { City = "London", UnitPrice = 27.20, Quantity = 56, Discount = 0, Freight = 6.54 });
            list.Add(new Order() { City = "London", UnitPrice = 28.80, Quantity = 70, Discount = 0.15, Freight = 1.36 });
            return list;
        }

        // A list of orders displayed within the grid control.   
        public List<Order> Orders { get; private set; }

        // A list of conditional formatting rules.   
        public List<FormattingRule> Rules { get; private set; }
    }

    // Corresponds to an order items displayed within grid.   
    public class Order
    {
        public string City { get; set; }
        public double Discount { get; set; }
        public double Freight { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    public enum FormattingType { Icon, Font, Background }

    public class FormattingRule
    {
        public virtual bool ApplyToRow { get; set; }
        public virtual string Expression { get; set; }
        public virtual string FieldName { get; set; }
        public virtual FormattingType Type { get; set; }
    }


    public class FormatConditionSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is FormattingRule)) return null;
            var vm = item as FormattingRule;
            switch (vm.Type)
            {
                case FormattingType.Icon:
                    return IconTemplate;
                case FormattingType.Font:
                    return FontTemplate;
                case FormattingType.Background:
                    return BackgroundTemplate;
                default: return null;
            }
        }

        public DataTemplate BackgroundTemplate { get; set; }
        public DataTemplate FontTemplate { get; set; }
        public DataTemplate IconTemplate { get; set; }
    }
}
