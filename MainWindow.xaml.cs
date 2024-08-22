using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using calculator.View;
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using System.Windows.Documents;
using Calculator.NewPage;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        //常量定义，用于窗口样式设置
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;

        //导入用户界面相关的Windows API函数，用于设置窗口样式
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        public MainWindow()
        {
            InitializeComponent();
            //初始化窗口时，设置openBorder的不透明度为1（完全不透明）
            openBorder.Opacity = 1;

            concisepageFrame.Navigate(new Uri("NewPage/concisepage.xaml", UriKind.Relative));


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Linestoy(1, SlantLine0);
            Linestoy(1.1, SlantLine1);
            Linestoy(1.2, SlantLine2);
            Linestoy(1.3, SlantLine3);
            Linestoy(1.4, SlantLine4);
            Linestoy(1.5, SlantLine5);

            //查找并开始名为"FadeOutStoryboard"的Storyboard动画
            var storyboard2 = (Storyboard)FindResource("FadeOutStoryboard");
            storyboard2.Begin();
        }

        private void Linestoy(double bt, Line line)
        {
            //创建旋转动画
            DoubleAnimation rotateAnimation = new DoubleAnimation();
            rotateAnimation.From = 0;
            rotateAnimation.To = 10;
            rotateAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            rotateAnimation.RepeatBehavior = RepeatBehavior.Forever; //或者 RepeatBehavior.Count(1) 如果只需要播放一次

            //设置旋转动画的目标属性
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(0).(1)", Line.RenderTransformProperty, RotateTransform.AngleProperty));

            //创建透明度动画
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = 1;
            opacityAnimation.To = 0;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            //设置透明度动画的目标属性
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(Line.OpacityProperty));

            //创建故事板并添加动画
            Storyboard storyBoard = new Storyboard();
            storyBoard.Children.Add(rotateAnimation);
            storyBoard.Children.Add(opacityAnimation);

            //设置动画开始时间
            rotateAnimation.BeginTime = TimeSpan.FromSeconds(bt);
            opacityAnimation.BeginTime = TimeSpan.FromSeconds(bt);
            //启动！！！
            storyBoard.Begin(line);
        }

        public void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //最小化窗口
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            //切换窗口最大化/还原状态
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //实现窗口拖动功能
            DragMove();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //关闭窗口
            Close();
        }

        double StoryTime = 0.15;//动画时间

        private void Animation(Border p, ThicknessAnimation marginAnimation, ColorAnimation colorAnimation)
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

        //下滑栏

        // 当记录按钮(record)被点击时的处理
        public void Record_Click(object sender, RoutedEventArgs e)
        {
            belowPageFrame.Navigate(new Uri("NewPage/BelowPage.xaml", UriKind.Relative));
            // 将主显示区的十进制数转换为其他进制并分别显示
            //HEX.Text = Convert.ToString((int)double.Parse(maintext.Text), 16); // 十进制转十六进制
            //DEC.Text = double.Parse(maintext.Text) + ""; // 十进制直接显示
            //OCT.Text = Convert.ToString((int)double.Parse(maintext.Text), 8); // 十进制转八进制
            //BIN.Text = Convert.ToString((int)double.Parse(maintext.Text), 2); // 十进制转二进制

            // Margin动画设置
            var marginAnimation = new ThicknessAnimation
            {
                To = new Thickness(0, 137, 0, 0), // 目标Margin值
                Duration = TimeSpan.FromSeconds(StoryTime), // 动画持续时间
                EasingFunction = new QuadraticEase() // 使用二次缓动效果
            };
            // 背景色动画设置
            var colorAnimation = new ColorAnimation
            {
                From = Color.FromArgb(0, 20, 20, 20), // 起始颜色
                To = Color.FromArgb(250, 46, 46, 46), // 结束颜色
                Duration = TimeSpan.FromSeconds(StoryTime),
                EasingFunction = new QuadraticEase()
            };
            Animation(BelowPanel, marginAnimation, colorAnimation);
        }
        public void BelowBack()
        {

            // Margin动画设置，让面板回到初始位置
            var marginAnimation = new ThicknessAnimation
            {
                To = new Thickness(0, this.Height, 0, 0),
                Duration = TimeSpan.FromSeconds(StoryTime),
                EasingFunction = new QuadraticEase()
            };

            // 背景色动画设置，恢复原始颜色
            var colorAnimation = new ColorAnimation
            {
                To = Color.FromArgb(0, 20, 20, 20),
                Duration = TimeSpan.FromSeconds(StoryTime),
                EasingFunction = new QuadraticEase()
            };
            Animation(BelowPanel, marginAnimation, colorAnimation);

        }
        public void BelowCollapsed()
        {
            // Margin动画设置，让面板回到初始位置
            var marginAnimation = new ThicknessAnimation
            {
                To = new Thickness(0, this.Height, 0, 0),
                Duration = TimeSpan.FromSeconds(0),
                EasingFunction = new QuadraticEase()
            };

            // 背景色动画设置，恢复原始颜色
            var colorAnimation = new ColorAnimation
            {
                To = Color.FromArgb(0, 20, 20, 20),
                Duration = TimeSpan.FromSeconds(0),
                EasingFunction = new QuadraticEase()
            };
            Animation(BelowPanel, marginAnimation, colorAnimation);
        }
        //侧滑栏
        public void sideButton_Click(object sender, RoutedEventArgs e)
        {
            // 显示用于展示进制转换的面板
            sidePageFrame.Navigate(new Uri("NewPage/SidePage.xaml", UriKind.Relative));

            // Margin动画设置
            var marginAnimation = new ThicknessAnimation
            {
                To = new Thickness(0, 33, this.Width - 190, 0), // 目标Margin值
                Duration = TimeSpan.FromSeconds(StoryTime), // 动画持续时间
                EasingFunction = new QuadraticEase() // 使用二次缓动效果
            };
            // 背景色动画设置
            var colorAnimation = new ColorAnimation
            {
                From = Color.FromArgb(0, 20, 20, 20), // 起始颜色
                To = Color.FromArgb(250, 46, 46, 46), // 结束颜色
                Duration = TimeSpan.FromSeconds(StoryTime),
                EasingFunction = new QuadraticEase()
            };
            Animation(SidePanel, marginAnimation, colorAnimation);
            BelowBack();
        }
        public void SideBack()
        {

            // Margin动画设置，让面板回到初始位置
            var marginAnimation = new ThicknessAnimation
            {
                To = new Thickness(0, 33, this.Width, 0),
                Duration = TimeSpan.FromSeconds(StoryTime),
                EasingFunction = new QuadraticEase()
            };

            // 背景色动画设置，恢复原始颜色
            var colorAnimation = new ColorAnimation
            {
                To = Color.FromArgb(0, 20, 20, 20),
                Duration = TimeSpan.FromSeconds(StoryTime),
                EasingFunction = new QuadraticEase()
            };
            Animation(SidePanel, marginAnimation, colorAnimation);
        }
        public void SideCollapsed()
        {
            // Margin动画设置，让面板回到初始位置
            var marginAnimation = new ThicknessAnimation
            {
                To = new Thickness(0, 33, this.Width, 0),
                Duration = TimeSpan.FromSeconds(0),
                EasingFunction = new QuadraticEase()
            };

            // 背景色动画设置，恢复原始颜色
            var colorAnimation = new ColorAnimation
            {
                To = Color.FromArgb(0, 20, 20, 20),
                Duration = TimeSpan.FromSeconds(0),
                EasingFunction = new QuadraticEase()
            };
            Animation(SidePanel, marginAnimation, colorAnimation);
        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}