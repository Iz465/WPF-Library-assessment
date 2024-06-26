using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Library_assessment
{
  
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
      
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;

            FileStream fileStream = new FileStream("console.log", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream) { AutoFlush = true };
            Console.SetOut(streamWriter);

           
        }
    }
}
