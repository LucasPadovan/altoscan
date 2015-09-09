namespace WindowsFormsApplication1
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
            this.functionToImplement = new System.Windows.Forms.ComboBox();
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
            this.dispositiveID = new System.Windows.Forms.NumericUpDown();
            this.inputFirstParam = new System.Windows.Forms.NumericUpDown();
            this.inputSecondParam = new System.Windows.Forms.NumericUpDown();
            this.retriesNumber = new System.Windows.Forms.NumericUpDown();
            this.retryTimeout = new System.Windows.Forms.NumericUpDown();
            this.cleanOutputButton = new System.Windows.Forms.Button();
            this.labelThirdParam = new System.Windows.Forms.Label();
            this.inputThirdParam = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dispositiveID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputFirstParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSecondParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retriesNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // cboPorts
            // 
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(103, 27);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(121, 21);
            this.cboPorts.TabIndex = 1;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Location = new System.Drawing.Point(103, 69);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cboBaudRate.TabIndex = 2;
            // 
            // cboDataBits
            // 
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(103, 110);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(121, 21);
            this.cboDataBits.TabIndex = 3;
            // 
            // cboStopBits
            // 
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(103, 151);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(121, 21);
            this.cboStopBits.TabIndex = 4;
            // 
            // cboParity
            // 
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(103, 188);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(121, 21);
            this.cboParity.TabIndex = 5;
            // 
            // cboHandShaking
            // 
            this.cboHandShaking.Enabled = false;
            this.cboHandShaking.FormattingEnabled = true;
            this.cboHandShaking.Location = new System.Drawing.Point(103, 225);
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
            this.decimalOutput.Size = new System.Drawing.Size(340, 120);
            this.decimalOutput.TabIndex = 8;
            this.decimalOutput.Text = "";
            // 
            // startQuery
            // 
            this.startQuery.Location = new System.Drawing.Point(105, 399);
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
            this.puerto.Location = new System.Drawing.Point(8, 31);
            this.puerto.Name = "puerto";
            this.puerto.Size = new System.Drawing.Size(68, 13);
            this.puerto.TabIndex = 11;
            this.puerto.Text = "Puerto salida";
            // 
            // velocidad
            // 
            this.velocidad.AutoSize = true;
            this.velocidad.Location = new System.Drawing.Point(22, 72);
            this.velocidad.Name = "velocidad";
            this.velocidad.Size = new System.Drawing.Size(54, 13);
            this.velocidad.TabIndex = 12;
            this.velocidad.Text = "Velocidad";
            // 
            // dataBits
            // 
            this.dataBits.AutoSize = true;
            this.dataBits.Location = new System.Drawing.Point(27, 113);
            this.dataBits.Name = "dataBits";
            this.dataBits.Size = new System.Drawing.Size(49, 13);
            this.dataBits.TabIndex = 13;
            this.dataBits.Text = "Data bits";
            // 
            // stopBits
            // 
            this.stopBits.AutoSize = true;
            this.stopBits.Location = new System.Drawing.Point(28, 154);
            this.stopBits.Name = "stopBits";
            this.stopBits.Size = new System.Drawing.Size(48, 13);
            this.stopBits.TabIndex = 14;
            this.stopBits.Text = "Stop bits";
            // 
            // paridad
            // 
            this.paridad.AutoSize = true;
            this.paridad.Location = new System.Drawing.Point(33, 191);
            this.paridad.Name = "paridad";
            this.paridad.Size = new System.Drawing.Size(43, 13);
            this.paridad.TabIndex = 15;
            this.paridad.Text = "Paridad";
            // 
            // saludo
            // 
            this.saludo.AutoSize = true;
            this.saludo.Location = new System.Drawing.Point(36, 228);
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
            this.label3.Location = new System.Drawing.Point(289, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "ID dispositivo";
            // 
            // labelFirstParam
            // 
            this.labelFirstParam.AutoSize = true;
            this.labelFirstParam.Location = new System.Drawing.Point(278, 114);
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
            this.label7.Location = new System.Drawing.Point(18, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Reintentos";
            // 
            // functionToImplement
            // 
            this.functionToImplement.FormattingEnabled = true;
            this.functionToImplement.Location = new System.Drawing.Point(378, 72);
            this.functionToImplement.Name = "functionToImplement";
            this.functionToImplement.Size = new System.Drawing.Size(124, 21);
            this.functionToImplement.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Configuración de reintentos";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 318);
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
            this.label14.Location = new System.Drawing.Point(31, 379);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "Consulta";
            // 
            // stopQuery
            // 
            this.stopQuery.Location = new System.Drawing.Point(250, 399);
            this.stopQuery.Name = "stopQuery";
            this.stopQuery.Size = new System.Drawing.Size(139, 28);
            this.stopQuery.TabIndex = 39;
            this.stopQuery.Text = "Detener consulta";
            this.stopQuery.UseVisualStyleBackColor = true;
            this.stopQuery.Click += new System.EventHandler(this.stopQuery_Click);
            // 
            // toSendTextBox
            // 
            this.toSendTextBox.Location = new System.Drawing.Point(105, 376);
            this.toSendTextBox.Name = "toSendTextBox";
            this.toSendTextBox.ReadOnly = true;
            this.toSendTextBox.Size = new System.Drawing.Size(491, 20);
            this.toSendTextBox.TabIndex = 40;
            // 
            // dispositiveID
            // 
            this.dispositiveID.Location = new System.Drawing.Point(378, 27);
            this.dispositiveID.Name = "dispositiveID";
            this.dispositiveID.Size = new System.Drawing.Size(124, 20);
            this.dispositiveID.TabIndex = 41;
            // 
            // inputFirstParam
            // 
            this.inputFirstParam.Location = new System.Drawing.Point(378, 111);
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
            // retriesNumber
            // 
            this.retriesNumber.Location = new System.Drawing.Point(103, 282);
            this.retriesNumber.Name = "retriesNumber";
            this.retriesNumber.Size = new System.Drawing.Size(121, 20);
            this.retriesNumber.TabIndex = 44;
            // 
            // retryTimeout
            // 
            this.retryTimeout.Location = new System.Drawing.Point(103, 316);
            this.retryTimeout.Name = "retryTimeout";
            this.retryTimeout.Size = new System.Drawing.Size(121, 20);
            this.retryTimeout.TabIndex = 45;
            // 
            // cleanOutputButton
            // 
            this.cleanOutputButton.Location = new System.Drawing.Point(395, 399);
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
            this.labelThirdParam.Location = new System.Drawing.Point(257, 196);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 443);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(926, 20);
            this.textBox1.TabIndex = 49;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 466);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.inputThirdParam);
            this.Controls.Add(this.labelThirdParam);
            this.Controls.Add(this.cleanOutputButton);
            this.Controls.Add(this.retryTimeout);
            this.Controls.Add(this.retriesNumber);
            this.Controls.Add(this.inputSecondParam);
            this.Controls.Add(this.inputFirstParam);
            this.Controls.Add(this.dispositiveID);
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
            this.Controls.Add(this.functionToImplement);
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
            ((System.ComponentModel.ISupportInitialize)(this.dispositiveID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputFirstParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSecondParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retriesNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeout)).EndInit();
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
        private System.Windows.Forms.ComboBox functionToImplement;
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
        private System.Windows.Forms.NumericUpDown retriesNumber;
        private System.Windows.Forms.NumericUpDown retryTimeout;
        private System.Windows.Forms.Button cleanOutputButton;
        private System.Windows.Forms.Label labelThirdParam;
        private System.Windows.Forms.RichTextBox inputThirdParam;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.NumericUpDown dispositiveID;
    }
}

