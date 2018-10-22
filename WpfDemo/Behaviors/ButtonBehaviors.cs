using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDemo.Behaviors
{
    public static class ButtonBehaviors
    {
        public static DependencyProperty CloseWindowOnClickProperty =
            DependencyProperty.RegisterAttached(
                "CloseWindowOnClick",
                typeof(bool),
                typeof(ButtonBehaviors),
                new FrameworkPropertyMetadata(false, OnCloseWindowOnClickChanged));

        public static bool GetCloseWindowOnClick(DependencyObject target)
        {
            return (bool)target.GetValue(CloseWindowOnClickProperty);
        }

        public static void SetCloseWindowOnClick(DependencyObject target, bool value)
        {
            target.SetValue(CloseWindowOnClickProperty, value);
        }

        private static void OnCloseWindowOnClickChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target is Button)
            {
                var button = target as Button;
                if ((bool)e.NewValue)
                    button.Click += Button_Click;
                else
                    button.Click -= Button_Click;
            }
        }

        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(sender as DependencyObject);
            window.Close();
        }
    }
}
