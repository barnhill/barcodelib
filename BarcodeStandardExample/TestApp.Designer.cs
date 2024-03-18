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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblAverageGenerationTime = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIncludeImageInSavedData = new System.Windows.Forms.CheckBox();
            this.btnSaveJSON = new System.Windows.Forms.Button();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.btnLoadJSON = new System.Windows.Forms.Button();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
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
            this.chkTopBar = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbGuardBarsMode = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkGenerateLabel = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.label15 = new System.Windows.Forms.Label();
            this.cbBearerBarsMode = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslEncodedType,
            this.tslblLibraryVersion,
            this.tslblCredits});
            this.statusStrip1.Location = new System.Drawing.Point(0, 671);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1015, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 30;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslEncodedType
            // 
            this.tsslEncodedType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslEncodedType.Name = "tsslEncodedType";
            this.tsslEncodedType.Size = new System.Drawing.Size(4, 19);
            // 
            // tslblLibraryVersion
            // 
            this.tslblLibraryVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslblLibraryVersion.Name = "tslblLibraryVersion";
            this.tslblLibraryVersion.Size = new System.Drawing.Size(860, 19);
            this.tslblLibraryVersion.Spring = true;
            this.tslblLibraryVersion.Text = "LibVersion";
            // 
            // tslblCredits
            // 
            this.tslblCredits.Name = "tslblCredits";
            this.tslblCredits.Size = new System.Drawing.Size(135, 19);
            this.tslblCredits.Text = "Written by: Brad Barnhill";
            this.tslblCredits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // barcode
            // 
            this.barcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barcode.Location = new System.Drawing.Point(0, 0);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(711, 671);
            this.barcode.TabIndex = 36;
            this.barcode.TabStop = false;
            this.barcode.Text = "Barcode Image";
            // 
            // btnMassGeneration
            // 
            this.btnMassGeneration.Location = new System.Drawing.Point(4, 16);
            this.btnMassGeneration.Margin = new System.Windows.Forms.Padding(2);
            this.btnMassGeneration.Name = "btnMassGeneration";
            this.btnMassGeneration.Size = new System.Drawing.Size(121, 33);
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
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
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
            this.splitContainer1.Size = new System.Drawing.Size(1015, 671);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 37;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.btnMassGeneration);
            this.groupBox2.Controls.Add(this.lblAverageGenerationTime);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(8, 586);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(290, 80);
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bulk Operations";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(5, 54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(279, 5);
            this.progressBar1.TabIndex = 79;
            // 
            // lblAverageGenerationTime
            // 
            this.lblAverageGenerationTime.AutoSize = true;
            this.lblAverageGenerationTime.Location = new System.Drawing.Point(136, 63);
            this.lblAverageGenerationTime.Name = "lblAverageGenerationTime";
            this.lblAverageGenerationTime.Size = new System.Drawing.Size(0, 13);
            this.lblAverageGenerationTime.TabIndex = 81;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 13);
            this.label14.TabIndex = 80;
            this.label14.Text = "Average Generation Time: ";
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
            this.groupBox1.Location = new System.Drawing.Point(8, 492);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(282, 90);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            // 
            // chkIncludeImageInSavedData
            // 
            this.chkIncludeImageInSavedData.AutoSize = true;
            this.chkIncludeImageInSavedData.Location = new System.Drawing.Point(186, 70);
            this.chkIncludeImageInSavedData.Margin = new System.Windows.Forms.Padding(2);
            this.chkIncludeImageInSavedData.Name = "chkIncludeImageInSavedData";
            this.chkIncludeImageInSavedData.Size = new System.Drawing.Size(93, 17);
            this.chkIncludeImageInSavedData.TabIndex = 0;
            this.chkIncludeImageInSavedData.Text = "Include Image";
            this.chkIncludeImageInSavedData.UseVisualStyleBackColor = true;
            // 
            // btnSaveJSON
            // 
            this.btnSaveJSON.Location = new System.Drawing.Point(201, 11);
            this.btnSaveJSON.Name = "btnSaveJSON";
            this.btnSaveJSON.Size = new System.Drawing.Size(75, 23);
            this.btnSaveJSON.TabIndex = 82;
            this.btnSaveJSON.Text = "Save &JSON";
            this.btnSaveJSON.UseVisualStyleBackColor = true;
            this.btnSaveJSON.Click += new System.EventHandler(this.btnSaveJSON_Click);
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(116, 40);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(79, 23);
            this.btnLoadXML.TabIndex = 72;
            this.btnLoadXML.Text = "Load XML";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // btnLoadJSON
            // 
            this.btnLoadJSON.Location = new System.Drawing.Point(201, 40);
            this.btnLoadJSON.Name = "btnLoadJSON";
            this.btnLoadJSON.Size = new System.Drawing.Size(75, 23);
            this.btnLoadJSON.TabIndex = 83;
            this.btnLoadJSON.Text = "Load JSON";
            this.btnLoadJSON.UseVisualStyleBackColor = true;
            this.btnLoadJSON.Click += new System.EventHandler(this.btnLoadJSON_Click);
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(116, 11);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(79, 23);
            this.btnSaveXML.TabIndex = 71;
            this.btnSaveXML.Text = "Save &XML";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(4, 53);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 29);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "&Save As";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(4, 11);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(83, 36);
            this.btnEncode.TabIndex = 60;
            this.btnEncode.Text = "&Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
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
            this.groupBox4.Location = new System.Drawing.Point(164, 48);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(128, 93);
            this.groupBox4.TabIndex = 78;
            this.groupBox4.TabStop = false;
            // 
            // textBoxAspectRatio
            // 
            this.textBoxAspectRatio.Location = new System.Drawing.Point(74, 67);
            this.textBoxAspectRatio.Name = "textBoxAspectRatio";
            this.textBoxAspectRatio.Size = new System.Drawing.Size(35, 20);
            this.textBoxAspectRatio.TabIndex = 55;
            // 
            // textBoxBarWidth
            // 
            this.textBoxBarWidth.Location = new System.Drawing.Point(16, 68);
            this.textBoxBarWidth.Name = "textBoxBarWidth";
            this.textBoxBarWidth.Size = new System.Drawing.Size(35, 20);
            this.textBoxBarWidth.TabIndex = 54;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(58, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 13);
            this.label13.TabIndex = 53;
            this.label13.Text = "AspectRatio";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 52;
            this.label12.Text = "BarWidth";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(19, 24);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(35, 20);
            this.txtWidth.TabIndex = 43;
            this.txtWidth.Text = "600";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(64, 24);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(35, 20);
            this.txtHeight.TabIndex = 44;
            this.txtHeight.Text = "300";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "x";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cbBearerBarsMode);
            this.groupBox3.Controls.Add(this.chkTopBar);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cbGuardBarsMode);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.chkGenerateLabel);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(164, 145);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(128, 193);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            // 
            // chkTopBar
            // 
            this.chkTopBar.AutoSize = true;
            this.chkTopBar.Location = new System.Drawing.Point(6, 124);
            this.chkTopBar.Name = "chkTopBar";
            this.chkTopBar.Size = new System.Drawing.Size(63, 17);
            this.chkTopBar.TabIndex = 58;
            this.chkTopBar.Text = "Top bar";
            this.chkTopBar.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Guard bars mode";
            // 
            // cbGuardBarsMode
            // 
            this.cbGuardBarsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGuardBarsMode.FormattingEnabled = true;
            this.cbGuardBarsMode.Items.AddRange(new object[] {
            "Enabled",
            "EnabledFirstCharOnQuietZone",
            "Disabled"});
            this.cbGuardBarsMode.Location = new System.Drawing.Point(4, 97);
            this.cbGuardBarsMode.Name = "cbGuardBarsMode";
            this.cbGuardBarsMode.Size = new System.Drawing.Size(118, 21);
            this.cbGuardBarsMode.TabIndex = 56;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 20);
            this.textBox1.TabIndex = 54;
            // 
            // chkGenerateLabel
            // 
            this.chkGenerateLabel.AutoSize = true;
            this.chkGenerateLabel.Location = new System.Drawing.Point(6, 15);
            this.chkGenerateLabel.Name = "chkGenerateLabel";
            this.chkGenerateLabel.Size = new System.Drawing.Size(95, 17);
            this.chkGenerateLabel.TabIndex = 40;
            this.chkGenerateLabel.Text = "Generate label";
            this.chkGenerateLabel.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 55;
            this.label11.Text = "Alternate Label Text";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Alignment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 68;
            this.label4.Text = "Foreground";
            // 
            // txtEncoded
            // 
            this.txtEncoded.Location = new System.Drawing.Point(7, 344);
            this.txtEncoded.Multiline = true;
            this.txtEncoded.Name = "txtEncoded";
            this.txtEncoded.ReadOnly = true;
            this.txtEncoded.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncoded.Size = new System.Drawing.Size(292, 149);
            this.txtEncoded.TabIndex = 62;
            this.txtEncoded.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Background";
            // 
            // btnBackColor
            // 
            this.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackColor.Location = new System.Drawing.Point(74, 157);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(58, 23);
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
            this.cbBarcodeAlign.Location = new System.Drawing.Point(8, 109);
            this.cbBarcodeAlign.Name = "cbBarcodeAlign";
            this.cbBarcodeAlign.Size = new System.Drawing.Size(127, 21);
            this.cbBarcodeAlign.TabIndex = 73;
            // 
            // btnForeColor
            // 
            this.btnForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForeColor.Location = new System.Drawing.Point(8, 157);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(58, 23);
            this.btnForeColor.TabIndex = 66;
            this.btnForeColor.UseVisualStyleBackColor = true;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Encoded Value";
            // 
            // lblEncodingTime
            // 
            this.lblEncodingTime.AutoSize = true;
            this.lblEncodingTime.Location = new System.Drawing.Point(89, 207);
            this.lblEncodingTime.Name = "lblEncodingTime";
            this.lblEncodingTime.Size = new System.Drawing.Size(0, 13);
            this.lblEncodingTime.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 65;
            this.label3.Text = "Encoding";
            // 
            // cbEncodeType
            // 
            this.cbEncodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEncodeType.FormattingEnabled = true;
            this.cbEncodeType.ItemHeight = 13;
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
            "MSI Mod 10",
            "MSI Mod 11",
            "MSI 2 Mod 10",
            "MSI Mod 11 Mod 10",
            "Telepen",
            "FIM",
            "Pharmacode"});
            this.cbEncodeType.Location = new System.Drawing.Point(8, 63);
            this.cbEncodeType.Name = "cbEncodeType";
            this.cbEncodeType.Size = new System.Drawing.Size(127, 21);
            this.cbEncodeType.TabIndex = 59;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(7, 24);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(287, 20);
            this.txtData.TabIndex = 58;
            this.txtData.Text = "038000356216";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Value to Encode";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 13);
            this.label15.TabIndex = 60;
            this.label15.Text = "Bearer bars mode";
            // 
            // cbBearerBarsMode
            // 
            this.cbBearerBarsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBearerBarsMode.FormattingEnabled = true;
            this.cbBearerBarsMode.Items.AddRange(new object[] {
            "Frame",
            "BearerBars",
            "Disabled"});
            this.cbBearerBarsMode.Location = new System.Drawing.Point(5, 159);
            this.cbBearerBarsMode.Name = "cbBearerBarsMode";
            this.cbBearerBarsMode.Size = new System.Drawing.Size(118, 21);
            this.cbBearerBarsMode.TabIndex = 59;
            // 
            // TestApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 695);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(688, 516);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbGuardBarsMode;
        private System.Windows.Forms.CheckBox chkTopBar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbBearerBarsMode;
    }
}

