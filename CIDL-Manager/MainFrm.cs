using Dapper;
using HidSharp;
using HidSharp.Reports;
using HidSharp.Reports.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToastNotifications;

namespace CIDL_Manager
{
    public partial class MainFrm : Form
    {
        const string apiUrl = "http://172.20.24.164:1899/Home/SearchRecord";
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
            {
                WinApi.ShowToFront(this.Handle);
            }
            base.WndProc(ref message);
        }

        public MainFrm()
        {
            InitializeComponent();
            InitCallLogDataStorage();
            // When window state changed, trigger state update.
            this.Resize += SetMinimizeState;

            // When tray icon clicked, trigger window state change.       
            systemTrayIcon.Click += ToggleMinimizeState;
        }

        void Log(string text, bool newLine = true)
        {
            deviceLogTxt.Invoke(new Action(() =>
            {
                deviceLogTxt.AppendText(text + (newLine ? "\r\n" : ""));
            }));

        }

        string outputBuffer = "";
        void HandelInputEvent(Object sender, EventArgs e)
        {
            //Log("Handle Event Triggered", true);

            var ir = sender as HidDeviceInputReceiver;
            //ir.
            //var inputReportBuffer = new byte[device.GetMaxInputReportLength()];
            var inputReportBuffer = new byte[16];

            while (ir.TryRead(inputReportBuffer, 0, out _))
            {
                //Log("TryRead", true);
                var part = System.Text.Encoding.UTF8.GetString(inputReportBuffer, 0, inputReportBuffer.Length).Trim('\0');
                outputBuffer += part;
                Log(part, false);
                var slist = outputBuffer.Split('\n');
                if (slist.Length > 1)
                {
                    var bufferFlush = (slist[0]);
                    outputBuffer = (slist[1]);

                    ParseDeviceData(bufferFlush);
                }

            }
        }

        void ListenDevice(HidDevice device)
        {

            HidStream hidStream;
            if (device.TryOpen(out hidStream))
            {
                Log("Device opened!");


                using (hidStream)
                {
                   
                    var reportDescriptor = device.GetReportDescriptor();
                    var inputReceiver = reportDescriptor.CreateHidDeviceInputReceiver();
                    inputReceiver.Start(hidStream);
                    inputReceiver.Received += HandelInputEvent;
                    /*inputReceiver.Received += (sender, e) =>
                    {
                        var ir = sender as HidDeviceInputReceiver;
                        //ir.
                        //var inputReportBuffer = new byte[device.GetMaxInputReportLength()];
                        var inputReportBuffer = new byte[16];
                        Report report;
                        while (ir.TryRead(inputReportBuffer, 0, out report))
                        {
                            var part = System.Text.Encoding.UTF8.GetString(inputReportBuffer, 0, inputReportBuffer.Length).Trim('\0');
                            buffer += part;
                            Log(part, false);
                            var slist = buffer.Split('\n');
                            if (slist.Length > 1)
                            {
                                var bufferFlush = (slist[0]);
                                buffer = (slist[1]);

                                ParseDeviceData(bufferFlush);
                            }

                        }
                    };*/

                    while (true)
                    {
                        if (!inputReceiver.IsRunning) { Log("Device diconnected!"); break; } // Disconnected?

                        Thread.Sleep(1000);
                    }
                    inputReceiver.Received -= HandelInputEvent;

                }



            }
        }

        public Thread listenThread = null;

        void InitDeviceDetector()
        {
            var list = DeviceList.Local;
            list.Changed += (sender, e) =>
            {
                Thread.Sleep(2000);
                Log("Device list changed.");
                var hidDeviceList = list.GetHidDevices();
                var device = hidDeviceList.Where(d => d.VendorID == 0x16c0 && d.ProductID == 0x05df).FirstOrDefault();

                if (device != null)
                {
                    Log("Device found!");

                    if (listenThread != null) {
                        listenThread.Abort();
                        listenThread = null;
                    }

                    listenThread = new Thread(() =>
                    {
                        ListenDevice(device);
                    });
                    listenThread.Start();
/*
                    listenTask=Task.Run(() =>
                    {
                        ListenDevice(device);
                    });*/

                }

            };


            list.RaiseChanged();

        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.CallLog' table. You can move, or remove it, as needed.
            this.callLogTableAdapter.Fill(this.dataSet.CallLog);
            InitDeviceDetector();
           this.WindowState = FormWindowState.Minimized;

        }

