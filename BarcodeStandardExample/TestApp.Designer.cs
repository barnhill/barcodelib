namespace BarcodeStandardExample
{
    partial class TestApp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestApp));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslEncodedType = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblLibraryVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblCredits = new System.Windows.Forms.ToolStripStatusLabel();
            this.barcode = new System.Windows.Forms.GroupBox();
            this.btnMassGeneration = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveJSON = new System.Windows.Forms.Button();
            this.btnLoadJSON = new System.Windows.Forms.Button();
            this.lblAverageGenerationTime = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAspectRatio = new System.Windows.Forms.TextBox();
            this.textBoxBarWidth = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkGenerateLabel = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbLabelLocation = new System.Windows.Forms.ComboBox();
            this.lblLabelLocation = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbRotateFlip = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEncode = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEncoded = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.cbBarcodeAlign = new System.Windows.Forms.ComboBox();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEncodingTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEncodeType = new System.Windows.Forms.ComboBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkIncludeImageInSavedData = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslEncodedType,
            this.tslblLibraryVersion,
            this.tslblCredits});
            this.statusStrip1.Location = new System.Drawing.Point(0, 908);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1522, 36);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 30;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslEncodedType
            // 
            this.tsslEncodedType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslEncodedType.Name = "tsslEncodedType";
            this.tsslEncodedType.Size = new System.Drawing.Size(4, 29);
            // 
            // tslblLibraryVersion
            // 
            this.tslblLibraryVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslblLibraryVersion.Name = "tslblLibraryVersion";
            this.tslblLibraryVersion.Size = new System.Drawing.Size(1293, 29);
            this.tslblLibraryVersion.Spring = true;
            this.tslblLibraryVersion.Text = "LibVersion";
            // 
            // tslblCredits
            // 
            this.tslblCredits.Name = "tslblCredits";
            this.tslblCredits.Size = new System.Drawing.Size(202, 29);
            this.tslblCredits.Text = "Written by: Brad Barnhill";
            this.tslblCredits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // barcode
            // 
            this.barcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barcode.Location = new System.Drawing.Point(0, 0);
            this.barcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barcode.Name = "barcode";
            this.barcode.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barcode.Size = new System.Drawing.Size(1066, 908);
            this.barcode.TabIndex = 36;
            this.barcode.TabStop = false;
            this.barcode.Text = "Barcode Image";
            // 
            // btnMassGeneration
            // 
            this.btnMassGeneration.Location = new System.Drawing.Point(6, 25);
            this.btnMassGeneration.Name = "btnMassGeneration";
            this.btnMassGeneration.Size = new System.Drawing.Size(181, 51);
            this.btnMassGeneration.TabIndex = 0;
            this.btnMassGeneration.Text = "Generate 1000";
            this.btnMassGeneration.UseVisualStyleBackColor = true;
            this.btnMassGeneration.Click += new System.EventHandler(this.btnMassGeneration_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.cbRotateFlip);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtEncoded);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnBackColor);
            this.splitContainer1.Panel1.Controls.Add(this.cbBarcodeAlign);
            this.splitContainer1.Panel1.Controls.Add(this.btnForeColor);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lblEncodingTime);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cbEncodeType);
            this.splitContainer1.Panel1.Controls.Add(this.txtData);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.barcode);
            this.splitContainer1.Size = new System.Drawing.Size(1522, 908);
            this.splitContainer1.SplitterDistance = 450;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 37;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // btnSaveJSON
            // 
            this.btnSaveJSON.Location = new System.Drawing.Point(302, 16);
            this.btnSaveJSON.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveJSON.Name = "btnSaveJSON";
            this.btnSaveJSON.Size = new System.Drawing.Size(113, 35);
            this.btnSaveJSON.TabIndex = 82;
            this.btnSaveJSON.Text = "Save &JSON";
            this.btnSaveJSON.UseVisualStyleBackColor = true;
            this.btnSaveJSON.Click += new System.EventHandler(this.btnSaveJSON_Click);
            // 
            // btnLoadJSON
            // 
            this.btnLoadJSON.Location = new System.Drawing.Point(302, 61);
            this.btnLoadJSON.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadJSON.Name = "btnLoadJSON";
            this.btnLoadJSON.Size = new System.Drawing.Size(113, 35);
            this.btnLoadJSON.TabIndex = 83;
            this.btnLoadJSON.Text = "Load JSON";
            this.btnLoadJSON.UseVisualStyleBackColor = true;
            this.btnLoadJSON.Click += new System.EventHandler(this.btnLoadJSON_Click);
            // 
            // lblAverageGenerationTime
            // 
            this.lblAverageGenerationTime.AutoSize = true;
            this.lblAverageGenerationTime.Location = new System.Drawing.Point(196, 101);
            this.lblAverageGenerationTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAverageGenerationTime.Name = "lblAverageGenerationTime";
            this.lblAverageGenerationTime.Size = new System.Drawing.Size(0, 20);
            this.lblAverageGenerationTime.TabIndex = 81;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(-2, 101);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(198, 20);
            this.label14.TabIndex = 80;
            this.label14.Text = "Average Generation Time: ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 84);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(419, 8);
            this.progressBar1.TabIndex = 79;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxAspectRatio);
            this.groupBox4.Controls.Add(this.textBoxBarWidth);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtWidth);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtHeight);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(204, 68);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(192, 143);
            this.groupBox4.TabIndex = 78;
            this.groupBox4.TabStop = false;
            // 
            // textBoxAspectRatio
            // 
            this.textBoxAspectRatio.Location = new System.Drawing.Point(110, 103);
            this.textBoxAspectRatio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxAspectRatio.Name = "textBoxAspectRatio";
            this.textBoxAspectRatio.Size = new System.Drawing.Size(50, 26);
            this.textBoxAspectRatio.TabIndex = 55;
            // 
            // textBoxBarWidth
            // 
            this.textBoxBarWidth.Location = new System.Drawing.Point(24, 105);
            this.textBoxBarWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxBarWidth.Name = "textBoxBarWidth";
            this.textBoxBarWidth.Size = new System.Drawing.Size(50, 26);
            this.textBoxBarWidth.TabIndex = 54;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(88, 78);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 20);
            this.label13.TabIndex = 53;
            this.label13.Text = "AspectRatio";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 78);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 20);
            this.label12.TabIndex = 52;
            this.label12.Text = "BarWidth";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(28, 38);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(50, 26);
            this.txtWidth.TabIndex = 43;
            this.txtWidth.Text = "600";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(92, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 42;
            this.label6.Text = "Height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(96, 38);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(50, 26);
            this.txtHeight.TabIndex = 44;
            this.txtHeight.Text = "300";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(80, 42);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 51;
            this.label9.Text = "x";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.chkGenerateLabel);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cbLabelLocation);
            this.groupBox3.Controls.Add(this.lblLabelLocation);
            this.groupBox3.Location = new System.Drawing.Point(204, 217);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(192, 185);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 80);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 26);
            this.textBox1.TabIndex = 54;
            // 
            // chkGenerateLabel
            // 
            this.chkGenerateLabel.AutoSize = true;
            this.chkGenerateLabel.Location = new System.Drawing.Point(9, 22);
            this.chkGenerateLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkGenerateLabel.Name = "chkGenerateLabel";
            this.chkGenerateLabel.Size = new System.Drawing.Size(140, 24);
            this.chkGenerateLabel.TabIndex = 40;
            this.chkGenerateLabel.Text = "Generate label";
            this.chkGenerateLabel.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 60);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 20);
            this.label11.TabIndex = 55;
            this.label11.Text = "Alternate Label Text";
            // 
            // cbLabelLocation
            // 
            this.cbLabelLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLabelLocation.FormattingEnabled = true;
            this.cbLabelLocation.Items.AddRange(new object[] {
            "BottomCenter",
            "BottomLeft",
            "BottomRight",
            "TopCenter",
            "TopLeft",
            "TopRight"});
            this.cbLabelLocation.Location = new System.Drawing.Point(9, 145);
            this.cbLabelLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbLabelLocation.Name = "cbLabelLocation";
            this.cbLabelLocation.Size = new System.Drawing.Size(146, 28);
            this.cbLabelLocation.TabIndex = 0;
            // 
            // lblLabelLocation
            // 
            this.lblLabelLocation.AutoSize = true;
            this.lblLabelLocation.Location = new System.Drawing.Point(4, 120);
            this.lblLabelLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabelLocation.Name = "lblLabelLocation";
            this.lblLabelLocation.Size = new System.Drawing.Size(113, 20);
            this.lblLabelLocation.TabIndex = 48;
            this.lblLabelLocation.Text = "Label Location";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 138);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 20);
            this.label10.TabIndex = 76;
            this.label10.Text = "Rotate";
            // 
            // cbRotateFlip
            // 
            this.cbRotateFlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRotateFlip.FormattingEnabled = true;
            this.cbRotateFlip.Items.AddRange(new object[] {
            "Center",
            "Left",
            "Right"});
            this.cbRotateFlip.Location = new System.Drawing.Point(10, 163);
            this.cbRotateFlip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbRotateFlip.Name = "cbRotateFlip";
            this.cbRotateFlip.Size = new System.Drawing.Size(188, 28);
            this.cbRotateFlip.TabIndex = 75;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 81);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 45);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "&Save As";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(173, 16);
            this.btnSaveXML.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(118, 35);
            this.btnSaveXML.TabIndex = 71;
            this.btnSaveXML.Text = "Save &XML";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(173, 61);
            this.btnLoadXML.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(118, 35);
            this.btnLoadXML.TabIndex = 72;
            this.btnLoadXML.Text = "Load XML";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 203);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 74;
            this.label8.Text = "Alignment";
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(7, 16);
            this.btnEncode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(125, 55);
            this.btnEncode.TabIndex = 60;
            this.btnEncode.Text = "&Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 271);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 68;
            this.label4.Text = "Foreground";
            // 
            // txtEncoded
            // 
            this.txtEncoded.Location = new System.Drawing.Point(10, 403);
            this.txtEncoded.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEncoded.Multiline = true;
            this.txtEncoded.Name = "txtEncoded";
            this.txtEncoded.ReadOnly = true;
            this.txtEncoded.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncoded.Size = new System.Drawing.Size(436, 227);
            this.txtEncoded.TabIndex = 62;
            this.txtEncoded.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 271);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 69;
            this.label5.Text = "Background";
            // 
            // btnBackColor
            // 
            this.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackColor.Location = new System.Drawing.Point(108, 295);
            this.btnBackColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(87, 35);
            this.btnBackColor.TabIndex = 67;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // cbBarcodeAlign
            // 
            this.cbBarcodeAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBarcodeAlign.FormattingEnabled = true;
            this.cbBarcodeAlign.Items.AddRange(new object[] {
            "Center",
            "Left",
            "Right"});
            this.cbBarcodeAlign.Location = new System.Drawing.Point(10, 228);
            this.cbBarcodeAlign.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbBarcodeAlign.Name = "cbBarcodeAlign";
            this.cbBarcodeAlign.Size = new System.Drawing.Size(188, 28);
            this.cbBarcodeAlign.TabIndex = 73;
            // 
            // btnForeColor
            // 
            this.btnForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForeColor.Location = new System.Drawing.Point(10, 295);
            this.btnForeColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(87, 35);
            this.btnForeColor.TabIndex = 66;
            this.btnForeColor.UseVisualStyleBackColor = true;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 378);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 64;
            this.label2.Text = "Encoded Value";
            // 
            // lblEncodingTime
            // 
            this.lblEncodingTime.AutoSize = true;
            this.lblEncodingTime.Location = new System.Drawing.Point(134, 375);
            this.lblEncodingTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEncodingTime.Name = "lblEncodingTime";
            this.lblEncodingTime.Size = new System.Drawing.Size(0, 20);
            this.lblEncodingTime.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 65;
            this.label3.Text = "Encoding";
            // 
            // cbEncodeType
            // 
            this.cbEncodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEncodeType.FormattingEnabled = true;
            this.cbEncodeType.ItemHeight = 20;
            this.cbEncodeType.Items.AddRange(new object[] {
            "UPC-A",
            "UPC-E",
            "UPC 2 Digit Ext.",
            "UPC 5 Digit Ext.",
            "EAN-13",
            "JAN-13",
            "EAN-8",
            "ITF-14",
            "Interleaved 2 of 5",
            "Interleaved 2 of 5 Mod 10",
            "Standard 2 of 5",
            "Standard 2 of 5 Mod 10",
            "Codabar",
            "PostNet",
            "Bookland/ISBN",
            "Code 11",
            "Code 39",
            "Code 39 Extended",
            "Code 39 Mod 43",
            "Code 93",
            "Code 128",
            "Code 128-A",
            "Code 128-B",
            "Code 128-C",
            "LOGMARS",
            "MSI",
            "Telepen",
            "FIM",
            "Pharmacode"});
            this.cbEncodeType.Location = new System.Drawing.Point(10, 98);
            this.cbEncodeType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbEncodeType.Name = "cbEncodeType";
            this.cbEncodeType.Size = new System.Drawing.Size(188, 28);
            this.cbEncodeType.TabIndex = 59;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(10, 37);
            this.txtData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(374, 26);
            this.txtData.TabIndex = 58;
            this.txtData.Text = "038000356216";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 63;
            this.label1.Text = "Value to Encode";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkIncludeImageInSavedData
            // 
            this.chkIncludeImageInSavedData.AutoSize = true;
            this.chkIncludeImageInSavedData.Location = new System.Drawing.Point(279, 108);
            this.chkIncludeImageInSavedData.Name = "chkIncludeImageInSavedData";
            this.chkIncludeImageInSavedData.Size = new System.Drawing.Size(136, 24);
            this.chkIncludeImageInSavedData.TabIndex = 0;
            this.chkIncludeImageInSavedData.Text = "Include Image";
            this.chkIncludeImageInSavedData.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIncludeImageInSavedData);
            this.groupBox1.Controls.Add(this.btnSaveJSON);
            this.groupBox1.Controls.Add(this.btnLoadXML);
            this.groupBox1.Controls.Add(this.btnLoadJSON);
            this.groupBox1.Controls.Add(this.btnSaveXML);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnEncode);
            this.groupBox1.Location = new System.Drawing.Point(12, 638);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 139);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.btnMassGeneration);
            this.groupBox2.Controls.Add(this.lblAverageGenerationTime);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(12, 783);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 122);
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bulk Operations";
            // 
            // TestApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1522, 944);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "TestApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Barcode Encoder";
            this.Load += new System.EventHandler(this.TestApp_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslEncodedType;
        private System.Windows.Forms.GroupBox barcode;
        private System.Windows.Forms.ToolStripStatusLabel tslblCredits;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripStatusLabel tslblLibraryVersion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkGenerateLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbLabelLocation;
        private System.Windows.Forms.Label lblLabelLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbRotateFlip;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEncoded;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.ComboBox cbBarcodeAlign;
        private System.Windows.Forms.Button btnForeColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEncodingTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEncodeType;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAspectRatio;
        private System.Windows.Forms.TextBox textBoxBarWidth;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnMassGeneration;
        private System.Windows.Forms.Label lblAverageGenerationTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnSaveJSON;
        private System.Windows.Forms.Button btnLoadJSON;
        private System.Windows.Forms.CheckBox chkIncludeImageInSavedData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

