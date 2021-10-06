using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace TimerApp
{
    public enum ShowState
    {
        Start,
        Normal,
        Invalid
    }

    public partial class DisplayForm : Form
    {
        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        private Screen screen;
        private Graphics graphics;
        //private PointF position;
        private SizeF size;
        private Brush brush;
        private readonly GraphicsPath gpath;
        private Pen pen;
        private string paintText;
        private Point xpoint;
        private static readonly string keyPath = $"{Constants.CompanyName}\\{Constants.CommonName}";
        private bool configChanged;
        private readonly string configPath;
        private readonly string configFolder;
        private const string configName = "CountDown.json";
        private ShowState showState = ShowState.Start;
        private bool noPaint = true;
        private Settings config;
        private readonly Logger logger = new Logger(Constants.CommonName);
        private bool mouseDown;
        private Point lastLocation;

        public DisplayForm()
        {
            InitializeComponent();

            gpath = new GraphicsPath();
            configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), keyPath);
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
            configPath = Path.Combine(configFolder, configName);

            SetParameters();
        }

        private void SetParameters()
        {
            Text = Constants.CommonName;
            TransparencyKey = Color.FromArgb(255, 02, 02, 02);
            SetupWatchers();

            var mydir = Directory.GetCurrentDirectory();
            string iconFilePath = Path.Combine(mydir, "hourglass_icon.ico");
            if (File.Exists(iconFilePath)) Icon = new Icon(iconFilePath);

            LoadConfig();
        }

        private void SetupWatchers()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(configFolder);
            watcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = configName;
            watcher.IncludeSubdirectories = false ;
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            configChanged = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            configChanged = true;
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            config = null;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!e.Name.Equals(configName, StringComparison.OrdinalIgnoreCase))
            {
                config = null;
            }
            configChanged = true;
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            config = null;
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string jsonText = File.ReadAllText(configPath);
                    Settings[] settings = JsonConvert.DeserializeObject<Settings[]>(jsonText);
                    config = settings?.FirstOrDefault();
                }

                Color color = config?.Color ?? SystemColors.ControlText;
                brush = new SolidBrush(color);

                Color penColor;
                double intensity = ColorToIntensity(color);
                if (intensity < 65)
                    penColor = Color.White;
                else
                    penColor = Color.Black;

                pen = new Pen(penColor, 1f);

                if (config == null)
                {
                    return;
                }

                Dictionary<string, Screen> screens = new Dictionary<string, Screen>(StringComparer.OrdinalIgnoreCase);
                foreach (var s in Screen.AllScreens)
                {
                    screens.Add(s.DeviceName, s);
                }

                if (config.Monitor != null)
                {
                    if (screens.ContainsKey(config.Monitor))
                    {
                        screen = screens[config.Monitor];
                    }
                    else
                    {
                        screen = Screen.PrimaryScreen;
                    }
                }

                IntPtr hwnd = GetDesktopWindow();
                if (hwnd != IntPtr.Zero)
                {
                    IntPtr hdc = GetDC(hwnd);
                    if (hdc != IntPtr.Zero)
                    {
                        graphics = Graphics.FromHdc(hdc);
                    }
                }

                string test = GetText();
                size = graphics.MeasureString(test, config.Font);

                size.Width += 8;
                size.Height += 8;

                if (config.Override.HasValue)
                {
                    Location = config.Override.Value;
                }
                else
                {
                    PointF position = new PointF();

                    switch (config.ScreenPosition)
                    {
                        case ScreenPosition.TopLeft:
                            position.X = screen.WorkingArea.Left;
                            position.Y = screen.WorkingArea.Top;
                            break;
                        case ScreenPosition.TopCenter:
                            position.X = screen.Bounds.X + screen.WorkingArea.Width / 2 - size.Width / 2;
                            position.Y = screen.WorkingArea.Top;
                            break;
                        case ScreenPosition.TopRight:
                            position.X = screen.Bounds.X + screen.WorkingArea.Width - size.Width;
                            position.Y = screen.WorkingArea.Top;
                            break;
                        case ScreenPosition.MiddleLeft:
                            position.X = screen.WorkingArea.Left;
                            position.Y = screen.Bounds.Y + screen.WorkingArea.Height / 2 - size.Height / 2;
                            break;
                        case ScreenPosition.MiddleCenter:
                            position.X = screen.Bounds.X + screen.WorkingArea.Width / 2 - size.Width / 2;
                            position.Y = screen.Bounds.Y + screen.WorkingArea.Height / 2 - size.Height / 2;
                            break;
                        case ScreenPosition.MiddleRight:
                            position.X = screen.Bounds.X + screen.WorkingArea.Width - size.Width;
                            position.Y = screen.Bounds.Y + screen.WorkingArea.Height / 2 - size.Height / 2;
                            break;
                        case ScreenPosition.BottomLeft:
                            position.X = screen.WorkingArea.Left;
                            position.Y = screen.Bounds.Y + screen.WorkingArea.Height - size.Height;
                            break;
                        case ScreenPosition.BottomCenter:
                            position.X = screen.Bounds.X + screen.WorkingArea.Width / 2 - size.Width / 2;
                            position.Y = screen.Bounds.Y + screen.WorkingArea.Height - size.Height;
                            break;
                        case ScreenPosition.BottomRight:
                            position.X = screen.Bounds.X + screen.WorkingArea.Width - size.Width;
                            position.Y = screen.Bounds.Y + screen.WorkingArea.Height - size.Height;
                            break;
                    }

                    Location = Point.Truncate(position);
                }

                Color transparencyKey = Color.FromArgb(255, 02, 02, 02);
                StartPosition = FormStartPosition.Manual;
                Size = Size.Truncate(size);
                BackColor = transparencyKey;
                TransparencyKey = transparencyKey;
                xpoint = new Point(Padding.Left, Padding.Top);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                config = null;
            }
            finally
            {
                noPaint = false;
            }
        }

        private double ColorToIntensity(Color c)
        {
            return 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
        }

        private SizeF MeasureString()
        {
            Graphics g = null;
            try
            {
                IntPtr hwnd = GetDesktopWindow();
                if (hwnd != IntPtr.Zero)
                {
                    IntPtr hdc = GetDC(hwnd);
                    if (hdc != IntPtr.Zero)
                    {
                        g = Graphics.FromHdc(hdc);
                    }
                }

                if (g == null)
                {
                    return SizeF.Empty;
                }

                string test = GetText();
                return g.MeasureString(test, config?.Font ?? SystemFonts.DialogFont);
            }
            finally
            {
                if (g != null) g.Dispose();
            }
        }

        private string GetText()
        {
            if (config == null)
            {
                return "CountDown: Not configured yet - or invalid configuration";
            }

            TimeSpan span = config.EndDate - DateTime.Now;
            if (span <= TimeSpan.Zero)
            {
                return config.FinishText;
            }

            StringBuilder sb = new StringBuilder(config.DisplayText);
            sb.Append(" ");

            int start = int.MaxValue;
            int end = int.MinValue;

            foreach (char ch in config.Format)
            {
                int index = 99;

                switch (char.ToLower(ch))
                {
                    case 'd':
                        index = 0;
                        break;
                    case 'h':
                        index = 1;
                        break;
                    case 'm':
                        index = 2;
                        break;
                    case 's':
                        index = 3;
                        break;
                }

                if (index < start) start = index;
                if (index > end) end = index;
            }

            double totalSeconds = span.TotalSeconds;

            for (int index = 0; index < config.Format.Length; index++)
            {
                bool isFirst = index == 0;
                bool isLast = index == (config.Format.Length - 1);
                char ch = config.Format[index];
                string t = "";

                switch (char.ToLower(ch))
                {
                    case 'd':
                        double days = Math.Truncate(totalSeconds / 86400);
                        totalSeconds -= days * 86400;

                        if (isLast)
                        {
                            t = $"Days:{span.TotalDays:0.000}";
                        }
                        else
                        {
                            int d = Convert.ToInt32(days);
                            t = d.ToString();
                        }
                        break;
                    case 'h':
                        double hours = totalSeconds / 3600;
                        int h = Convert.ToInt32(Math.Truncate(hours));
                        totalSeconds -= h * 3600;

                        if (isFirst && isLast)
                        {
                            t = $"Hours:{span.TotalHours:0.00}";
                        }
                        else if (isLast)
                        {
                            t = $"{hours:0.0}";
                        }
                        else
                        {
                            t = h.ToString("00");
                        }
                        break;
                    case 'm':
                        double minutes = totalSeconds / 60;
                        int m = Convert.ToInt32(Math.Truncate(minutes));
                        totalSeconds -= m * 60;

                        if (isFirst && isLast)
                        {
                            t = $"Minutes:{span.TotalMinutes:0.0}";
                        }
                        else if (isLast)
                        {
                            t = $"{minutes:0.0}";
                        }
                        else
                        {
                            t = m.ToString("00");
                        }
                        break;
                    case 's':
                        double seconds = totalSeconds;
                        int s = Convert.ToInt32(Math.Truncate(seconds));

                        if (isFirst && isLast)
                        {
                            t = $"Seconds:{span.TotalSeconds:0}";
                        }
                        else
                        {
                            t = s.ToString("00");
                        }
                        break;
                }

                if (!isFirst) sb.Append(":");
                sb.Append(t);
            }

            return sb.ToString();
        }

        private void FormSetup()
        {
            if (configChanged)
            {
                LoadConfig();
            }

            if (config == null)
            {
                if (showState != ShowState.Invalid)
                {
                    int x = Screen.PrimaryScreen.Bounds.X + Screen.PrimaryScreen.WorkingArea.Width / 2 - Size.Width / 2;
                    int y = Screen.PrimaryScreen.Bounds.Y + Screen.PrimaryScreen.WorkingArea.Height / 2 - Size.Height / 2;
                    Size textSize = Size.Truncate(MeasureString());

                    BackColor = SystemColors.Control;
                    FormBorderStyle = FormBorderStyle.FixedSingle;
                    ControlBox = true;
                    Location = new Point(x, y);
                    Size = new Size(400, 80);

                    xpoint = new Point(ClientSize.Width / 2 - textSize.Width / 2, ClientSize.Height / 2 - textSize.Height /2);
                    showState = ShowState.Invalid;
                }

                return;
            }

            if (showState != ShowState.Normal)
            {
                BackColor = TransparencyKey;
                FormBorderStyle = FormBorderStyle.None;
                ControlBox = false;
                StartPosition = FormStartPosition.Manual;
                Size = Size.Truncate(size);

                xpoint = new Point(Padding.Left, Padding.Top);
                showState = ShowState.Normal;
            }
        }

        private void timerDisplay_Tick(object sender, EventArgs e)
        {
            try
            {
                FormSetup();

                string text = GetText();
                if (text != paintText || configChanged)
                {
                    paintText = text;
                    Invalidate();
                }
            }
            finally
            {
                configChanged = false;
            }
        }

        private void DisplayForm_Paint(object sender, PaintEventArgs e)
        {
            if (noPaint) return;

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            Font font = config?.Font ?? SystemFonts.DialogFont;
            float fontSize = e.Graphics.DpiY * font.SizeInPoints / 72;

            gpath.Reset();
            gpath.AddString(
                paintText ?? "",
                font.FontFamily,
                (int)font.Style,
                fontSize,
                xpoint,
                StringFormat.GenericTypographic);

            // And finally, using our pen, all we have to do now
            //  is draw our graphics path to the screen. Voila!
            e.Graphics.FillPath(brush, gpath);
            if (fontSize > 14) e.Graphics.DrawPath(pen, gpath);
        }

        private void DisplayForm_Shown(object sender, EventArgs e)
        {
            Continue();
        }

        public void Pause()
        {
            timerDisplay.Stop();
        }

        private void Continue()
        {
            timerDisplay.Start();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            notifyIconDisplay.ContextMenuStrip = contextMenuStripDisplay;
        }

        private void DisplayForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (config == null) return;
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void DisplayForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(Location.X - lastLocation.X + e.X, Location.Y - lastLocation.Y + e.Y);
                Update();
            }
        }

        private void DisplayForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            if (config == null) return;
            config.Override = Location;
            SaveConfig();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = null;

            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                if (string.IsNullOrEmpty(baseDir))
                {
                    msg = "Could not find the base directory";
                    logger.Error($"[Config] {msg}");
                    return;
                }

                string configToolPath = Path.Combine(baseDir, $"{Constants.CommonName}Config.exe");
                if (!File.Exists(configToolPath))
                {
                    msg = $"Could not find config application: {configToolPath}";
                    logger.Warn($"[Config] {msg}");
                    return;
                }

                Process.Start(configToolPath);
            }
            finally
            {
                if (msg != null) MessageBox.Show(msg, Constants.CommonName);
            }
        }

        private void SaveConfig()
        {
            if (config == null) return;
            File.WriteAllText(configPath, JsonConvert.SerializeObject(new[] { config }, Formatting.Indented));
        }
    }
}
