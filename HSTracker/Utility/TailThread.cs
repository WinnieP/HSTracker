using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Windows.Forms;
using System.IO;

/// 

/// This code is heavily adapted and modified from the example at
/// http://www.csharphelp.com/2006/07/a-c-tail/
/// The original code is posted with no explicit or license, so public domain is implied.
/// 
namespace Utility.TailThread
{
    /// 

    /// This delegate stands in for a method which appends the given string to an output text control.
    /// 

    /// 
    public delegate void AppendTextDelegate(string appendString);

    public class TailThread
    {
        private readonly string filePath;
        private readonly AppendTextDelegate callback;

        // these variables are defined when the TailThread is running, null when not
        private Thread runThread;
        private AsynchronousFileMonitor fileMonitor;

        public TailThread(string filePath, AppendTextDelegate callback)
        {
            this.filePath = filePath;
            this.callback = callback;
        }

        internal void Start()
        {
            if (runThread != null) return; // thread is running, so do nothing

            fileMonitor = new AsynchronousFileMonitor(filePath, callback);
            runThread = new Thread(new ThreadStart(fileMonitor.DoMonitoring));
            runThread.Start();
        }

        public void Stop()
        {
            fileMonitor.StopMonitoring();
            fileMonitor = null;
            runThread = null;
        }
    }

    /// 

    /// Summary description for Monitor.
    /// 

    class AsynchronousFileMonitor
    {
        public AsynchronousFileReader asyncFileReader;
        private FileSystemWatcher watcher;

        private string fileDir;
        private string fileName;
        private AppendTextDelegate appendText;

        public AsynchronousFileMonitor(string filePath, AppendTextDelegate appendTextDelegate)
        {
            appendText = appendTextDelegate;
            FindPathAndFile(filePath);
            Console.WriteLine(fileDir);
            Console.WriteLine(fileName);
        }

        public void FindPathAndFile(string filePath)
        {
            string[] elements = filePath.Split(new char[] { '\\' });

            fileName = elements[elements.Length - 1];
            for (int i = 0; i < elements.Length - 1; i++)
                fileDir += elements[i] + '\\';
        }

        // This method reads the file data from the stream and prints the output
        public void ReadAndPrintFromFile(int i)
        {
            string input;

            if (i != 0)
            {
                asyncFileReader.fileStream.Position = asyncFileReader.fileStream.Length - i;
            }

            while ((input = asyncFileReader.ReadAsynchronous()) != null)
            {
                appendText(input + "\n");
                Console.WriteLine(input + "\n");
            }
        }

        public void DoMonitoring()
        {
            try
            {
                // Open the file
                asyncFileReader = new AsynchronousFileReader(fileDir + fileName);

                // Create a new FileSystemWatcher and set its properties.
                watcher = new FileSystemWatcher();

                watcher.Path = fileDir;                                   // set the path
                watcher.Filter = fileName;                                // set the file
                watcher.NotifyFilter = NotifyFilters.Size;                // look for size change
                watcher.Changed += new FileSystemEventHandler(OnChanged); // Add event handler(s)
                watcher.EnableRaisingEvents = true;                       // Begin watching

                ReadAndPrintFromFile(1000);                               // Perform an initial read
            }

            catch (Exception _e)
            {
                Console.WriteLine(_e.ToString());
            }
        }

        public void StopMonitoring()
        {
            watcher.EnableRaisingEvents = false;
            asyncFileReader.Close();
        }

        #region Event handlers

        // Event handler for file changed.  This causes all the work to be done.
        public void OnChanged(object source, FileSystemEventArgs e)
        {
            ReadAndPrintFromFile(0);
        }

        // Event handler for thread abort
        public void OnThreadException(object sender, ThreadExceptionEventArgs te)
        {
            appendText(te.ToString() + "\n");
            StopMonitoring();
        }

        #endregion Event handlers
    }

    /// 

    /// This class encapsulates a file stream for asynchronous reads.
    /// 

    class AsynchronousFileReader
    {
        public FileStream fileStream;
        public StreamReader fileStreamReader;

        public AsynchronousFileReader(string filePath)
        {
            // Open the file for asynchronous read
            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1, true);
            // attach a StreamReader to the file stream
            fileStreamReader = new StreamReader(fileStream);
        }

        public string ReadAsynchronous()
        {
            return (fileStreamReader.ReadLine());
        }

        public void Close()
        {
            fileStreamReader.Close();
            fileStream.Close();
        }
    }
}
