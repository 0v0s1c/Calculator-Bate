using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator.NewPage
{
    /// <summary>
    /// SidePage.xaml 的交互逻辑
    /// </summary>
    public partial class SidePage : Page
    {
        public SidePage()
        {
            InitializeComponent();

        }

        double storytime = 0.15;//动画时间
        //侧滑栏
        private void CancelSideButton_Click(object sender, RoutedEventArgs e) { }
        private void sidedarkpanel_MouseDown(object sender, MouseButtonEventArgs e) { sideback(); }
        private void sideback()
        {

        }

        private void SetButton_Clik(object sender, RoutedEventArgs e)
        {
        }
        private void SidePage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void sideCollapsed()
        {
        }
        private void animation(Border p, ThicknessAnimation marginAnimation, ColorAnimation colorAnimation)
        {
            // 创建动画以平滑改变面板的Margin属性和背景色
            var sb = new Storyboard();

            Storyboard.SetTarget(marginAnimation, p); // 设置动画作用的对象为"panel"
            Storyboard.SetTargetProperty(marginAnimation, new PropertyPath(FrameworkElement.MarginProperty)); // 设置动画影响的属性为Margin

            Storyboard.SetTarget(colorAnimation, p); // 设置动画目标为"panel"
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)")); // 动画作用于背景色

            // 将两个动画加入到Storyboard中
            sb.Children.Add(marginAnimation);
            sb.Children.Add(colorAnimation);

            // 开始执行Storyboard中的动画
            sb.Begin();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