        void ParseDeviceData(string data)
        {

            var m = Regex.Match(data, @"[*D](?<num>\d+)[C#]");
            if (m.Success)
            {
                var num = m.Groups["num"].Value;
                try
                {
                    LogNewCall(num);
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());

                }


                return;
            }


        }

        private void cmdTestTxb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ParseDeviceData(cmdTestTxb.Text);
                cmdTestTxb.Text = "";
            }

        }

        public class CallLogDto
        {
            public string CallerName { get; set; }
            public string PhoneNumber { get; set; }
            public string CallTime { get; set; }
        }

        List<CallLogDto> callLogList = new List<CallLogDto>();
        DataTable callLogDt = new DataTable();
        public void InitCallLogDataStorage()
        {
            //callLogDt = new DataTable();
            //callLogDt.Columns.Add("CallerName", System.Type.GetType("System.String"));
            //callLogDt.Columns.Add("PhoneNumber", System.Type.GetType("System.String"));
            //callLogDt.Columns.Add("CallTime", System.Type.GetType("System.String"));
            callLogDt.Columns.Add("CallerName", System.Type.GetType("System.String"));
            callLogDt.Columns.Add("PhoneNumber", System.Type.GetType("System.String"));
            callLogDt.Columns.Add("CallTime", System.Type.GetType("System.String"));

            callLogTableAdapter.Connection.Open();
        }

        async Task<T> PostJson<T>(string url, object request, Dictionary<string, string> headers = null)
        {



            string json = JsonConvert.SerializeObject(request);
            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        // do something with entry.Value or entry.Key
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }

                }


                var r = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                r.EnsureSuccessStatusCode();
                string response = await r.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
        }


        async Task<T> GetJson<T>(string url, Dictionary<string, string> headers = null)
        {

            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        // do something with entry.Value or entry.Key
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }

                }


                var r = await client.GetAsync(url);
                r.EnsureSuccessStatusCode();
                string response = await r.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        public string FindContactNameByNumber(string num)
        {
            var name = "";
            Task.Run(async () =>
            {
                JObject resObj = await PostJson<JObject>(apiUrl, new { tel = num });
                var foundRecord = resObj["data"].FirstOrDefault();
                if (foundRecord == null)
                {
                    foundRecord = await PostJson<JObject>(apiUrl, new { mobile = num });
                }

                if (foundRecord != null)
                {
                    name = $"{foundRecord["fname"]} {foundRecord["lname"]} / {foundRecord["unit"]} ";
                }

            }).Wait();

            return name;
        }


        public void LogNewCall(string num)
        {
            Log($"New Call Detected, Phone Number: {num}");


            var name = FindContactNameByNumber(num);
            var data = new CallLogDto
            {
                PhoneNumber = num,
                CallerName = name,
                CallTime = DateTime.Now.ToString("yyyy/MM/dd - HH:mm:ss", new CultureInfo("fa-IR"))

            };


            callLogTableAdapter.Insert(data.CallerName, data.PhoneNumber, data.CallTime);
            //callLogList.Add(data);


            this.Invoke(new Action(() =>
            {
                //callLogGrd.DataSource = callLogList.ToList();
                callLogTableAdapter.Fill(dataSet.CallLog);
                var toastNotification = new Notification($"تماس جدید از `{data.PhoneNumber}`", $"{data.CallTime}\n{data.CallerName}", -1, FormAnimator.AnimationMethod.Slide, FormAnimator.AnimationDirection.Up, false);
                //PlayNotificationSound(comboBoxSound.Text);
                toastNotification.Show();

            }));

            //callLogGrd.DataSource = callLogDt;




        }

        // Toggle state between Normal and Minimized.
        private void ToggleMinimizeState(object sender, EventArgs e)
        {
            bool isMinimized = this.WindowState == FormWindowState.Minimized;
            this.WindowState = (isMinimized) ? FormWindowState.Normal : FormWindowState.Minimized;
        }

        // Show/Hide window and tray icon to match window state.
        private void SetMinimizeState(object sender, EventArgs e)
        {
            bool isMinimized = this.WindowState == FormWindowState.Minimized;

            this.ShowInTaskbar = !isMinimized;
            systemTrayIcon.Visible = isMinimized;
            //if (isMinimized) systemTrayIcon.ShowBalloonTip(500, "Application", "Application minimized to tray.", ToolTipIcon.Info);
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
           // SingleInstance.Stop();
        }
    }
}
