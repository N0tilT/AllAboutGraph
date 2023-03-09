namespace AllAboutGraph
{
    partial class View
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.panelTools = new System.Windows.Forms.Panel();
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.ConnectVerticesButton = new System.Windows.Forms.Button();
            this.RemoveObjButton = new System.Windows.Forms.Button();
            this.CreateGraphButton = new System.Windows.Forms.Button();
            this.comboBoxAlgorithms = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panelCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.panelTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelMain.Controls.Add(this.panelCanvas);
            this.panelMain.Controls.Add(this.panelTools);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1292, 610);
            this.panelMain.TabIndex = 0;
            // 
            // panelCanvas
            // 
            this.panelCanvas.BackColor = System.Drawing.Color.RosyBrown;
            this.panelCanvas.Controls.Add(this.Canvas);
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(0, 0);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(1026, 610);
            this.panelCanvas.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1026, 610);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.Resize += new System.EventHandler(this.Canvas_Resize);
            // 
            // panelTools
            // 
            this.panelTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panelTools.Controls.Add(this.comboBoxAlgorithms);
            this.panelTools.Controls.Add(this.AddVertexButton);
            this.panelTools.Controls.Add(this.ConnectVerticesButton);
            this.panelTools.Controls.Add(this.RemoveObjButton);
            this.panelTools.Controls.Add(this.CreateGraphButton);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTools.Location = new System.Drawing.Point(1026, 0);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(266, 610);
            this.panelTools.TabIndex = 1;
            // 
            // AddVertexButton
            // 
            this.AddVertexButton.Location = new System.Drawing.Point(17, 51);
            this.AddVertexButton.Name = "AddVertexButton";
            this.AddVertexButton.Size = new System.Drawing.Size(139, 23);
            this.AddVertexButton.TabIndex = 8;
            this.AddVertexButton.Text = "Add vertex";
            this.AddVertexButton.UseVisualStyleBackColor = true;
            // 
            // ConnectVerticesButton
            // 
            this.ConnectVerticesButton.Location = new System.Drawing.Point(17, 80);
            this.ConnectVerticesButton.Name = "ConnectVerticesButton";
            this.ConnectVerticesButton.Size = new System.Drawing.Size(139, 23);
            this.ConnectVerticesButton.TabIndex = 7;
            this.ConnectVerticesButton.Text = "Connect vertices";
            this.ConnectVerticesButton.UseVisualStyleBackColor = true;
            // 
            // RemoveObjButton
            // 
            this.RemoveObjButton.Location = new System.Drawing.Point(17, 109);
            this.RemoveObjButton.Name = "RemoveObjButton";
            this.RemoveObjButton.Size = new System.Drawing.Size(139, 23);
            this.RemoveObjButton.TabIndex = 6;
            this.RemoveObjButton.Text = "Remove object";
            this.RemoveObjButton.UseVisualStyleBackColor = true;
            // 
            // CreateGraphButton
            // 
            this.CreateGraphButton.Location = new System.Drawing.Point(17, 22);
            this.CreateGraphButton.Name = "CreateGraphButton";
            this.CreateGraphButton.Size = new System.Drawing.Size(139, 23);
            this.CreateGraphButton.TabIndex = 0;
            this.CreateGraphButton.Text = "Create graph";
            this.CreateGraphButton.UseVisualStyleBackColor = true;
            // 
            // comboBoxAlgorithms
            // 
            this.comboBoxAlgorithms.FormattingEnabled = true;
            this.comboBoxAlgorithms.Items.AddRange(new object[] {
            "10.1 Breadth-first search",
            "10.2 Depth-first search",
            "10.3 Print all paths",
            "10.4 All paths weights",
            "10.5 Precedence subgraph",
            "10.6 Bracket structure",
            "10.7 Vertex visit time",
            "11. Strongly connected components",
            "12.1. Find Euler cycle",
            "12.2. Fleury`s algorithm",
            "12.3. Find Hamiltonian cycle",
            "12.4. Algebraic method for Hamiltonian cycle",
            "12.5. Roberts and Flores algorithm",
            "12.6. Improved Roberts and Flores algorithm",
            "12.7. Multichain method",
            "12.8. Fundamental set of cycles",
            "1.1. Kruskal`s algorithm",
            "1.2. Prim`s algorithm",
            "2. Dijkstra`s algorithm",
            "3. Floyd`s algorithm",
            "4. Bellman-Ford`s algorithm"});
            this.comboBoxAlgorithms.Location = new System.Drawing.Point(17, 138);
            this.comboBoxAlgorithms.Name = "comboBoxAlgorithms";
            this.comboBoxAlgorithms.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAlgorithms.TabIndex = 9;
            this.comboBoxAlgorithms.Text = "Choose algorithm";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 610);
            this.Controls.Add(this.panelMain);
            this.Name = "View";
            this.Text = "All about graph";
            this.panelMain.ResumeLayout(false);
            this.panelCanvas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.panelTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button AddVertexButton;
        private System.Windows.Forms.Button ConnectVerticesButton;
        private System.Windows.Forms.Button RemoveObjButton;
        private System.Windows.Forms.Button CreateGraphButton;
        private System.Windows.Forms.ComboBox comboBoxAlgorithms;
    }
}

