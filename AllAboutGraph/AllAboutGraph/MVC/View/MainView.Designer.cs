namespace AllAboutGraph
{
    partial class MainView
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.panelTools = new System.Windows.Forms.Panel();
            this.CreationMethodLabel = new System.Windows.Forms.Label();
            this.textBoxGraphRepresentation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNumberOfVertices = new System.Windows.Forms.TextBox();
            this.comboBoxCreationMethodSelector = new System.Windows.Forms.ComboBox();
            this.comboBoxAlgorithms = new System.Windows.Forms.ComboBox();
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.ConnectVerticesButton = new System.Windows.Forms.Button();
            this.RemoveObjButton = new System.Windows.Forms.Button();
            this.CreateGraphButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.panelMain.Size = new System.Drawing.Size(999, 611);
            this.panelMain.TabIndex = 0;
            // 
            // panelCanvas
            // 
            this.panelCanvas.BackColor = System.Drawing.Color.RosyBrown;
            this.panelCanvas.Controls.Add(this.Canvas);
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(0, 0);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(736, 611);
            this.panelCanvas.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(736, 611);
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
            this.panelTools.Controls.Add(this.CreationMethodLabel);
            this.panelTools.Controls.Add(this.textBoxGraphRepresentation);
            this.panelTools.Controls.Add(this.label1);
            this.panelTools.Controls.Add(this.textBoxNumberOfVertices);
            this.panelTools.Controls.Add(this.comboBoxCreationMethodSelector);
            this.panelTools.Controls.Add(this.comboBoxAlgorithms);
            this.panelTools.Controls.Add(this.AddVertexButton);
            this.panelTools.Controls.Add(this.ConnectVerticesButton);
            this.panelTools.Controls.Add(this.RemoveObjButton);
            this.panelTools.Controls.Add(this.CreateGraphButton);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTools.Location = new System.Drawing.Point(736, 0);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(263, 611);
            this.panelTools.TabIndex = 1;
            // 
            // CreationMethodLabel
            // 
            this.CreationMethodLabel.AutoSize = true;
            this.CreationMethodLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreationMethodLabel.Location = new System.Drawing.Point(14, 137);
            this.CreationMethodLabel.Name = "CreationMethodLabel";
            this.CreationMethodLabel.Size = new System.Drawing.Size(124, 21);
            this.CreationMethodLabel.TabIndex = 14;
            this.CreationMethodLabel.Text = "AdjacencyMatrix";
            // 
            // textBoxGraphRepresentation
            // 
            this.textBoxGraphRepresentation.Location = new System.Drawing.Point(37, 170);
            this.textBoxGraphRepresentation.Multiline = true;
            this.textBoxGraphRepresentation.Name = "textBoxGraphRepresentation";
            this.textBoxGraphRepresentation.Size = new System.Drawing.Size(186, 186);
            this.textBoxGraphRepresentation.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Number of vertices";
            // 
            // textBoxNumberOfVertices
            // 
            this.textBoxNumberOfVertices.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumberOfVertices.Location = new System.Drawing.Point(163, 98);
            this.textBoxNumberOfVertices.Name = "textBoxNumberOfVertices";
            this.textBoxNumberOfVertices.Size = new System.Drawing.Size(79, 29);
            this.textBoxNumberOfVertices.TabIndex = 11;
            this.textBoxNumberOfVertices.TextChanged += new System.EventHandler(this.textBoxNumberOfVertices_TextChanged);
            // 
            // comboBoxCreationMethodSelector
            // 
            this.comboBoxCreationMethodSelector.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCreationMethodSelector.FormattingEnabled = true;
            this.comboBoxCreationMethodSelector.Items.AddRange(new object[] {
            "Adjacency matrix",
            "Adjacency list"});
            this.comboBoxCreationMethodSelector.Location = new System.Drawing.Point(18, 63);
            this.comboBoxCreationMethodSelector.Name = "comboBoxCreationMethodSelector";
            this.comboBoxCreationMethodSelector.Size = new System.Drawing.Size(224, 29);
            this.comboBoxCreationMethodSelector.TabIndex = 10;
            this.comboBoxCreationMethodSelector.Text = "Choose creation method";
            this.comboBoxCreationMethodSelector.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreationMethodSelector_SelectedIndexChanged);
            // 
            // comboBoxAlgorithms
            // 
            this.comboBoxAlgorithms.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.comboBoxAlgorithms.Location = new System.Drawing.Point(17, 531);
            this.comboBoxAlgorithms.Name = "comboBoxAlgorithms";
            this.comboBoxAlgorithms.Size = new System.Drawing.Size(224, 29);
            this.comboBoxAlgorithms.TabIndex = 9;
            this.comboBoxAlgorithms.Text = "Choose algorithm";
            // 
            // AddVertexButton
            // 
            this.AddVertexButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AddVertexButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddVertexButton.Location = new System.Drawing.Point(17, 383);
            this.AddVertexButton.Name = "AddVertexButton";
            this.AddVertexButton.Size = new System.Drawing.Size(224, 35);
            this.AddVertexButton.TabIndex = 8;
            this.AddVertexButton.Text = "Add vertex";
            this.AddVertexButton.UseVisualStyleBackColor = false;
            // 
            // ConnectVerticesButton
            // 
            this.ConnectVerticesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ConnectVerticesButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConnectVerticesButton.Location = new System.Drawing.Point(17, 463);
            this.ConnectVerticesButton.Name = "ConnectVerticesButton";
            this.ConnectVerticesButton.Size = new System.Drawing.Size(224, 35);
            this.ConnectVerticesButton.TabIndex = 7;
            this.ConnectVerticesButton.Text = "Connect vertices";
            this.ConnectVerticesButton.UseVisualStyleBackColor = false;
            // 
            // RemoveObjButton
            // 
            this.RemoveObjButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RemoveObjButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RemoveObjButton.Location = new System.Drawing.Point(17, 425);
            this.RemoveObjButton.Name = "RemoveObjButton";
            this.RemoveObjButton.Size = new System.Drawing.Size(224, 32);
            this.RemoveObjButton.TabIndex = 6;
            this.RemoveObjButton.Text = "Remove object";
            this.RemoveObjButton.UseVisualStyleBackColor = false;
            // 
            // CreateGraphButton
            // 
            this.CreateGraphButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CreateGraphButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreateGraphButton.Location = new System.Drawing.Point(17, 22);
            this.CreateGraphButton.Name = "CreateGraphButton";
            this.CreateGraphButton.Size = new System.Drawing.Size(225, 35);
            this.CreateGraphButton.TabIndex = 0;
            this.CreateGraphButton.Text = "Create graph";
            this.CreateGraphButton.UseVisualStyleBackColor = false;
            this.CreateGraphButton.Click += new System.EventHandler(this.CreateGraphButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 611);
            this.Controls.Add(this.panelMain);
            this.MinimumSize = new System.Drawing.Size(1015, 650);
            this.Name = "MainView";
            this.Text = "All about graph";
            this.panelMain.ResumeLayout(false);
            this.panelCanvas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.panelTools.ResumeLayout(false);
            this.panelTools.PerformLayout();
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
        private System.Windows.Forms.ComboBox comboBoxCreationMethodSelector;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxNumberOfVertices;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label CreationMethodLabel;
        private System.Windows.Forms.TextBox textBoxGraphRepresentation;
    }
}

