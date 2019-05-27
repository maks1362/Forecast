﻿namespace Forecast
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forecastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.ComboBoxMethod = new System.Windows.Forms.ComboBox();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ShowChart = new System.Windows.Forms.Button();
            this.PeriodNumeric = new System.Windows.Forms.NumericUpDown();
            this.LabelDalnPrognoza = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.buttonSgladitb = new System.Windows.Forms.Button();
            this.comboBoxMethods = new System.Windows.Forms.ComboBox();
            this.labelMethodTrend = new System.Windows.Forms.Label();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(987, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "&Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openToolStripMenuItem.Text = "Открыть...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveToolStripMenuItem.Text = "Сохранить";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveAsToolStripMenuItem.Text = "Сохранить как...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forecastToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.actionToolStripMenuItem.Text = "Действие";
            // 
            // forecastToolStripMenuItem
            // 
            this.forecastToolStripMenuItem.Name = "forecastToolStripMenuItem";
            this.forecastToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.forecastToolStripMenuItem.Text = "Спрогнозировать";
            this.forecastToolStripMenuItem.Click += new System.EventHandler(this.forecast_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clearToolStripMenuItem.Text = "Очистить";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.aboutToolStripMenuItem.Text = "О программе...";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Location = new System.Drawing.Point(5, 22);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(202, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // ComboBoxMethod
            // 
            this.ComboBoxMethod.DataSource = this.form1BindingSource;
            this.ComboBoxMethod.FormattingEnabled = true;
            this.ComboBoxMethod.Location = new System.Drawing.Point(12, 49);
            this.ComboBoxMethod.Name = "ComboBoxMethod";
            this.ComboBoxMethod.Size = new System.Drawing.Size(195, 21);
            this.ComboBoxMethod.TabIndex = 3;
            this.ComboBoxMethod.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMethod_SelectedIndexChanged);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(Forecast.Form1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Способ прогноза:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 315);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(963, 123);
            this.dataGridView1.TabIndex = 8;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.Gray;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(219, 24);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Serie";
            series1.YValuesPerPoint = 7;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(756, 285);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // dataBindingSource
            // 
            this.dataBindingSource.DataSource = typeof(Forecast.Data);
            // 
            // ShowChart
            // 
            this.ShowChart.Location = new System.Drawing.Point(80, 273);
            this.ShowChart.Name = "ShowChart";
            this.ShowChart.Size = new System.Drawing.Size(127, 23);
            this.ShowChart.TabIndex = 10;
            this.ShowChart.Text = "Спрогнозировать";
            this.ShowChart.UseVisualStyleBackColor = true;
            this.ShowChart.Click += new System.EventHandler(this.forecast_Click);
            // 
            // PeriodNumeric
            // 
            this.PeriodNumeric.Location = new System.Drawing.Point(11, 98);
            this.PeriodNumeric.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.PeriodNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PeriodNumeric.Name = "PeriodNumeric";
            this.PeriodNumeric.Size = new System.Drawing.Size(120, 20);
            this.PeriodNumeric.TabIndex = 11;
            this.PeriodNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PeriodNumeric.Visible = false;
            // 
            // LabelDalnPrognoza
            // 
            this.LabelDalnPrognoza.AutoSize = true;
            this.LabelDalnPrognoza.Location = new System.Drawing.Point(11, 79);
            this.LabelDalnPrognoza.Name = "LabelDalnPrognoza";
            this.LabelDalnPrognoza.Size = new System.Drawing.Size(161, 13);
            this.LabelDalnPrognoza.TabIndex = 12;
            this.LabelDalnPrognoza.Text = "Дальность прогноза(ед. ряда)";
            this.LabelDalnPrognoza.Visible = false;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(11, 132);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(50, 13);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "Ошибка:";
            this.labelError.Visible = false;
            // 
            // buttonSgladitb
            // 
            this.buttonSgladitb.Location = new System.Drawing.Point(75, 222);
            this.buttonSgladitb.Name = "buttonSgladitb";
            this.buttonSgladitb.Size = new System.Drawing.Size(132, 45);
            this.buttonSgladitb.TabIndex = 14;
            this.buttonSgladitb.Text = "Сглаживание методом скользящей средней";
            this.buttonSgladitb.UseVisualStyleBackColor = true;
            this.buttonSgladitb.Visible = false;
            this.buttonSgladitb.Click += new System.EventHandler(this.AvarageChart);
            // 
            // comboBoxMethods
            // 
            this.comboBoxMethods.DataSource = this.form1BindingSource;
            this.comboBoxMethods.FormattingEnabled = true;
            this.comboBoxMethods.Location = new System.Drawing.Point(12, 95);
            this.comboBoxMethods.Name = "comboBoxMethods";
            this.comboBoxMethods.Size = new System.Drawing.Size(145, 21);
            this.comboBoxMethods.TabIndex = 15;
            this.comboBoxMethods.Visible = false;
            // 
            // labelMethodTrend
            // 
            this.labelMethodTrend.AutoSize = true;
            this.labelMethodTrend.Location = new System.Drawing.Point(12, 79);
            this.labelMethodTrend.Name = "labelMethodTrend";
            this.labelMethodTrend.Size = new System.Drawing.Size(106, 13);
            this.labelMethodTrend.TabIndex = 16;
            this.labelMethodTrend.Text = "Расчёт с помощью:";
            this.labelMethodTrend.Visible = false;
            // 
            // textBoxError
            // 
            this.textBoxError.Location = new System.Drawing.Point(67, 129);
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.ReadOnly = true;
            this.textBoxError.Size = new System.Drawing.Size(100, 20);
            this.textBoxError.TabIndex = 17;
            this.textBoxError.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 450);
            this.Controls.Add(this.textBoxError);
            this.Controls.Add(this.labelMethodTrend);
            this.Controls.Add(this.comboBoxMethods);
            this.Controls.Add(this.buttonSgladitb);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.LabelDalnPrognoza);
            this.Controls.Add(this.PeriodNumeric);
            this.Controls.Add(this.ShowChart);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBoxMethod);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ПрогнозМастер";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forecastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ComboBox ComboBoxMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.BindingSource dataBindingSource;
        private System.Windows.Forms.Button ShowChart;
        private System.Windows.Forms.NumericUpDown PeriodNumeric;
        private System.Windows.Forms.Label LabelDalnPrognoza;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button buttonSgladitb;
        private System.Windows.Forms.ComboBox comboBoxMethods;
        private System.Windows.Forms.Label labelMethodTrend;
        private System.Windows.Forms.TextBox textBoxError;
    }
}

