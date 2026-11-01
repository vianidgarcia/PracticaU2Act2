namespace PracticaU2Act2
{
    partial class Form1
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>Required method for Designer support.</summary>
        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabCatalog = new TabPage();
            tabTests = new TabPage();
            splitMain = new SplitContainer();
            txtInfo = new TextBox();
            txtResult = new TextBox();
            lblStatus = new Label();
            panelTop = new Panel();
            lblExercise = new Label();
            cmbExercise = new ComboBox();
            lblInput = new Label();
            txtInput = new TextBox();
            lblTarget = new Label();
            txtTarget = new TextBox();
            btnRun = new Button();
            txtTestReport = new TextBox();
            panelTests = new Panel();
            btnRunTests = new Button();
            lblTestSummary = new Label();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            tabControl.SuspendLayout();
            tabCatalog.SuspendLayout();
            tabTests.SuspendLayout();
            panelTop.SuspendLayout();
            panelTests.SuspendLayout();
            SuspendLayout();
            // tabControl
            tabControl.Dock = DockStyle.Fill;
            tabControl.SelectedIndex = 0;
            tabControl.Name = "tabControl";
            tabControl.TabIndex = 0;
            // tabCatalog
            tabCatalog.Text = "Catálogo";
            tabCatalog.Padding = new Padding(3);
            tabCatalog.UseVisualStyleBackColor = true;
            tabCatalog.Name = "tabCatalog";
            tabCatalog.TabIndex = 0;
            // tabTests
            tabTests.Text = "Pruebas";
            tabTests.Padding = new Padding(3);
            tabTests.UseVisualStyleBackColor = true;
            tabTests.Name = "tabTests";
            tabTests.TabIndex = 1;
            // splitMain
            splitMain.Dock = DockStyle.Fill;
            splitMain.SplitterDistance = 340;
            splitMain.Name = "splitMain";
            splitMain.TabIndex = 0;
            // txtInfo
            txtInfo.Dock = DockStyle.Fill;
            txtInfo.Multiline = true;
            txtInfo.ReadOnly = true;
            txtInfo.ScrollBars = ScrollBars.Vertical;
            txtInfo.Name = "txtInfo";
            txtInfo.TabIndex = 0;
            // txtResult
            txtResult.Dock = DockStyle.Fill;
            txtResult.Multiline = true;
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.WordWrap = false;
            txtResult.Font = new Font("Consolas", 10F);
            txtResult.Name = "txtResult";
            txtResult.TabIndex = 0;
            // lblStatus
            lblStatus.AutoSize = false;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Size = new Size(884, 28);
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Name = "lblStatus";
            lblStatus.TabIndex = 1;
            // panelTop
            panelTop.Dock = DockStyle.Top;
            panelTop.Size = new Size(884, 96);
            panelTop.Name = "panelTop";
            panelTop.TabIndex = 2;
            // lblExercise
            lblExercise.AutoSize = true;
            lblExercise.Location = new Point(12, 16);
            lblExercise.Text = "Ejercicio:";
            lblExercise.Name = "lblExercise";
            lblExercise.TabIndex = 0;
            // cmbExercise
            cmbExercise.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExercise.Location = new Point(110, 12);
            cmbExercise.Size = new Size(300, 25);
            cmbExercise.Name = "cmbExercise";
            cmbExercise.TabIndex = 1;
            // lblInput
            lblInput.AutoSize = false;
            lblInput.Location = new Point(12, 58);
            lblInput.Size = new Size(95, 23);
            lblInput.TextAlign = ContentAlignment.MiddleLeft;
            lblInput.Name = "lblInput";
            lblInput.TabIndex = 2;
            // txtInput
            txtInput.Location = new Point(110, 54);
            txtInput.Size = new Size(400, 27);
            txtInput.Name = "txtInput";
            txtInput.TabIndex = 3;
            // lblTarget
            lblTarget.AutoSize = false;
            lblTarget.Location = new Point(522, 58);
            lblTarget.Size = new Size(62, 23);
            lblTarget.Text = "Buscar:";
            lblTarget.TextAlign = ContentAlignment.MiddleLeft;
            lblTarget.Name = "lblTarget";
            lblTarget.TabIndex = 4;
            // txtTarget
            txtTarget.Location = new Point(588, 54);
            txtTarget.Size = new Size(80, 27);
            txtTarget.Name = "txtTarget";
            txtTarget.TabIndex = 5;
            // btnRun
            btnRun.Location = new Point(700, 51);
            btnRun.Size = new Size(130, 33);
            btnRun.Text = "Ejecutar";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Name = "btnRun";
            btnRun.TabIndex = 6;
            // txtTestReport
            txtTestReport.Dock = DockStyle.Fill;
            txtTestReport.Multiline = true;
            txtTestReport.ReadOnly = true;
            txtTestReport.ScrollBars = ScrollBars.Both;
            txtTestReport.WordWrap = false;
            txtTestReport.Font = new Font("Consolas", 10F);
            txtTestReport.Name = "txtTestReport";
            txtTestReport.TabIndex = 0;
            // panelTests
            panelTests.Dock = DockStyle.Top;
            panelTests.Size = new Size(884, 56);
            panelTests.Name = "panelTests";
            panelTests.TabIndex = 1;
            // btnRunTests
            btnRunTests.Location = new Point(12, 12);
            btnRunTests.Size = new Size(170, 33);
            btnRunTests.Text = "Ejecutar pruebas";
            btnRunTests.UseVisualStyleBackColor = true;
            btnRunTests.Name = "btnRunTests";
            btnRunTests.TabIndex = 0;
            // lblTestSummary
            lblTestSummary.AutoSize = false;
            lblTestSummary.Location = new Point(196, 16);
            lblTestSummary.Size = new Size(400, 23);
            lblTestSummary.TextAlign = ContentAlignment.MiddleLeft;
            lblTestSummary.Name = "lblTestSummary";
            lblTestSummary.TabIndex = 1;
            // containers
            Controls.Add(tabControl);
            tabControl.Controls.Add(tabCatalog);
            tabControl.Controls.Add(tabTests);
            tabCatalog.Controls.Add(splitMain);
            splitMain.Panel1.Controls.Add(txtInfo);
            splitMain.Panel2.Controls.Add(txtResult);
            tabCatalog.Controls.Add(lblStatus);
            tabCatalog.Controls.Add(panelTop);
            panelTop.Controls.Add(lblExercise);
            panelTop.Controls.Add(cmbExercise);
            panelTop.Controls.Add(lblInput);
            panelTop.Controls.Add(txtInput);
            panelTop.Controls.Add(lblTarget);
            panelTop.Controls.Add(txtTarget);
            panelTop.Controls.Add(btnRun);
            tabTests.Controls.Add(txtTestReport);
            tabTests.Controls.Add(panelTests);
            panelTests.Controls.Add(btnRunTests);
            panelTests.Controls.Add(lblTestSummary);
            // Form1
            AutoScaleMode = AutoScaleMode.Dpi;
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(900, 620);
            MinimumSize = new Size(900, 560);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Práctica 2.2 – Recursividad: catálogo de ejercicios";
            cmbExercise.SelectedIndexChanged += cmbExercise_SelectedIndexChanged;
            btnRun.Click += btnRun_Click;
            txtInput.KeyDown += txtInput_KeyDown;
            txtTarget.KeyDown += txtInput_KeyDown;
            btnRunTests.Click += btnRunTests_Click;
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel1.PerformLayout();
            splitMain.Panel2.ResumeLayout(false);
            splitMain.Panel2.PerformLayout();
            splitMain.ResumeLayout(false);
            panelTests.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            tabTests.ResumeLayout(false);
            tabCatalog.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabCatalog;
        private TabPage tabTests;
        private SplitContainer splitMain;
        private TextBox txtInfo;
        private TextBox txtResult;
        private Label lblStatus;
        private Panel panelTop;
        private Label lblExercise;
        private ComboBox cmbExercise;
        private Label lblInput;
        private TextBox txtInput;
        private Label lblTarget;
        private TextBox txtTarget;
        private Button btnRun;
        private TextBox txtTestReport;
        private Panel panelTests;
        private Button btnRunTests;
        private Label lblTestSummary;
    }
}