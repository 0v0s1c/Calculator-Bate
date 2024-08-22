using calculator.View;
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
using static Calculator.MainWindow;

namespace Calculator.NewPage
{
    /// <summary>
    /// BelowPage.xaml 的交互逻辑
    /// </summary>
    public partial class BelowPage : Page
    {
        public BelowPage()
        {
            InitializeComponent();
        }
        public void delete_Click(object sender, RoutedEventArgs e)
        {

        }

        ConcisePage concisePage=new ConcisePage();
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            r = concisePage.r;
        }
    }
}
