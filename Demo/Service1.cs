using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace Demo
{
    public partial class Service1 : ServiceBase
    {
        Timer timerDemo = new Timer();
        DateTime endtime = DateTime.Now;
        DateTime starttime = DateTime.Now;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            WriteLog("Bật máy lúc mấy giờ " + starttime); // ghi log thời gian bật máy 
            timerDemo.Elapsed += new ElapsedEventHandler(TimeInterval);
            timerDemo.Interval = 1000; //10 s ghi log một lần 
            
            timerDemo.Enabled = true;
            

        }
        private void TimeInterval(object source, ElapsedEventArgs e)
        {
            WriteLog("sau bao nhiêu giây thì tạo mới thời gian" + DateTime.Now);
        }
        private void counttime()
        {
            TimeSpan counttime = endtime - starttime;
            WriteLog("tổng số giờ hoạt động" + counttime);
        }

        protected override void OnStop()
        {

            WriteLog("Tắt máy lúc nào " + endtime);
            counttime();
        }

        private void WriteLog(string message)// hàm ghi log 
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string file = path + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file))
                {
                    sw.WriteLine(message);
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(file))
                {
                    sw.WriteLine(message);
                }
            }

        }
    }
}
