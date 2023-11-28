using System.Diagnostics;

namespace Calculator;

public partial class CalculatorApp : Form
{
    public CalculatorApp()
    {
        InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        //var hoofdThread  = SynchronizationContext.Current;

        if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b)) 
        {
            //int result = LongAdd(a, b);
            //UpdateAnswer(result);


            //Task.Run(()=>LongAdd(a, b)).ContinueWith(t => hoofdThread?.Post(UpdateAnswer, t.Result));
            // int result=await LongAddAsync(a, b);
            // UpdateAnswer(result);
            //var t1 = ToMath(a, b).ConfigureAwait(false);
            var res = ToMath(a, b).Result; // Dead lock
            Debug.WriteLine(res);
        }     
    }

    private async Task<int> ToMath(int a, int b)
    {
        int result = await LongAddAsync(a, b);
        UpdateAnswer(result);
        return result;
    }

    private void UpdateAnswer(object? result)
    {
        lblAnswer.Text = result?.ToString();
    }

    private int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
    private Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a,b));
    }
}