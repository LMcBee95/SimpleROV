using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMLibrary;

namespace WpfDemo.ViewModels
{
    public class HelloModel : BindingBase
    {
        public string Name
        {
            get => GetProperty(() => Name);
            set
            {
                SetProperty(() => Name, value);
                Notify(() => CanSayHello);
            }
        }

        public bool CanSayHello => Name.IsSomething();
        public async void SayHello()
        {
            await Task.Run(() => MessageBox.Show($"Hello {Name}!"));
        }

        [IgnoreBinding]
        public Command SayHelloCommand => new Command(SayHello);
    }

    public static class Extensions
    {
        public static bool IsSomething(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }
}
