namespace TSP_Research
{
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
            this.BranchesAndBoundariesResult = new System.Windows.Forms.TextBox();
            this.labelLengths = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
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
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series5.Legend = "Legend1";
            series5.Name = "Метод ветвей и границ";
            series6.BorderWidth = 3;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series6.Legend = "Legend1";
            series6.Name = "Алгоритм муравьиной колонии";
            this.TSPchart.Series.Add(series1);
            this.TSPchart.Series.Add(series2);
            this.TSPchart.Series.Add(series3);
            this.TSPchart.Series.Add(series4);
            this.TSPchart.Series.Add(series5);
            this.TSPchart.Series.Add(series6);
            this.TSPchart.Size = new System.Drawing.Size(664, 775);
            this.TSPchart.TabIndex = 0;
            this.TSPchart.Text = "chart1";
            // 
            // Canvas
            // 
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Left;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(468, 775);
            this.Canvas.TabIndex = 1;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // FullSearchResult
            // 
            this.FullSearchResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullSearchResult.Location = new System.Drawing.Point(13, 259);
            this.FullSearchResult.Name = "FullSearchResult";
            this.FullSearchResult.Size = new System.Drawing.Size(219, 33);
            this.FullSearchResult.TabIndex = 2;
            // 
            // NearestNeighbourResult
            // 
            this.NearestNeighbourResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NearestNeighbourResult.Location = new System.Drawing.Point(13, 298);
            this.NearestNeighbourResult.Name = "NearestNeighbourResult";
            this.NearestNeighbourResult.Size = new System.Drawing.Size(219, 33);
            this.NearestNeighbourResult.TabIndex = 3;
            // 
            // SimulatedAnnealingResult
            // 
            this.SimulatedAnnealingResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SimulatedAnnealingResult.Location = new System.Drawing.Point(13, 376);
            this.SimulatedAnnealingResult.Name = "SimulatedAnnealingResult";
            this.SimulatedAnnealingResult.Size = new System.Drawing.Size(219, 33);
            this.SimulatedAnnealingResult.TabIndex = 5;
            // 
            // ImprovedNearestNeighbourResult
            // 
            this.ImprovedNearestNeighbourResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ImprovedNearestNeighbourResult.Location = new System.Drawing.Point(13, 337);
            this.ImprovedNearestNeighbourResult.Name = "ImprovedNearestNeighbourResult";
            this.ImprovedNearestNeighbourResult.Size = new System.Drawing.Size(219, 33);
            this.ImprovedNearestNeighbourResult.TabIndex = 4;
            // 
            // AntColonyResult
            // 
            this.AntColonyResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AntColonyResult.Location = new System.Drawing.Point(13, 454);
            this.AntColonyResult.Name = "AntColonyResult";
            this.AntColonyResult.Size = new System.Drawing.Size(219, 33);
            this.AntColonyResult.TabIndex = 7;
            // 
            // BranchesAndBoundariesResult
            // 
            this.BranchesAndBoundariesResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BranchesAndBoundariesResult.Location = new System.Drawing.Point(13, 415);
            this.BranchesAndBoundariesResult.Name = "BranchesAndBoundariesResult";
            this.BranchesAndBoundariesResult.Size = new System.Drawing.Size(219, 33);
            this.BranchesAndBoundariesResult.TabIndex = 6;
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
            this.panel1.Controls.Add(this.labelLengths);
            this.panel1.Controls.Add(this.FullSearchResult);
            this.panel1.Controls.Add(this.AntColonyResult);
            this.panel1.Controls.Add(this.NearestNeighbourResult);
            this.panel1.Controls.Add(this.BranchesAndBoundariesResult);
            this.panel1.Controls.Add(this.ImprovedNearestNeighbourResult);
            this.panel1.Controls.Add(this.SimulatedAnnealingResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1132, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 775);
            this.panel1.TabIndex = 9;
            // 
            // TSPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 775);
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
        private System.Windows.Forms.TextBox BranchesAndBoundariesResult;
        private System.Windows.Forms.Label labelLengths;
        private System.Windows.Forms.Panel panel1;
    }
}

