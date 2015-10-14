namespace AltoScan
{
    partial class Form1
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
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.cboHandShaking = new System.Windows.Forms.ComboBox();
            this.hexaOutput = new System.Windows.Forms.RichTextBox();
            this.decimalOutput = new System.Windows.Forms.RichTextBox();
            this.startQuery = new System.Windows.Forms.Button();
            this.puerto = new System.Windows.Forms.Label();
            this.velocidad = new System.Windows.Forms.Label();
            this.dataBits = new System.Windows.Forms.Label();
            this.stopBits = new System.Windows.Forms.Label();
            this.paridad = new System.Windows.Forms.Label();
            this.saludo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelFirstParam = new System.Windows.Forms.Label();
            this.labelSecond = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.inputFunction = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.binOutput = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.stopQuery = new System.Windows.Forms.Button();
            this.toSendTextBox = new System.Windows.Forms.TextBox();
            this.inputDispositiveId = new System.Windows.Forms.NumericUpDown();
            this.inputFirstParam = new System.Windows.Forms.NumericUpDown();
            this.inputSecondParam = new System.Windows.Forms.NumericUpDown();
            this.numberOfRetriesInput = new System.Windows.Forms.NumericUpDown();
            this.timeoutInput = new System.Windows.Forms.NumericUpDown();
            this.cleanOutputButton = new System.Windows.Forms.Button();
            this.labelThirdParam = new System.Windows.Forms.Label();
            this.inputThirdParam = new System.Windows.Forms.RichTextBox();
            this.errorTextBox = new System.Windows.Forms.TextBox();
            this.tableOutput = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.connectionType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tcpListeningPort = new System.Windows.Forms.NumericUpDown();
            this.tcpConnectionAddress = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.inputDispositiveId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputFirstParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSecondParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfRetriesInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcpListeningPort)).BeginInit();
            this.SuspendLayout();
            // 
            // cboPorts
            // 
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(103, 72);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(121, 21);
            this.cboPorts.TabIndex = 1;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Location = new System.Drawing.Point(103, 114);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cboBaudRate.TabIndex = 2;
            // 
            // cboDataBits
            // 
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(103, 155);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(121, 21);
            this.cboDataBits.TabIndex = 3;
            // 
            // cboStopBits
            // 
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(103, 196);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(121, 21);
            this.cboStopBits.TabIndex = 4;
            // 
            // cboParity
            // 
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(103, 233);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(121, 21);
            this.cboParity.TabIndex = 5;
            // 
            // cboHandShaking
            // 
            this.cboHandShaking.Enabled = false;
            this.cboHandShaking.FormattingEnabled = true;
            this.cboHandShaking.Location = new System.Drawing.Point(103, 270);
            this.cboHandShaking.Name = "cboHandShaking";
            this.cboHandShaking.Size = new System.Drawing.Size(121, 21);
            this.cboHandShaking.TabIndex = 6;
            // 
            // hexaOutput
            // 
            this.hexaOutput.Location = new System.Drawing.Point(601, 154);
            this.hexaOutput.Name = "hexaOutput";
            this.hexaOutput.Size = new System.Drawing.Size(336, 118);
            this.hexaOutput.TabIndex = 7;
            this.hexaOutput.Text = "";
            // 
            // decimalOutput
            // 
            this.decimalOutput.Location = new System.Drawing.Point(601, 28);
            this.decimalOutput.Name = "decimalOutput";
            this.decimalOutput.Size = new System.Drawing.Size(336, 120);
            this.decimalOutput.TabIndex = 8;
            this.decimalOutput.Text = "";
            // 
            // startQuery
            // 
            this.startQuery.Location = new System.Drawing.Point(105, 494);
            this.startQuery.Name = "startQuery";
            this.startQuery.Size = new System.Drawing.Size(139, 28);
            this.startQuery.TabIndex = 10;
            this.startQuery.Text = "Comenzar consulta";
            this.startQuery.UseVisualStyleBackColor = true;
            this.startQuery.Click += new System.EventHandler(this.startQuery_Click);
            // 
            // puerto
            // 
            this.puerto.AutoSize = true;
            this.puerto.Location = new System.Drawing.Point(18, 76);
            this.puerto.Name = "puerto";
            this.puerto.Size = new System.Drawing.Size(68, 13);
            this.puerto.TabIndex = 11;
            this.puerto.Text = "Puerto salida";
            // 
            // velocidad
            // 
            this.velocidad.AutoSize = true;
            this.velocidad.Location = new System.Drawing.Point(32, 117);
            this.velocidad.Name = "velocidad";
            this.velocidad.Size = new System.Drawing.Size(54, 13);
            this.velocidad.TabIndex = 12;
            this.velocidad.Text = "Velocidad";
            // 
            // dataBits
            // 
            this.dataBits.AutoSize = true;
            this.dataBits.Location = new System.Drawing.Point(37, 158);
            this.dataBits.Name = "dataBits";
            this.dataBits.Size = new System.Drawing.Size(49, 13);
            this.dataBits.TabIndex = 13;
            this.dataBits.Text = "Data bits";
            // 
            // stopBits
            // 
            this.stopBits.AutoSize = true;
            this.stopBits.Location = new System.Drawing.Point(38, 199);
            this.stopBits.Name = "stopBits";
            this.stopBits.Size = new System.Drawing.Size(48, 13);
            this.stopBits.TabIndex = 14;
            this.stopBits.Text = "Stop bits";
            // 
            // paridad
            // 
            this.paridad.AutoSize = true;
            this.paridad.Location = new System.Drawing.Point(43, 236);
            this.paridad.Name = "paridad";
            this.paridad.Size = new System.Drawing.Size(43, 13);
            this.paridad.TabIndex = 15;
            this.paridad.Text = "Paridad";
            // 
            // saludo
            // 
            this.saludo.AutoSize = true;
            this.saludo.Location = new System.Drawing.Point(46, 273);
            this.saludo.Name = "saludo";
            this.saludo.Size = new System.Drawing.Size(40, 13);
            this.saludo.TabIndex = 16;
            this.saludo.Text = "Saludo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Configuración del puerto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Configuración de la trama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "ID dispositivo";
            // 
            // labelFirstParam
            // 
            this.labelFirstParam.AutoSize = true;
            this.labelFirstParam.Location = new System.Drawing.Point(282, 113);
            this.labelFirstParam.Name = "labelFirstParam";
            this.labelFirstParam.Size = new System.Drawing.Size(81, 13);
            this.labelFirstParam.TabIndex = 20;
            this.labelFirstParam.Text = "Dirección inicial";
            // 
            // labelSecond
            // 
            this.labelSecond.AutoSize = true;
            this.labelSecond.Location = new System.Drawing.Point(254, 155);
            this.labelSecond.Name = "labelSecond";
            this.labelSecond.Size = new System.Drawing.Size(109, 13);
            this.labelSecond.TabIndex = 21;
            this.labelSecond.Text = "Cantidad de variables";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Función a implementar";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(309, 387);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Intentos";
            // 
            // inputFunction
            // 
            this.inputFunction.FormattingEnabled = true;
            this.inputFunction.Location = new System.Drawing.Point(378, 72);
            this.inputFunction.Name = "inputFunction";
            this.inputFunction.Size = new System.Drawing.Size(124, 21);
            this.inputFunction.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(366, 364);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Configuración de reintentos";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(309, 421);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Timeout";
            // 
            // binOutput
            // 
            this.binOutput.Location = new System.Drawing.Point(601, 278);
            this.binOutput.Name = "binOutput";
            this.binOutput.Size = new System.Drawing.Size(336, 118);
            this.binOutput.TabIndex = 32;
            this.binOutput.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(550, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Decimal";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(527, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Hexadecimal";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(556, 278);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Binario";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(602, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 13);
            this.label13.TabIndex = 37;
            this.label13.Text = "Valores recibidos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 468);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "Consulta";
            // 
            // stopQuery
            // 
            this.stopQuery.Location = new System.Drawing.Point(250, 494);
            this.stopQuery.Name = "stopQuery";
            this.stopQuery.Size = new System.Drawing.Size(139, 28);
            this.stopQuery.TabIndex = 39;
            this.stopQuery.Text = "Detener consulta";
            this.stopQuery.UseVisualStyleBackColor = true;
            this.stopQuery.Click += new System.EventHandler(this.stopQuery_Click);
            // 
            // toSendTextBox
            // 
            this.toSendTextBox.Location = new System.Drawing.Point(85, 465);
            this.toSendTextBox.Name = "toSendTextBox";
            this.toSendTextBox.ReadOnly = true;
            this.toSendTextBox.Size = new System.Drawing.Size(1086, 20);
            this.toSendTextBox.TabIndex = 40;
            // 
            // inputDispositiveId
            // 
            this.inputDispositiveId.Location = new System.Drawing.Point(378, 27);
            this.inputDispositiveId.Name = "inputDispositiveId";
            this.inputDispositiveId.Size = new System.Drawing.Size(124, 20);
            this.inputDispositiveId.TabIndex = 41;
            // 
            // inputFirstParam
            // 
            this.inputFirstParam.Location = new System.Drawing.Point(378, 111);
            this.inputFirstParam.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.inputFirstParam.Name = "inputFirstParam";
            this.inputFirstParam.Size = new System.Drawing.Size(124, 20);
            this.inputFirstParam.TabIndex = 42;
            // 
            // inputSecondParam
            // 
            this.inputSecondParam.Location = new System.Drawing.Point(378, 155);
            this.inputSecondParam.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.inputSecondParam.Name = "inputSecondParam";
            this.inputSecondParam.Size = new System.Drawing.Size(124, 20);
            this.inputSecondParam.TabIndex = 43;
            // 
            // numberOfRetriesInput
            // 
            this.numberOfRetriesInput.Location = new System.Drawing.Point(381, 385);
            this.numberOfRetriesInput.Name = "numberOfRetriesInput";
            this.numberOfRetriesInput.Size = new System.Drawing.Size(121, 20);
            this.numberOfRetriesInput.TabIndex = 44;
            this.numberOfRetriesInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // timeoutInput
            // 
            this.timeoutInput.Location = new System.Drawing.Point(381, 419);
            this.timeoutInput.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.timeoutInput.Name = "timeoutInput";
            this.timeoutInput.Size = new System.Drawing.Size(121, 20);
            this.timeoutInput.TabIndex = 45;
            this.timeoutInput.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // cleanOutputButton
            // 
            this.cleanOutputButton.Location = new System.Drawing.Point(395, 494);
            this.cleanOutputButton.Name = "cleanOutputButton";
            this.cleanOutputButton.Size = new System.Drawing.Size(139, 28);
            this.cleanOutputButton.TabIndex = 46;
            this.cleanOutputButton.Text = "Limpiar salidas";
            this.cleanOutputButton.UseVisualStyleBackColor = true;
            this.cleanOutputButton.Click += new System.EventHandler(this.cleanOutputButton_Click);
            // 
            // labelThirdParam
            // 
            this.labelThirdParam.AutoSize = true;
            this.labelThirdParam.Location = new System.Drawing.Point(261, 196);
            this.labelThirdParam.Name = "labelThirdParam";
            this.labelThirdParam.Size = new System.Drawing.Size(102, 13);
            this.labelThirdParam.TabIndex = 47;
            this.labelThirdParam.Text = "Valores de variables";
            // 
            // inputThirdParam
            // 
            this.inputThirdParam.Location = new System.Drawing.Point(378, 196);
            this.inputThirdParam.Name = "inputThirdParam";
            this.inputThirdParam.Size = new System.Drawing.Size(124, 140);
            this.inputThirdParam.TabIndex = 48;
            this.inputThirdParam.Text = "";
            // 
            // errorTextBox
            // 
            this.errorTextBox.Location = new System.Drawing.Point(85, 532);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.ReadOnly = true;
            this.errorTextBox.Size = new System.Drawing.Size(1086, 20);
            this.errorTextBox.TabIndex = 49;
            // 
            // tableOutput
            // 
            this.tableOutput.Location = new System.Drawing.Point(947, 28);
            this.tableOutput.Name = "tableOutput";
            this.tableOutput.Size = new System.Drawing.Size(224, 368);
            this.tableOutput.TabIndex = 50;
            this.tableOutput.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 532);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Estado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Tipo conexión";
            // 
            // connectionType
            // 
            this.connectionType.FormattingEnabled = true;
            this.connectionType.Location = new System.Drawing.Point(103, 31);
            this.connectionType.Name = "connectionType";
            this.connectionType.Size = new System.Drawing.Size(121, 21);
            this.connectionType.TabIndex = 52;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 360);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 57;
            this.label15.Text = "Dirección";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 319);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 56;
            this.label16.Text = "Puerto salida";
            // 
            // tcpListeningPort
            // 
            this.tcpListeningPort.Location = new System.Drawing.Point(103, 316);
            this.tcpListeningPort.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.tcpListeningPort.Name = "tcpListeningPort";
            this.tcpListeningPort.Size = new System.Drawing.Size(121, 20);
            this.tcpListeningPort.TabIndex = 58;
            this.tcpListeningPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            // 
            // tcpConnectionAddress
            // 
            this.tcpConnectionAddress.Enabled = false;
            this.tcpConnectionAddress.Location = new System.Drawing.Point(103, 357);
            this.tcpConnectionAddress.Name = "tcpConnectionAddress";
            this.tcpConnectionAddress.Size = new System.Drawing.Size(121, 20);
            this.tcpConnectionAddress.TabIndex = 59;
            this.tcpConnectionAddress.Text = "127.0.0.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 560);
            this.Controls.Add(this.tcpConnectionAddress);
            this.Controls.Add(this.tcpListeningPort);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.connectionType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tableOutput);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.inputThirdParam);
            this.Controls.Add(this.labelThirdParam);
            this.Controls.Add(this.cleanOutputButton);
            this.Controls.Add(this.timeoutInput);
            this.Controls.Add(this.numberOfRetriesInput);
            this.Controls.Add(this.inputSecondParam);
            this.Controls.Add(this.inputFirstParam);
            this.Controls.Add(this.inputDispositiveId);
            this.Controls.Add(this.toSendTextBox);
            this.Controls.Add(this.stopQuery);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.binOutput);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.inputFunction);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelSecond);
            this.Controls.Add(this.labelFirstParam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saludo);
            this.Controls.Add(this.paridad);
            this.Controls.Add(this.stopBits);
            this.Controls.Add(this.dataBits);
            this.Controls.Add(this.velocidad);
            this.Controls.Add(this.puerto);
            this.Controls.Add(this.startQuery);
            this.Controls.Add(this.decimalOutput);
            this.Controls.Add(this.hexaOutput);
            this.Controls.Add(this.cboHandShaking);
            this.Controls.Add(this.cboParity);
            this.Controls.Add(this.cboStopBits);
            this.Controls.Add(this.cboDataBits);
            this.Controls.Add(this.cboBaudRate);
            this.Controls.Add(this.cboPorts);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.inputDispositiveId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputFirstParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSecondParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfRetriesInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcpListeningPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.ComboBox cboHandShaking;
        private System.Windows.Forms.RichTextBox hexaOutput;
        private System.Windows.Forms.RichTextBox decimalOutput;
        private System.Windows.Forms.Button startQuery;
        private System.Windows.Forms.Label puerto;
        private System.Windows.Forms.Label velocidad;
        private System.Windows.Forms.Label dataBits;
        private System.Windows.Forms.Label stopBits;
        private System.Windows.Forms.Label paridad;
        private System.Windows.Forms.Label saludo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelFirstParam;
        private System.Windows.Forms.Label labelSecond;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox inputFunction;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox binOutput;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button stopQuery;
        private System.Windows.Forms.TextBox toSendTextBox;
        private System.Windows.Forms.NumericUpDown inputFirstParam;
        private System.Windows.Forms.NumericUpDown inputSecondParam;
        private System.Windows.Forms.NumericUpDown numberOfRetriesInput;
        private System.Windows.Forms.NumericUpDown timeoutInput;
        private System.Windows.Forms.Button cleanOutputButton;
        private System.Windows.Forms.Label labelThirdParam;
        private System.Windows.Forms.RichTextBox inputThirdParam;
        private System.Windows.Forms.TextBox errorTextBox;
        private System.Windows.Forms.RichTextBox tableOutput;
        public System.Windows.Forms.NumericUpDown inputDispositiveId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox connectionType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.NumericUpDown tcpListeningPort;
        private System.Windows.Forms.TextBox tcpConnectionAddress;
    }
}
