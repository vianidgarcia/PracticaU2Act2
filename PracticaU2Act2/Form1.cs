namespace PracticaU2Act2;

/// <summary>
/// Window only: reads the controls, calls ExerciseRunner / TestBench on another thread and shows the text.
/// All the recursion logic lives in RecursiveCatalog.
/// </summary>
public partial class Form1 : Form
{
    private readonly ExerciseRunner _runner = new();

    public Form1()
    {
        InitializeComponent();

        foreach (ExerciseInfo info in ExerciseCatalogInfo.All)
        {
            cmbExercise.Items.Add(info.Name);
        }

        cmbExercise.SelectedIndex = 0;
    }

    private void cmbExercise_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ExerciseInfo info = ExerciseCatalogInfo.All[cmbExercise.SelectedIndex];
        lblInput.Text = info.InputLabel;
        txtInput.Text = info.SampleInput;
        lblTarget.Visible = info.NeedsTarget;
        txtTarget.Visible = info.NeedsTarget;
        txtTarget.Text = info.SampleTarget;
        txtInfo.Text = info.ToDescription();
        txtResult.Clear();
        lblStatus.Text = string.Empty;
    }

    private async void btnRun_Click(object? sender, EventArgs e)
    {
        // Copy the values first: the work runs on another thread and must not touch the controls.
        int index = cmbExercise.SelectedIndex;
        string input = txtInput.Text;
        string target = txtTarget.Text;

        btnRun.Enabled = false;
        lblStatus.Text = "Calculando...";
        try
        {
            string report = await Task.Run(() => _runner.Run(index, input, target));
            txtResult.Text = report;
            lblStatus.Text = "Listo.";
        }
        catch (Exception ex) when (ex is FormatException or ArgumentException)
        {
            txtResult.Clear();
            lblStatus.Text = "Error: " + ex.Message;
        }
        finally
        {
            btnRun.Enabled = true;
        }
    }

    private void txtInput_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            btnRun.PerformClick();
        }
    }

    private async void btnRunTests_Click(object? sender, EventArgs e)
    {
        btnRunTests.Enabled = false;
        lblTestSummary.Text = "Ejecutando pruebas...";
        try
        {
            List<TestResult> results = await Task.Run(TestBench.RunAll);
            txtTestReport.Text = TestBench.FormatReport(results);
            lblTestSummary.Text = $"{results.Count(r => r.Passed)} de {results.Count} casos aprobados";
        }
        finally
        {
            btnRunTests.Enabled = true;
        }
    }
}