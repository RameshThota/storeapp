using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bg.WorkerReportsProgress = true;
            bg.DoWork += bg_DoWork;
            bg.ProgressChanged += bg_ProgressChanged;
            bg.WorkerSupportsCancellation = true;
            bg1.WorkerReportsProgress = true;
            bg1.DoWork += bg1_DoWork;
            bg1.ProgressChanged += bg1_ProgressChanged;
        }
         void bg1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }


        //You Can Write The Do_work Method In The Following Two Ways
        void bg1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Instead Of this Thread.sleep(5000) Statement,We Can Write Our Methods Here To Work Concurrently
             Thread.Sleep(5000);
            bg.ReportProgress(100);
            bg.CancelAsync();
        }

     

       //void bg1_DoWork(object sender, DoWorkEventArgs e)
         //{
         //    //In This We Are Approach We Are Directly Calling TheDo_Work Method Which Consists Of Our Bussiness Logic Statements/Code
         //    ExecutableStatementsMethod();
         //    bg.ReportProgress(100);
         //    bg.CancelAsync();
         //}

         //private void ExecutableStatementsMethod()
         //{
         //    Thread.Sleep(5000);
         //    MessageBox.Show("5 Secands Completed Now The Progressbar Completes It's Duty");
         //}


        void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           Progress.Value = e.ProgressPercentage;
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
           
            for (int i = 0; i <= 100; i++)
            {
                
                // Report progress to 'UI' thread
                bg.ReportProgress(i);
                // Simulate long task
                System.Threading.Thread.Sleep(100);

                if (bg.CancellationPending == true)
                {
                    e.Cancel = true;
                    return; // this will fall to the finally and close everything    
                } 
            }
            
          
        }

        BackgroundWorker bg =new BackgroundWorker();
        BackgroundWorker bg1 = new BackgroundWorker();
        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bg.RunWorkerAsync();
          
            bg1.RunWorkerAsync();
           
            //Duration duration = new Duration(TimeSpan.FromSeconds(20));
            //DoubleAnimation doubleanimation = new DoubleAnimation(200.0, duration);
            //Progress.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            //Progress.Maximum
        }
    }
    
}
