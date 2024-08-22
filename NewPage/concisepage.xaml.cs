using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using calculator.View;
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using System.Windows.Documents;
using calculator;

namespace Calculator.NewPage
{
    /// <summary>
    /// concisepage.xaml 的交互逻辑
    /// </summary>
    public partial class ConcisePage : Page
    {
        //常量定义，用于窗口样式设置
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;

        //导入用户界面相关的Windows API函数，用于设置窗口样式
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        public ConcisePage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 确保panel始终位于窗口底部
            SideCollapsed();
            BelowCollapsed();


            //获取Storyboard

            //查找并开始名为"FadeOutStoryboard"的Storyboard动画
            var storyBoard = (Storyboard)FindResource("FadeOutStoryboard");
            storyBoard.Begin();
        }

        private void Linestoy(double bt, Line Line)
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
            storyBoard.Begin(Line);
        }

        public void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //最小化窗口
            ((MainWindow)Window.GetWindow(this)).MinimizeButton_Click(sender, e);
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //关闭窗口
            ((MainWindow)Window.GetWindow(this)).CloseButton_Click(sender, e);
        }

        public void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            //切换窗口最大化/还原状态
            ((MainWindow)Window.GetWindow(this)).MaximizeButton_Click(sender, e);
        }

        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //实现窗口拖动功能
            ((MainWindow)Window.GetWindow(this)).Border_MouseLeftButtonDown(sender, e);
        }

        // private void Window_SizeChanged(object sender, EventArgs e)
        // {
        //     Window mainWindow = Window.GetWindow(this);
        //     Border effectBorder = mainWindow.FindName("Effect") as Border;
        //     //获取关闭按钮背景的Border元素，用于调整样式
        //     Border closeBorder = this.closebutton.Template.FindName("closeback", this.closebutton) as Border;
        //
        //     // 根据窗口状态调整按钮图标、窗口圆角效果及关闭按钮的圆角
        //     if (mainWindow.WindowState == WindowState.Maximized)
        //     {
        //         maxbutton.Content = "\ue60f"; // 设置最大化图标
        //         effectBorder.CornerRadius = new CornerRadius(0); // 设置窗口圆角为0
        //         effectBorder.Margin = new Thickness(7.3); // 设置窗口边缘内边距
        //         closeBorder.CornerRadius = new CornerRadius(0, 0, 0, 0); // 非全屏时关闭按钮的圆角设置
        //     }
        //     else
        //     {
        //         maxbutton.Content = "\ue6a8"; // 设置非最大化图标
        //         effectBorder.CornerRadius = new CornerRadius(5); // 恢复窗口圆角为5
        //         effectBorder.Margin = new Thickness(1); // 恢复窗口边缘内边距
        //         closeBorder.CornerRadius = new CornerRadius(0, 5, 0, 0); // 全屏时关闭按钮的圆角设置
        //     }
        //
        //     Size(e);
        //
        //     // 确保panel始终位于窗口底部
        //     SideCollapsed();
        //     BelowCollapsed();
        // }

        struct History
        {
            public string RecordTexts;
            public string MainTexts;
        }

        decimal Op = 0;
        string Symbol = "";
        int Mod = 1;
        bool FirstMod = false;
        bool Equal = false;

        //数字输入
        void Num(int Num)
        {
            switch (Mod)
            {
                case 1:
                    MainText.Text = Num.ToString();
                    Mod = 2;
                    break;
                case 3:
                case 5:
                    MainText.Text = Num.ToString();
                    Mod = 4;
                    break;
                case 2:
                case 4:
                    MainText.Text += Num.ToString();
                    break;
                case 6:
                    FirstMod = false;
                    Equal = false;
                    Op = 0;
                    Mod = 2;
                    RecordText.Text = "";
                    MainText.Text = Num + "";
                    break;
            }
        }

        //数字
        private void btn1_Click(object sender, EventArgs e) { Num(1); }
        private void btn2_Click(object sender, EventArgs e) { Num(2); }
        private void btn3_Click(object sender, EventArgs e) { Num(3); }
        private void btn4_Click(object sender, EventArgs e) { Num(4); }
        private void btn5_Click(object sender, EventArgs e) { Num(5); }
        private void btn6_Click(object sender, EventArgs e) { Num(6); }
        private void btn7_Click(object sender, EventArgs e) { Num(7); }
        private void btn8_Click(object sender, EventArgs e) { Num(8); }
        private void btn9_Click(object sender, EventArgs e) { Num(9); }
        private void btn0_Click(object sender, EventArgs e) { Num(0); }

        //负
        private void btnbur_Click(object sender, EventArgs e)
        {
            MainText.Text = double.Parse(MainText.Text) * -1 + "";//奇怪的正负转换
        }
        //点
        private void btnDot_Click(object sender, EventArgs e)
        {
            if (!MainText.Text.Contains("."))//奇怪的检验小数
                MainText.Text += ".";
            if (Mod == 1)
                Mod = 4;
        }

        //c 清除全部
        private void btnC_Click(object sender, EventArgs e)
        {
            FirstMod = false;
            Equal = false;
            Op = 0;
            Mod = 1;
            RecordText.Text = "";
            MainText.Text = "0";
        }
        //ce 清除输入
        private void btnCE_Click(object sender, EventArgs e) { MainText.Text = "0"; }

        //运算符号
        void mode(string Mode)
        {
            if (Mod == 1 || Mod == 2 || Mod == 3 || Mod == 5 || Mod == 6)
            {
                if (Mode == "=")
                {
                    RecordText.Text += MainText.Text + "=";
                    Mod = 6;
                    Record_refresh();
                }
                else
                    Mod = 3;
                Symbol = Mode;
                RecordText.Text = MainText.Text + Mode;
                Op = decimal.Parse(MainText.Text);
            }
            else if (Mod == 4)
            {
                Mod = 5;
                string mt = MainText.Text;
                switch (Symbol)
                {
                    case "+": // 当symbol为"+"时
                              // 执行加法运算，并将结果转换为字符串赋值给maintext.Text
                        MainText.Text = (Op + decimal.Parse(MainText.Text)) + "";
                        break; // 完成当前case处理后退出switch
                    case "-": // 当symbol为"-"时
                              // 执行减法运算
                        MainText.Text = (Op - decimal.Parse(MainText.Text)) + "";
                        break;
                    case "×":
                        MainText.Text = (Op * decimal.Parse(MainText.Text)) + "";
                        break;
                    case "÷":
                        MainText.Text = (Op / decimal.Parse(MainText.Text)) + "";
                        break;
                }
                if (Mode == "=")
                {
                    RecordText.Text += mt + "=";
                    Mod = 6;
                    Record_refresh();
                }
                if (Mode != "=")
                {
                    RecordText.Text += mt + "=";
                    Record_refresh();
                    RecordText.Text = MainText.Text + Mode;
                }
            }

            Op = decimal.Parse(MainText.Text);
        }
        //+
        private void btnAdd_Click(object sender, EventArgs e) { mode("+"); }
        //-
        private void btnSub_Click(object sender, EventArgs e) { mode("-"); }
        //*
        private void btnMul_Click(object sender, EventArgs e) { mode("×"); }
        // /
        private void btnDiv_Click(object sender, EventArgs e) { mode("÷"); }
        // =
        private void btnEqual_Click(object sender, EventArgs e) { mode("="); }

        // 当退格键对应的按钮被点击时触发此方法
        private void btnremove_Click(object sender, EventArgs e)
        {
            // 如果显示文本长度为2，假设显示的是负数，将其转为正数
            if (MainText.Text.Length == 2)
                MainText.Text = Math.Abs(double.Parse(MainText.Text)) + "";
            // 如果显示文本长度大于1，移除最后一个字符
            if (MainText.Text.Length > 1)
                MainText.Text = MainText.Text.Substring(0, MainText.Text.Length - 1);
            // 如果仅剩一个字符，重置为0
            else if (MainText.Text.Length == 1)
                MainText.Text = "0";
        }

        // 执行x分之1的运算
        private void butquarter_Click(object sender, EventArgs e)
        {
            // 记录操作历史到recordtext
            RecordText.Text = "1/(" + MainText.Text + ")";
            // 计算并更新显示结果
            MainText.Text = "" + 1 / double.Parse(MainText.Text);
        }

        // 执行次方运算，这里默认为平方
        private void butsquare_Click(object sender, EventArgs e)
        {
            // 记录操作历史
            RecordText.Text = "sqr(" + MainText.Text + ")";
            // 计算并更新显示结果
            MainText.Text = "" + Math.Pow(double.Parse(MainText.Text), 2);
        }

        // 执行平方根运算
        private void butradicalsign_Click(object sender, EventArgs e)
        {
            // 记录操作历史
            RecordText.Text = "√(" + MainText.Text + ")";
            // 计算并更新显示结果
            MainText.Text = "" + Math.Sqrt(double.Parse(MainText.Text));
        }

        // 将当前数值转换为百分比
        private void btnpercent_Click(object sender, RoutedEventArgs e)
        {
            // 记录操作历史
            RecordText.Text = MainText.Text + "%";
            // 计算并更新显示结果为百分比
            MainText.Text = "" + double.Parse(MainText.Text) / 100;
        }

        public StackPanel r = new StackPanel();
        // 刷新历史记录显示
        private void Record_refresh()
        {
        historyControl historyControls = new historyControl(); // 创建一个新的历史记录项控件实例
            historyControls.rnum.Text = RecordText.Text;
            historyControls.mnum.Text = MainText.Text;

            // 保存现有历史记录项的副本，以便稍后重新添加
            var backupElements = new List<historyControl>(r.Children.Count);
            foreach (historyControl child in r.Children)
            {
                backupElements.Add(child);
            }
            // 清空现有显示的历史记录项
            r.Children.Clear();
            // 将新创建的历史记录项添加到显示列表中
            r.Children.Add(historyControls);
            // 将之前保存的历史记录项重新添加回显示列表
            foreach (historyControl child in backupElements)
            {
                r.Children.Add(child);
            }
        }

        // 鼠标按下历史记录项时的处理
        private void bMouseDown(historyControl b)
        {
            // 当点击历史记录项时，将对应的操作和结果反映到主界面
            MainText.Text = b.mnum.Text;
            RecordText.Text = b.rnum.Text;
        }

        // 删除所有历史记录的按钮点击处理
        private void Delete_Click(object sender, EventArgs e) { }// 清空历史记录显示区域


        double PageWidth;
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PageWidth = e.NewSize.Width;
            Size(e);
            // 隐藏进制转换展示面板
            sidedarkpanel.Visibility = Visibility.Collapsed;
            // 恢复标题栏背景透明
            Title.Background = Brushes.Transparent;
            SideCollapsed();
            BelowCollapsed();
        }

        // 主显示区域文本改变时的处理
        private void MainText_Change(object sender, EventArgs e) { Size(e); }// 调整字体大小以适应文本长度和窗口宽度

        // 自动调整字体大小的方法
        void Size(EventArgs e)
        {
            // 计算新的字体大小以适应文本内容
            int w = (int)((PageWidth / (MainText.Text.Length + 1) + 0.2) / 0.6);//整这个是真™烦
            // 根据计算结果限制字体大小在10到34之间
            if (w > 34)
                MainText.FontSize = 34;
            else if (w < 10)
                MainText.FontSize = 10;
            else
                MainText.FontSize = w;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            string Label;
            switch (e.Key)
            {
                case Key.Enter: // 如果按下的是Enter键
                    btnEqual_Click(sender, e); // 触发等于按钮的点击事件
                    break;

                // 对应数字键0-9的操作
                case Key.D0: btn0_Click(sender, e); break;
                case Key.D1: btn1_Click(sender, e); break;
                case Key.D2:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift) // 检查Shift键是否同时按下
                        butradicalsign_Click(sender, e); // 如果是，则触发平方根按钮事件
                    else
                        btn2_Click(sender, e); // 否则，触发数字2的按钮事件
                    break;
                case Key.D3: btn3_Click(sender, e); break;
                case Key.D4: btn4_Click(sender, e); break;
                case Key.D5:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                        btnpercent_Click(sender, e);
                    else
                        btn5_Click(sender, e);
                    break;
                case Key.D6: btn6_Click(sender, e); break;
                case Key.D7: btn7_Click(sender, e); break;
                case Key.D8:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                        btnMul_Click(sender, e);
                    else
                        btn8_Click(sender, e); break;
                case Key.D9: btn9_Click(sender, e); break;


                case Key.Q: butsquare_Click(sender, e); break; // Q键触发平方按钮
                case Key.R: butquarter_Click(sender, e); break; // R键触发四分之一按钮
                case Key.B: btnbur_Click(sender, e); break; // B键触发某个特定功能
                case Key.OemPeriod: btnDot_Click(sender, e); break; // 小数点键
                case Key.Back: btnremove_Click(sender, e); break; // Backspace键触发删除功能
                case Key.D: btnCE_Click(sender, e); break; // D键触发清除当前输入
                case Key.Escape: btnC_Click(sender, e); break; // Esc键触发全部清除
                case Key.OemPlus:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift) // Shift+加号键
                        btnAdd_Click(sender, e); // 触发加法按钮
                    else
                        btnEqual_Click(sender, e); // 否则，触发等于按钮
                    break;
                case Key.OemMinus: btnSub_Click(sender, e); break; // 减号键
                case Key.OemQuestion: btnDiv_Click(sender, e); break; // 斜杠键，通常代表除法
                                                                      // Ctrl+V粘贴操作
                case Key.V:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                    {
                        try
                        {
                            Label = Clipboard.GetText(TextDataFormat.Text); // 从剪贴板获取文本
                            double.Parse(Label); // 尝试将文本转换为double类型，检查是否为有效数字
                        }
                        catch
                        {
                            MainText.Text = "无效输入"; // 如果转换失败，显示错误信息
                            break;
                        }
                        MainText.Text = Clipboard.GetText(TextDataFormat.Text); // 显示粘贴的内容
                    }
                    break;
                // Ctrl+C复制操作
                case Key.C:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                        Clipboard.SetText(MainText.Text); // 复制计算结果到剪贴板
                    break;
                // Ctrl+X剪切操作
                case Key.X:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                    {
                        Clipboard.SetText(MainText.Text); // 将计算结果显示内容复制到剪贴板
                        MainText.Text = "0"; // 清空显示区域
                    }
                    break;
            }
        }
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


        double StoryTime = 0.15;//动画时间

        //下滑栏
        bool belowopen = false;
        // 当记录按钮(record)被点击时的处理
        private void Record_Click(object sender, RoutedEventArgs e)
        {
                belowopen = true;
                // 更改标题栏背景色为深色
                Title.Background = new SolidColorBrush(Color.FromArgb(0x45, 0x00, 0x00, 0x00));
                // 显示用于展示进制转换的面板
                belowdarkpanel.Visibility = Visibility.Visible;

                ((MainWindow)Window.GetWindow(this)).Record_Click(sender, e);
        }
        // 当深色面板(darkpanel)被鼠标按下时的处理
        private void BelowDarkPanel_MouseDown(object sender, MouseButtonEventArgs e) { BelowBack(); }

        private void BelowBack()
        {
            belowopen = false;
            // 隐藏进制转换展示面板
            belowdarkpanel.Visibility = Visibility.Collapsed;
            // 恢复标题栏背景透明
            Title.Background = Brushes.Transparent;
            ((MainWindow)Window.GetWindow(this)).BelowBack();
        }
        private void BelowCollapsed()
        {
            if (belowopen)
            {
                belowopen = false;
                BelowBack();
            }
            else
            {
                ((MainWindow)Window.GetWindow(this)).BelowCollapsed();
            }
        }


        //侧滑栏
        bool sideopen = false;
        public void sideButton_Click(object sender, RoutedEventArgs e)
        {
            BelowBack();
            if (sideopen)
            {
                sideopen = false;
                SideBack();
            }
            else
            {
                sideopen = true;
                sidedarkpanel.Visibility = Visibility.Visible;
                // 更改标题栏背景色为深色
                Title.Background = new SolidColorBrush(Color.FromArgb(0x45, 0x00, 0x00, 0x00));

                ((MainWindow)Window.GetWindow(this)).sideButton_Click(sender, e);
            }
        }
        private void CancelSideButton_Click(object sender, RoutedEventArgs e) { SideBack(); }// 当深色面板(darkpanel)被鼠标按下时的处理
        private void SideDarkPanel_MouseDown(object sender, MouseButtonEventArgs e) { SideBack(); }
        private void SideBack()
        {
            sideopen = false;
            // 隐藏进制转换展示面板
            sidedarkpanel.Visibility = Visibility.Collapsed;
            // 恢复标题栏背景透明
            Title.Background = Brushes.Transparent;
            ((MainWindow)Window.GetWindow(this)).SideBack();
        }

        public void SideCollapsed()
        {
            if (sideopen)
            {
                sideopen = false;
                SideBack();
            }
            else
            {
                ((MainWindow)Window.GetWindow(this)).SideCollapsed();
            }

        }


        private void SetButton_Clik(object sender, RoutedEventArgs e)
        {

        }
    }
}
