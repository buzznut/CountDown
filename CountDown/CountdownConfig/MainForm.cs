using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace TimerConfig
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        private Font font = SystemFonts.MessageBoxFont;
        private Color color = Color.Black;
        private static readonly string keyPath = $"{Constants.CompanyName}\\{Constants.CommonName}";
        ScreenPosition screenPosition = ScreenPosition.MiddleCenter;
        private bool isConfig;
        readonly Dictionary<string, RectangleF> rectByName = new Dictionary<string, RectangleF>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<int, string> nameByIndex = new Dictionary<int, string>();
        private string screenName;
        private Font paintFont;
        private Brush brush;
        private readonly GraphicsPath gpath;
        private Pen pen;
        private string paintText;
        private Point xpoint;

        public MainForm()
        {
            InitializeComponent();

            gpath = new GraphicsPath();
            LoadConfig();
            buttonApply.Enabled = false;
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (!SaveConfig())
            {
                DialogResult result = MessageBox.Show("Could not save configuration. Exit anyway?", "Quit", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;
            }

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (SaveConfig())
            {
                ManageChange(false);
            }
        }

        private void ContentChanged(object sender, EventArgs e)
        {
            ManageChange(true);
        }

        private void ManageChange(bool hasChanges)
        {
            bool validated = ValidateDateTime();
            buttonApply.Enabled = hasChanges && validated;
            buttonOkay.Enabled = validated;
            paintText = GetDisplayText();
            Size textSize = Size.Truncate(MeasureString(paintText));
            xpoint = new Point(pictureBoxText.Width / 2 - textSize.Width / 2, pictureBoxText.Height / 2 - textSize.Height /2);

            Color penColor;
            double intensity = ColorToIntensity(color);
            if (intensity < 65)
                penColor = Color.White;
            else
                penColor = Color.Black;

            pen = new Pen(penColor, 1f);
            pictureBoxText.Invalidate();
        }

        private bool ValidateDateTime()
        {
            string dtText = $"{textBoxDate.Text} {textBoxTime.Text}";
            bool hasDate = DateTime.TryParse(dtText, out DateTime _);                                           
            return hasDate;
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = font;
            fd.Color = color;
            fd.ShowColor = true;
            bool hasChanges = false;

            string ft = JsonConvert.SerializeObject(font);
            string ct = JsonConvert.SerializeObject(color);

            DialogResult result = fd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fn = JsonConvert.SerializeObject(fd.Font);
                if (fn != ft)
                {
                    font = fd.Font;
                    hasChanges = true;
                }

                string cn = JsonConvert.SerializeObject(fd.Color);
                if (ct != cn)
                {
                    color = fd.Color;
                    brush = new SolidBrush(color);
                    hasChanges = true;
                }
            }

            if (hasChanges) ManageChange(true);
        }

        private double ColorToIntensity(Color c)
        {
            return 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
        }

        private SizeF MeasureString(string test)
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

                return g.MeasureString(test, font ?? SystemFonts.DialogFont);
            }
            finally
            {
                if (g != null) g.Dispose();
            }
        }

        private string GetDisplayText()
        {
            return $"'{textBoxDisplay.Text}' or '{textBoxFinished.Text}'";
        }

        private void LoadConfig()
        {
            isConfig = true;
            try
            {
                Settings config = null;

                string configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), keyPath);
                if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
                string path = Path.Combine(configFolder, "CountDown.json");
                if (File.Exists(path))
                {
                    string jsonText = File.ReadAllText(path);
                    Settings[] settings = JsonConvert.DeserializeObject<Settings[]>(jsonText);
                    config = settings?.FirstOrDefault();
                }

                if (config != null)
                {
                    textBoxDisplay.Text = config.DisplayText;
                    textBoxFinished.Text = config.FinishText;

                    textBoxDate.Text = config.EndDate.ToString("MM/dd/yyyy");
                    textBoxTime.Text = config.EndDate.ToString("hh:mm:ss tt");

                    font = config.Font;

                    color = config.Color;

                    Color penColor;
                    double intensity = ColorToIntensity(color);
                    if (intensity < 127)
                        penColor = Color.White;
                    else
                        penColor = Color.Black;

                    pen = new Pen(penColor, 1f);
                    brush = new SolidBrush(color);

                    paintText = GetDisplayText();

                    screenPosition = config.ScreenPosition;

                    switch (screenPosition)
                    {
                        case ScreenPosition.TopLeft:
                            radioButtonTopLeft.Checked = true;
                            break;
                        case ScreenPosition.TopCenter:
                            radioButtonTopCenter.Checked = true;
                            break;
                        case ScreenPosition.TopRight:
                            radioButtonTopRight.Checked = true;
                            break;
                        case ScreenPosition.MiddleLeft:
                            radioButtonMiddleLeft.Checked = true;
                            break;
                        case ScreenPosition.MiddleCenter:
                            radioButtonMiddleCenter.Checked = true;
                            break;
                        case ScreenPosition.MiddleRight:
                            radioButtonMiddleRight.Checked = true;
                            break;
                        case ScreenPosition.BottomLeft:
                            radioButtonBottomLeft.Checked = true;
                            break;
                        case ScreenPosition.BottomCenter:
                            radioButtonBottomCenter.Checked = true;
                            break;
                        case ScreenPosition.BottomRight:
                            radioButtonBottomRight.Checked = true;
                            break;
                    }

                    screenName = config.Monitor;
                    string formatText = config.Format;

                    if (string.IsNullOrEmpty(formatText)) formatText = "";

                    foreach (char ch in formatText)
                    {
                        switch (char.ToLower(ch))
                        {
                            case 'd':
                                checkBoxDays.Checked = true;
                                break;
                            case 'h':
                                checkBoxHours.Checked = true;
                                break;
                            case 'm':
                                checkBoxMinutes.Checked = true;
                                break;
                            case 's':
                                checkBoxSeconds.Checked = true;
                                break;
                        }
                    }
                }
                else
                {
                    radioButtonMiddleCenter.Checked = true;
                    screenPosition = ScreenPosition.MiddleCenter;
                }

                checkBoxOverridden.Checked = config?.Override != null;
                textBoxOverride.Text = config?.Override != null ? JsonConvert.SerializeObject(config.Override) : "";
                if (!checkBoxOverridden.Checked)
                {
                    checkBoxOverridden.Enabled = false;
                    textBoxOverride.Enabled = false;
                }
            }
            finally
            {
                DrawScreens();
                isConfig = false;
                ManageChange(true);
            }
        }

        private void DrawScreens()
        {
            int xmin = int.MaxValue;
            int ymax = int.MinValue;
            int xmax = int.MinValue;
            int ymin = int.MaxValue;

            Dictionary<string, Screen> screens = new Dictionary<string, Screen>(StringComparer.OrdinalIgnoreCase);
            foreach (var screen in Screen.AllScreens)
            {
                screens.Add(screen.DeviceName, screen);
            }

            foreach (var screen in screens.Values)
            {
                int a = Math.Min(screen.Bounds.Left, screen.Bounds.Right);
                int b = Math.Max(screen.Bounds.Left, screen.Bounds.Right);
                if (a < xmin) xmin = a;
                if (b > xmax) xmax = b;

                a = Math.Min(screen.Bounds.Top, screen.Bounds.Bottom);
                b = Math.Max(screen.Bounds.Top, screen.Bounds.Bottom);
                if (a < ymin) ymin = a;
                if (b > ymax) ymax = b;
            }

            // scale the picture box to the bounds
            int width = xmax - xmin;
            int height = ymax - ymin;

            float scaleX = pictureBox.Width / (float)width;
            float scaleY = pictureBox.Height / (float)height;

            float scale = Math.Min(scaleX, scaleY);

            height = Round(height * scale);
            width = Round(width * scale);
            int offsetX = 0 - xmin;
            int offsetY = 0 - ymin;

            List<string> orderList = new List<string>();
            foreach (var screen in screens.Values)
            {
                int x = offsetX + screen.Bounds.X + (screen.Bounds.Right - screen.Bounds.Left) / 2;
                int y = offsetY + screen.Bounds.Y + (screen.Bounds.Bottom - screen.Bounds.Top) / 2;

                string order = $"{y:00000}.{x:00000}";
                orderList.Add(order);
            }

            orderList.Sort();

            Bitmap image = (width > 0 && height > 0) ? new Bitmap(width + 1, height + 1) : null;
            if (image == null) return;

            // draw rectangles to match the screens in the picture box
            if (paintFont != null) paintFont.Dispose();
            paintFont = new Font("Arial Narrow", (float)(height / 3.0), FontStyle.Regular, GraphicsUnit.Pixel);
            rectByName.Clear();
            nameByIndex.Clear();

            for (int ii = 0; ii < orderList.Count; ii++)
            {
                int index = ii + 1;

                string[] values = orderList[ii].Split('.');
                int ptx = int.Parse(values[1]);
                int pty = int.Parse(values[0]);

                Point pt = new Point(ptx - offsetX, pty - offsetY);
                Screen screen = Screen.FromPoint(pt);
                if (screen != null)
                {
                    float x = 1 + (screen.Bounds.X + offsetX) * scale;
                    float y = 1 + (screen.Bounds.Y + offsetY) * scale;
                    float w = screen.Bounds.Width * scale - 2;
                    float h = screen.Bounds.Height * scale - 2;

                    RectangleF rect = new RectangleF(x, y, w, h);
                    rectByName.Add(screen.DeviceName, rect);
                    nameByIndex[index] = screen.DeviceName;
                }
            }

            if (pictureBox.Image != null) pictureBox.Image.Dispose();
            pictureBox.Image = image;
            pictureBox.Invalidate();
        }

        private int Round(double v)
        {
            return Convert.ToInt32(Math.Truncate(Math.Round(v)));
        }

        private bool SaveConfig()
        {
            string dtText = $"{textBoxDate.Text} {textBoxTime.Text}";
            DateTime dt;
            if (!DateTime.TryParse(dtText, out dt))
            {
                MessageBox.Show($"Could not parse date time: {dtText}");
                return false;
            }

            StringBuilder formatText = new StringBuilder();
            if (checkBoxDays.Checked) formatText.Append('D');
            if (checkBoxHours.Checked) formatText.Append('H');
            if (checkBoxMinutes.Checked) formatText.Append('M');
            if (checkBoxSeconds.Checked) formatText.Append('S');
            if (formatText.Length == 0) formatText.Append('H');

            Point? o = null;
            if (!string.IsNullOrEmpty(textBoxOverride.Text) && checkBoxOverridden.Checked)
            {
                o = JsonConvert.DeserializeObject<Point>(textBoxOverride.Text);
            }

            Settings settings = new Settings
            {
                DisplayText = textBoxDisplay.Text,
                FinishText = textBoxFinished.Text,
                EndDate = dt,
                Monitor = screenName,
                Format = formatText.ToString(),
                ScreenPosition = screenPosition,
                Font = font,
                Color = color,
                Override = o
            };

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), keyPath, "CountDown.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(new[] { settings }, Formatting.Indented));

            return true;
        }

        private void radioButtonScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (isConfig) return;
            ManageChange(true);
            RadioButton rb = sender as RadioButton;
            if (rb == null) return;
            if (!rb.Checked) return;

            int value;
            if (int.TryParse(rb.Tag as string, out value))
            {
                screenPosition = (ScreenPosition)value;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            DrawScreens();
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            string lastScreen = screenName;

            foreach (string name in rectByName.Keys)
            {
                RectangleF rect = rectByName[name];
                if (rect.Contains(e.Location))
                {
                    screenName = name;
                    break;
                }
            }

            if (lastScreen != screenName)
            {
                pictureBox.Invalidate();
                ManageChange(true);
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (int index in nameByIndex.Keys)
            {
                string name = nameByIndex[index];
                RectangleF rect = rectByName[name];

                g.FillRectangle(name == screenName ? Brushes.LightBlue : Brushes.White, rect);
                g.DrawRectangle(Pens.Black, Rectangle.Truncate(rect));

                float fx = rect.X + ((rect.Width - paintFont.Size) / 2);
                float fy = rect.Y + ((rect.Height - paintFont.GetHeight()) / 2);

                // put the device numbers in each rectangle
                g.DrawString($"{index}", paintFont, name == screenName ? Brushes.White : Brushes.Black, fx, fy);
            }
        }

        private void checkBoxDisplayFormat_Changed(object sender, EventArgs e)
        {
            ManageChange(true);
        }

        private void checkBoxOverridden_CheckedChanged(object sender, EventArgs e)
        {
            ManageChange(true);
        }

        private void pictureBoxText_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

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
    }
}
