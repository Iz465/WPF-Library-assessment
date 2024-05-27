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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Call the base method to ensure that the default startup behavior is preserved
            base.OnStartup(e);

            // Redirect console output to a file
            FileStream fileStream = new FileStream("console.log", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream) { AutoFlush = true };
            Console.SetOut(streamWriter);

            // Now, any output to the console will be written to the file "console.log"
        }
    }
}
