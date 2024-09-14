using System.Windows;

namespace Lab4_1
{
    public partial class InputDialog : Window
    {
        public string Answer { get; private set; }

        public InputDialog(string prompt)
        {
            InitializeComponent();
            PromptTextBlock.Text = prompt;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Answer = InputTextBox.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
