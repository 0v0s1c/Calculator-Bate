using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculator.View
{
    /// <summary>
    /// historyControl.xaml 的交互逻辑
    /// </summary>
    public partial class historyControl : UserControl
    {
        public historyControl()
        {
            InitializeComponent();
            content.MouseEnter += bMouseEnter;
            content.MouseLeave += bMouseLeave;
            content.MouseDown += bMouseDown;
            rnum.MouseDown += bMouseDown;
            mnum.MouseDown += bMouseDown;
        }
        private void bMouseEnter(object sender, MouseEventArgs e)
        {
            // 更改 Rectangle 的 Fill 颜色
            content.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2D2D2D"));
        }

        private void bMouseLeave(object sender, MouseEventArgs e)
        {
            // 恢复 Rectangle 的原始 Fill 颜色
            content.Background = Brushes.Transparent;
        }


        public event EventHandler SomeActionTriggered;

        private void OnSomeAction()
        {
            SomeActionTriggered?.Invoke(this, EventArgs.Empty);
        }


        public delegate void MyEventHandler(object sender, EventArgs e);
        public event MyEventHandler MyEvent;

        private void bMouseDown(object sender, MouseEventArgs e)
        {
            MyEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}