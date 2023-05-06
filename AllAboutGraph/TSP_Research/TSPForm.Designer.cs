namespace TSP_Research
{
    /// <summary>
    /// Форма для вывода результатов работы алгоритмов решения TSP
    /// </summary>
    partial class TSPForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TSPchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.FullSearchResult = new System.Windows.Forms.TextBox();
            this.NearestNeighbourResult = new System.Windows.Forms.TextBox();
            this.SimulatedAnnealingResult = new System.Windows.Forms.TextBox();
            this.ImprovedNearestNeighbourResult = new System.Windows.Forms.TextBox();
            this.AntColonyResult = new System.Windows.Forms.TextBox();
            this.labelLengths = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelStep = new System.Windows.Forms.Label();
            this.labelMaxValue = new System.Windows.Forms.Label();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.textBoxMaxValue = new System.Windows.Forms.TextBox();
            this.buttonCalc = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.BranchesAndBoundariesResult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TSPchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TSPchart
            // 
            this.TSPchart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.TSPchart.ChartAreas.Add(chartArea1);
            this.TSPchart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.TSPchart.Legends.Add(legend1);
            this.TSPchart.Location = new System.Drawing.Point(468, 0);
            this.TSPchart.Name = "TSPchart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Метод полного перебора";
            series1.YValuesPerPoint = 2;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "Метод ближайшего соседа";
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Color = System.Drawing.Color.Lime;
            series3.Legend = "Legend1";
            series3.Name = "Ближайший сосед (усовершенствованный)";
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.Fuchsia;
            series4.Legend = "Legend1";
            series4.Name = "Метод имитации отжига";
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series5.Legend = "Legend1";
            series5.Name = "Алгоритм муравьиной колонии";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "Метод ветвей и границ";
            this.TSPchart.Series.Add(series1);
            this.TSPchart.Series.Add(series2);
            this.TSPchart.Series.Add(series3);
            this.TSPchart.Series.Add(series4);
            this.TSPchart.Series.Add(series5);
            this.TSPchart.Series.Add(series6);
            this.TSPchart.Size = new System.Drawing.Size(838, 807);
            this.TSPchart.TabIndex = 0;
            this.TSPchart.Text = "chart1";
            // 
            // Canvas
            // 
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Left;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(468, 807);
            this.Canvas.TabIndex = 1;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // FullSearchResult
            // 
            this.FullSearchResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullSearchResult.Location = new System.Drawing.Point(13, 283);
            this.FullSearchResult.Name = "FullSearchResult";
            this.FullSearchResult.Size = new System.Drawing.Size(275, 33);
            this.FullSearchResult.TabIndex = 2;
            // 
            // NearestNeighbourResult
            // 
            this.NearestNeighbourResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NearestNeighbourResult.Location = new System.Drawing.Point(13, 339);
            this.NearestNeighbourResult.Name = "NearestNeighbourResult";
            this.NearestNeighbourResult.Size = new System.Drawing.Size(275, 33);
            this.NearestNeighbourResult.TabIndex = 3;
            // 
            // SimulatedAnnealingResult
            // 
            this.SimulatedAnnealingResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SimulatedAnnealingResult.Location = new System.Drawing.Point(13, 468);
            this.SimulatedAnnealingResult.Name = "SimulatedAnnealingResult";
            this.SimulatedAnnealingResult.Size = new System.Drawing.Size(275, 33);
            this.SimulatedAnnealingResult.TabIndex = 5;
            // 
            // ImprovedNearestNeighbourResult
            // 
            this.ImprovedNearestNeighbourResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ImprovedNearestNeighbourResult.Location = new System.Drawing.Point(13, 412);
            this.ImprovedNearestNeighbourResult.Name = "ImprovedNearestNeighbourResult";
            this.ImprovedNearestNeighbourResult.Size = new System.Drawing.Size(275, 33);
            this.ImprovedNearestNeighbourResult.TabIndex = 4;
            // 
            // AntColonyResult
            // 
            this.AntColonyResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AntColonyResult.Location = new System.Drawing.Point(13, 524);
            this.AntColonyResult.Name = "AntColonyResult";
            this.AntColonyResult.Size = new System.Drawing.Size(275, 33);
            this.AntColonyResult.TabIndex = 7;
            // 
            // labelLengths
            // 
            this.labelLengths.AutoSize = true;
            this.labelLengths.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLengths.Location = new System.Drawing.Point(9, 235);
            this.labelLengths.Name = "labelLengths";
            this.labelLengths.Size = new System.Drawing.Size(223, 21);
            this.labelLengths.TabIndex = 8;
            this.labelLengths.Text = "Оценка точности алгоритмов";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.BranchesAndBoundariesResult);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelStep);
            this.panel1.Controls.Add(this.labelMaxValue);
            this.panel1.Controls.Add(this.textBoxStep);
            this.panel1.Controls.Add(this.textBoxMaxValue);
            this.panel1.Controls.Add(this.buttonCalc);
            this.panel1.Controls.Add(this.labelLengths);
            this.panel1.Controls.Add(this.FullSearchResult);
            this.panel1.Controls.Add(this.AntColonyResult);
            this.panel1.Controls.Add(this.NearestNeighbourResult);
            this.panel1.Controls.Add(this.ImprovedNearestNeighbourResult);
            this.panel1.Controls.Add(this.SimulatedAnnealingResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1306, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 807);
            this.panel1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 504);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Алгоритм муравьиной колонии";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(13, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Метод имитации отжига";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(13, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 34);
            this.label3.TabIndex = 16;
            this.label3.Text = "Усовершенствованный метод\r\n ближайшего соседа";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(13, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Метод ближайшего соседа";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Метод полного перебора";
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Location = new System.Drawing.Point(13, 19);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(27, 13);
            this.labelStep.TabIndex = 13;
            this.labelStep.Text = "Шаг";
            // 
            // labelMaxValue
            // 
            this.labelMaxValue.AutoSize = true;
            this.labelMaxValue.Location = new System.Drawing.Point(13, 68);
            this.labelMaxValue.Name = "labelMaxValue";
            this.labelMaxValue.Size = new System.Drawing.Size(134, 13);
            this.labelMaxValue.TabIndex = 12;
            this.labelMaxValue.Text = "Максимальное значение";
            // 
            // textBoxStep
            // 
            this.textBoxStep.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxStep.Location = new System.Drawing.Point(13, 35);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(219, 27);
            this.textBoxStep.TabIndex = 11;
            // 
            // textBoxMaxValue
            // 
            this.textBoxMaxValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMaxValue.Location = new System.Drawing.Point(13, 87);
            this.textBoxMaxValue.Name = "textBoxMaxValue";
            this.textBoxMaxValue.Size = new System.Drawing.Size(219, 27);
            this.textBoxMaxValue.TabIndex = 10;
            // 
            // buttonCalc
            // 
            this.buttonCalc.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalc.Location = new System.Drawing.Point(13, 127);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(219, 38);
            this.buttonCalc.TabIndex = 9;
            this.buttonCalc.Text = "Рассчитать";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(13, 562);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Метод ветвей и границ";
            // 
            // BranchesAndBoundariesResult
            // 
            this.BranchesAndBoundariesResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BranchesAndBoundariesResult.Location = new System.Drawing.Point(13, 582);
            this.BranchesAndBoundariesResult.Name = "BranchesAndBoundariesResult";
            this.BranchesAndBoundariesResult.Size = new System.Drawing.Size(275, 33);
            this.BranchesAndBoundariesResult.TabIndex = 19;
            // 
            // TSPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1607, 807);
            this.Controls.Add(this.TSPchart);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.panel1);
            this.Name = "TSPForm";
            this.Text = "TSPResearch";
            ((System.ComponentModel.ISupportInitialize)(this.TSPchart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart TSPchart;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.TextBox FullSearchResult;
        private System.Windows.Forms.TextBox NearestNeighbourResult;
        private System.Windows.Forms.TextBox SimulatedAnnealingResult;
        private System.Windows.Forms.TextBox ImprovedNearestNeighbourResult;
        private System.Windows.Forms.TextBox AntColonyResult;
        private System.Windows.Forms.Label labelLengths;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Label labelMaxValue;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.TextBox textBoxMaxValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox BranchesAndBoundariesResult;
    }
}

