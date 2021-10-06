
namespace TimerConfig
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOkay = new System.Windows.Forms.Button();
            this.labelEndTime = new System.Windows.Forms.Label();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.buttonFont = new System.Windows.Forms.Button();
            this.labelPosition2 = new System.Windows.Forms.Label();
            this.radioButtonTopLeft = new System.Windows.Forms.RadioButton();
            this.radioButtonTopRight = new System.Windows.Forms.RadioButton();
            this.radioButtonTopCenter = new System.Windows.Forms.RadioButton();
            this.radioButtonMiddleCenter = new System.Windows.Forms.RadioButton();
            this.radioButtonMiddleRight = new System.Windows.Forms.RadioButton();
            this.radioButtonMiddleLeft = new System.Windows.Forms.RadioButton();
            this.radioButtonBottomCenter = new System.Windows.Forms.RadioButton();
            this.radioButtonBottomRight = new System.Windows.Forms.RadioButton();
            this.radioButtonBottomLeft = new System.Windows.Forms.RadioButton();
            this.labelPosition1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labelBox = new System.Windows.Forms.Label();
            this.checkBoxDays = new System.Windows.Forms.CheckBox();
            this.checkBoxHours = new System.Windows.Forms.CheckBox();
            this.checkBoxMinutes = new System.Windows.Forms.CheckBox();
            this.checkBoxSeconds = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFinished = new System.Windows.Forms.TextBox();
            this.checkBoxOverridden = new System.Windows.Forms.CheckBox();
            this.textBoxOverride = new System.Windows.Forms.TextBox();
            this.pictureBoxText = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxText)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(385, 443);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(70, 23);
            this.buttonApply.TabIndex = 28;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(537, 443);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 23);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOkay
            // 
            this.buttonOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOkay.Enabled = false;
            this.buttonOkay.Location = new System.Drawing.Point(461, 443);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(70, 23);
            this.buttonOkay.TabIndex = 29;
            this.buttonOkay.Text = "OK";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // labelEndTime
            // 
            this.labelEndTime.AutoSize = true;
            this.labelEndTime.Location = new System.Drawing.Point(13, 39);
            this.labelEndTime.Name = "labelEndTime";
            this.labelEndTime.Size = new System.Drawing.Size(65, 13);
            this.labelEndTime.TabIndex = 2;
            this.labelEndTime.Text = "Ending time:";
            this.labelEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(13, 14);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(67, 13);
            this.labelEndDate.TabIndex = 0;
            this.labelEndDate.Text = "Ending date:";
            this.labelEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(13, 64);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(64, 13);
            this.labelText.TabIndex = 4;
            this.labelText.Text = "Display text:";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(85, 11);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(133, 20);
            this.textBoxDate.TabIndex = 1;
            this.textBoxDate.TextChanged += new System.EventHandler(this.ContentChanged);
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(85, 36);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(133, 20);
            this.textBoxTime.TabIndex = 3;
            this.textBoxTime.TextChanged += new System.EventHandler(this.ContentChanged);
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Location = new System.Drawing.Point(85, 61);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Size = new System.Drawing.Size(419, 20);
            this.textBoxDisplay.TabIndex = 0;
            this.textBoxDisplay.TextChanged += new System.EventHandler(this.ContentChanged);
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(510, 59);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(50, 23);
            this.buttonFont.TabIndex = 6;
            this.buttonFont.Text = "Font...";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // labelPosition2
            // 
            this.labelPosition2.AutoSize = true;
            this.labelPosition2.Location = new System.Drawing.Point(30, 195);
            this.labelPosition2.Name = "labelPosition2";
            this.labelPosition2.Size = new System.Drawing.Size(47, 13);
            this.labelPosition2.TabIndex = 11;
            this.labelPosition2.Text = "Position:";
            this.labelPosition2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonTopLeft
            // 
            this.radioButtonTopLeft.AutoSize = true;
            this.radioButtonTopLeft.Location = new System.Drawing.Point(85, 168);
            this.radioButtonTopLeft.Name = "radioButtonTopLeft";
            this.radioButtonTopLeft.Size = new System.Drawing.Size(14, 13);
            this.radioButtonTopLeft.TabIndex = 12;
            this.radioButtonTopLeft.TabStop = true;
            this.radioButtonTopLeft.Tag = "0";
            this.radioButtonTopLeft.UseVisualStyleBackColor = true;
            this.radioButtonTopLeft.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonTopRight
            // 
            this.radioButtonTopRight.AutoSize = true;
            this.radioButtonTopRight.Location = new System.Drawing.Point(126, 168);
            this.radioButtonTopRight.Name = "radioButtonTopRight";
            this.radioButtonTopRight.Size = new System.Drawing.Size(14, 13);
            this.radioButtonTopRight.TabIndex = 14;
            this.radioButtonTopRight.TabStop = true;
            this.radioButtonTopRight.Tag = "2";
            this.radioButtonTopRight.UseVisualStyleBackColor = true;
            this.radioButtonTopRight.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonTopCenter
            // 
            this.radioButtonTopCenter.AutoSize = true;
            this.radioButtonTopCenter.Location = new System.Drawing.Point(106, 168);
            this.radioButtonTopCenter.Name = "radioButtonTopCenter";
            this.radioButtonTopCenter.Size = new System.Drawing.Size(14, 13);
            this.radioButtonTopCenter.TabIndex = 13;
            this.radioButtonTopCenter.TabStop = true;
            this.radioButtonTopCenter.Tag = "1";
            this.radioButtonTopCenter.UseVisualStyleBackColor = true;
            this.radioButtonTopCenter.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonMiddleCenter
            // 
            this.radioButtonMiddleCenter.AutoSize = true;
            this.radioButtonMiddleCenter.Location = new System.Drawing.Point(106, 187);
            this.radioButtonMiddleCenter.Name = "radioButtonMiddleCenter";
            this.radioButtonMiddleCenter.Size = new System.Drawing.Size(14, 13);
            this.radioButtonMiddleCenter.TabIndex = 16;
            this.radioButtonMiddleCenter.TabStop = true;
            this.radioButtonMiddleCenter.Tag = "4";
            this.radioButtonMiddleCenter.UseVisualStyleBackColor = true;
            this.radioButtonMiddleCenter.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonMiddleRight
            // 
            this.radioButtonMiddleRight.AutoSize = true;
            this.radioButtonMiddleRight.Location = new System.Drawing.Point(126, 187);
            this.radioButtonMiddleRight.Name = "radioButtonMiddleRight";
            this.radioButtonMiddleRight.Size = new System.Drawing.Size(14, 13);
            this.radioButtonMiddleRight.TabIndex = 17;
            this.radioButtonMiddleRight.TabStop = true;
            this.radioButtonMiddleRight.Tag = "5";
            this.radioButtonMiddleRight.UseVisualStyleBackColor = true;
            this.radioButtonMiddleRight.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonMiddleLeft
            // 
            this.radioButtonMiddleLeft.AutoSize = true;
            this.radioButtonMiddleLeft.Location = new System.Drawing.Point(85, 187);
            this.radioButtonMiddleLeft.Name = "radioButtonMiddleLeft";
            this.radioButtonMiddleLeft.Size = new System.Drawing.Size(14, 13);
            this.radioButtonMiddleLeft.TabIndex = 15;
            this.radioButtonMiddleLeft.TabStop = true;
            this.radioButtonMiddleLeft.Tag = "3";
            this.radioButtonMiddleLeft.UseVisualStyleBackColor = true;
            this.radioButtonMiddleLeft.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonBottomCenter
            // 
            this.radioButtonBottomCenter.AutoSize = true;
            this.radioButtonBottomCenter.Location = new System.Drawing.Point(106, 206);
            this.radioButtonBottomCenter.Name = "radioButtonBottomCenter";
            this.radioButtonBottomCenter.Size = new System.Drawing.Size(14, 13);
            this.radioButtonBottomCenter.TabIndex = 19;
            this.radioButtonBottomCenter.TabStop = true;
            this.radioButtonBottomCenter.Tag = "7";
            this.radioButtonBottomCenter.UseVisualStyleBackColor = true;
            this.radioButtonBottomCenter.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonBottomRight
            // 
            this.radioButtonBottomRight.AutoSize = true;
            this.radioButtonBottomRight.Location = new System.Drawing.Point(126, 206);
            this.radioButtonBottomRight.Name = "radioButtonBottomRight";
            this.radioButtonBottomRight.Size = new System.Drawing.Size(14, 13);
            this.radioButtonBottomRight.TabIndex = 20;
            this.radioButtonBottomRight.TabStop = true;
            this.radioButtonBottomRight.Tag = "8";
            this.radioButtonBottomRight.UseVisualStyleBackColor = true;
            this.radioButtonBottomRight.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // radioButtonBottomLeft
            // 
            this.radioButtonBottomLeft.AutoSize = true;
            this.radioButtonBottomLeft.Location = new System.Drawing.Point(85, 206);
            this.radioButtonBottomLeft.Name = "radioButtonBottomLeft";
            this.radioButtonBottomLeft.Size = new System.Drawing.Size(14, 13);
            this.radioButtonBottomLeft.TabIndex = 18;
            this.radioButtonBottomLeft.TabStop = true;
            this.radioButtonBottomLeft.Tag = "6";
            this.radioButtonBottomLeft.UseVisualStyleBackColor = true;
            this.radioButtonBottomLeft.CheckedChanged += new System.EventHandler(this.radioButtonScreen_CheckedChanged);
            // 
            // labelPosition1
            // 
            this.labelPosition1.AutoSize = true;
            this.labelPosition1.Location = new System.Drawing.Point(32, 176);
            this.labelPosition1.Name = "labelPosition1";
            this.labelPosition1.Size = new System.Drawing.Size(41, 13);
            this.labelPosition1.TabIndex = 10;
            this.labelPosition1.Text = "Screen";
            this.labelPosition1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(85, 253);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(522, 179);
            this.pictureBox.TabIndex = 24;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            // 
            // labelBox
            // 
            this.labelBox.AutoSize = true;
            this.labelBox.Location = new System.Drawing.Point(32, 255);
            this.labelBox.Name = "labelBox";
            this.labelBox.Size = new System.Drawing.Size(41, 13);
            this.labelBox.TabIndex = 27;
            this.labelBox.Text = "Screen";
            this.labelBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxDays
            // 
            this.checkBoxDays.AutoSize = true;
            this.checkBoxDays.Location = new System.Drawing.Point(202, 186);
            this.checkBoxDays.Name = "checkBoxDays";
            this.checkBoxDays.Size = new System.Drawing.Size(50, 17);
            this.checkBoxDays.TabIndex = 21;
            this.checkBoxDays.Text = "Days";
            this.checkBoxDays.UseVisualStyleBackColor = true;
            this.checkBoxDays.CheckedChanged += new System.EventHandler(this.checkBoxDisplayFormat_Changed);
            // 
            // checkBoxHours
            // 
            this.checkBoxHours.AutoSize = true;
            this.checkBoxHours.Location = new System.Drawing.Point(277, 186);
            this.checkBoxHours.Name = "checkBoxHours";
            this.checkBoxHours.Size = new System.Drawing.Size(54, 17);
            this.checkBoxHours.TabIndex = 22;
            this.checkBoxHours.Text = "Hours";
            this.checkBoxHours.UseVisualStyleBackColor = true;
            this.checkBoxHours.CheckedChanged += new System.EventHandler(this.checkBoxDisplayFormat_Changed);
            // 
            // checkBoxMinutes
            // 
            this.checkBoxMinutes.AutoSize = true;
            this.checkBoxMinutes.Location = new System.Drawing.Point(352, 187);
            this.checkBoxMinutes.Name = "checkBoxMinutes";
            this.checkBoxMinutes.Size = new System.Drawing.Size(63, 17);
            this.checkBoxMinutes.TabIndex = 23;
            this.checkBoxMinutes.Text = "Minutes";
            this.checkBoxMinutes.UseVisualStyleBackColor = true;
            this.checkBoxMinutes.CheckedChanged += new System.EventHandler(this.checkBoxDisplayFormat_Changed);
            // 
            // checkBoxSeconds
            // 
            this.checkBoxSeconds.AutoSize = true;
            this.checkBoxSeconds.Location = new System.Drawing.Point(427, 186);
            this.checkBoxSeconds.Name = "checkBoxSeconds";
            this.checkBoxSeconds.Size = new System.Drawing.Size(68, 17);
            this.checkBoxSeconds.TabIndex = 24;
            this.checkBoxSeconds.Text = "Seconds";
            this.checkBoxSeconds.UseVisualStyleBackColor = true;
            this.checkBoxSeconds.CheckedChanged += new System.EventHandler(this.checkBoxDisplayFormat_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Finished text:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxFinished
            // 
            this.textBoxFinished.Location = new System.Drawing.Point(85, 140);
            this.textBoxFinished.Name = "textBoxFinished";
            this.textBoxFinished.Size = new System.Drawing.Size(419, 20);
            this.textBoxFinished.TabIndex = 9;
            this.textBoxFinished.TextChanged += new System.EventHandler(this.ContentChanged);
            // 
            // checkBoxOverridden
            // 
            this.checkBoxOverridden.AutoSize = true;
            this.checkBoxOverridden.Location = new System.Drawing.Point(85, 230);
            this.checkBoxOverridden.Name = "checkBoxOverridden";
            this.checkBoxOverridden.Size = new System.Drawing.Size(72, 17);
            this.checkBoxOverridden.TabIndex = 25;
            this.checkBoxOverridden.Text = "Overriden";
            this.checkBoxOverridden.UseVisualStyleBackColor = true;
            this.checkBoxOverridden.CheckedChanged += new System.EventHandler(this.checkBoxOverridden_CheckedChanged);
            // 
            // textBoxOverride
            // 
            this.textBoxOverride.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOverride.Enabled = false;
            this.textBoxOverride.Location = new System.Drawing.Point(160, 229);
            this.textBoxOverride.Name = "textBoxOverride";
            this.textBoxOverride.ReadOnly = true;
            this.textBoxOverride.Size = new System.Drawing.Size(141, 20);
            this.textBoxOverride.TabIndex = 26;
            // 
            // pictureBoxText
            // 
            this.pictureBoxText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxText.Location = new System.Drawing.Point(85, 88);
            this.pictureBoxText.Name = "pictureBoxText";
            this.pictureBoxText.Size = new System.Drawing.Size(419, 43);
            this.pictureBoxText.TabIndex = 31;
            this.pictureBoxText.TabStop = false;
            this.pictureBoxText.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxText_Paint);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(619, 478);
            this.Controls.Add(this.pictureBoxText);
            this.Controls.Add(this.textBoxOverride);
            this.Controls.Add(this.checkBoxOverridden);
            this.Controls.Add(this.textBoxFinished);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxSeconds);
            this.Controls.Add(this.checkBoxMinutes);
            this.Controls.Add(this.checkBoxHours);
            this.Controls.Add(this.checkBoxDays);
            this.Controls.Add(this.labelBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelPosition1);
            this.Controls.Add(this.radioButtonBottomCenter);
            this.Controls.Add(this.radioButtonBottomRight);
            this.Controls.Add(this.radioButtonBottomLeft);
            this.Controls.Add(this.radioButtonMiddleCenter);
            this.Controls.Add(this.radioButtonMiddleRight);
            this.Controls.Add(this.radioButtonMiddleLeft);
            this.Controls.Add(this.radioButtonTopCenter);
            this.Controls.Add(this.radioButtonTopRight);
            this.Controls.Add(this.radioButtonTopLeft);
            this.Controls.Add(this.labelPosition2);
            this.Controls.Add(this.buttonFont);
            this.Controls.Add(this.textBoxDisplay);
            this.Controls.Add(this.textBoxTime);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelEndDate);
            this.Controls.Add(this.labelEndTime);
            this.Controls.Add(this.buttonOkay);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Timer Configuration";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOkay;
        private System.Windows.Forms.Label labelEndTime;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.TextBox textBoxDisplay;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.Label labelPosition2;
        private System.Windows.Forms.RadioButton radioButtonTopLeft;
        private System.Windows.Forms.RadioButton radioButtonTopRight;
        private System.Windows.Forms.RadioButton radioButtonTopCenter;
        private System.Windows.Forms.RadioButton radioButtonMiddleCenter;
        private System.Windows.Forms.RadioButton radioButtonMiddleRight;
        private System.Windows.Forms.RadioButton radioButtonMiddleLeft;
        private System.Windows.Forms.RadioButton radioButtonBottomCenter;
        private System.Windows.Forms.RadioButton radioButtonBottomRight;
        private System.Windows.Forms.RadioButton radioButtonBottomLeft;
        private System.Windows.Forms.Label labelPosition1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelBox;
        private System.Windows.Forms.CheckBox checkBoxDays;
        private System.Windows.Forms.CheckBox checkBoxHours;
        private System.Windows.Forms.CheckBox checkBoxMinutes;
        private System.Windows.Forms.CheckBox checkBoxSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFinished;
        private System.Windows.Forms.CheckBox checkBoxOverridden;
        private System.Windows.Forms.TextBox textBoxOverride;
        private System.Windows.Forms.PictureBox pictureBoxText;
    }
}

