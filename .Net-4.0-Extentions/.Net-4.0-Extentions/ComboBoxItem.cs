namespace Net_4._0_Extentions
{
    public class ComboBoxItem
    {
        public ComboBoxItem(string text, object value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            return this.Text ?? "";
        }
    }
}