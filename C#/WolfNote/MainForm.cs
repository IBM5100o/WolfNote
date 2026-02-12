namespace WolfNote
{
    public class MainForm : Form
    {
        private NumericUpDown numericCount;
        private Button btnApply;
        private Panel panelGrid;
        private Button btnParse;
        private TextBox userText;
        private List<string> playerNames;

        public MainForm()
        {
            Text = "WolfNote";
            Width = 780;
            Height = 450;

            Icon = new Icon("..\\..\\..\\app.ico");
            var label = new Label { Text = "Number of boxes:", Location = new Point(10, 15), AutoSize = true };
            Controls.Add(label);

            numericCount = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 100,
                Value = 25,
                Location = new Point(120, 10),
                Width = 60
            };
            Controls.Add(numericCount);

            btnApply = new Button { Text = "Apply", Location = new Point(200, 8) };
            btnApply.Click += BtnApply_Click;
            Controls.Add(btnApply);

            panelGrid = new Panel { Location = new Point(10, 50), AutoScroll = true, Width = ClientSize.Width - 20, Height = ClientSize.Height - 60, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            Controls.Add(panelGrid);

            var label2 = new Label { Text = "Player names:", Location = new Point(470, 15), AutoSize = true };
            Controls.Add(label2);

            userText = new TextBox
            {
                Location = new Point(560, 10),
                Width = 100,
                Multiline = true,
            };
            Controls.Add(userText);
            btnParse = new Button
            {
                Text = "Set names",
                Location = new Point(670, 10)
            };
            btnParse.Click += BtnParse_Click;
            Controls.Add(btnParse);

            playerNames = [];

            Shown += MainForm_Shown;
            Resize += MainForm_Resize;
        }

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            panelGrid.Width = ClientSize.Width - 20;
            panelGrid.Height = ClientSize.Height - 60;
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            BuildGrid((int)numericCount.Value);
        }

        private void BtnApply_Click(object? sender, EventArgs e)
        {
            BuildGrid((int)numericCount.Value);
        }

        private void BtnParse_Click(object? sender, EventArgs e)
        {
            playerNames = [];
            string input = userText.Text;
            var parts = input
                .Split(['\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();
            int flag = 0;
            foreach (var part in parts)
            {
                if (part[0] == '◆')
                {
                    if (flag == 0)
                    {
                        playerNames.Add(part[1..]);
                        flag = 1;
                    }
                }
                else
                {
                    flag = 0;
                }
            }
            BuildGrid((int)numericCount.Value);
            userText.Text = string.Empty;
            playerNames = [];
        }

        private void BuildGrid(int count)
        {
            panelGrid.Controls.Clear();

            int maxColumns = 5;
            int cellWidth = 80;
            int cellHeight = 60;
            //int cellSize = 60;
            int padding = 10;

            for (int i = 0; i < count; i++)
            {
                int row = i / maxColumns;
                int col = i % maxColumns;

                // Create a container panel for the cell so we can have separate labels for name and number
                var cell = new Panel();
                cell.BorderStyle = BorderStyle.FixedSingle;
                cell.BackColor = Color.White;
                cell.Size = new Size(cellWidth, cellHeight);
                cell.Location = new Point(col * (cellWidth + padding), row * (cellHeight + padding));

                // Name label (smaller font, can be longer)
                var nameLabel = new Label();
                nameLabel.Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
                nameLabel.AutoSize = false;
                nameLabel.Size = new Size(cellWidth - 4, cellHeight / 2);
                nameLabel.Location = new Point(2, 2);
                nameLabel.TextAlign = ContentAlignment.MiddleCenter;
                nameLabel.AutoEllipsis = true;
                nameLabel.BackColor = Color.Transparent;

                // Number label (larger, bold)
                var numberLabel = new Label();
                numberLabel.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
                numberLabel.AutoSize = false;
                numberLabel.Size = new Size(cellWidth - 4, cellHeight - nameLabel.Height - 4);
                numberLabel.Location = new Point(2, nameLabel.Bottom + 2);
                numberLabel.TextAlign = ContentAlignment.MiddleCenter;
                numberLabel.BackColor = Color.Transparent;

                var nameCount = playerNames.Count();
                if (nameCount != 0 && i < nameCount)
                {
                    nameLabel.Text = playerNames[i];
                    numberLabel.Text = "0";
                }
                else
                {
                    nameLabel.Text = string.Empty;
                    numberLabel.Text = "0";
                }

                // Click handlers: update only the numberLabel
                MouseEventHandler adjustNumber = (s, e) =>
                {
                    if (int.TryParse(numberLabel.Text, out int v))
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            numberLabel.Text = (v - 1).ToString();
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            numberLabel.Text = (v + 1).ToString();
                        }
                    }
                };

                // Attach handler to container and both labels so clicks are handled regardless of where user clicks
                cell.MouseDown += adjustNumber;
                nameLabel.MouseDown += adjustNumber;
                numberLabel.MouseDown += adjustNumber;

                cell.Controls.Add(nameLabel);
                cell.Controls.Add(numberLabel);

                panelGrid.Controls.Add(cell);
            }
        }
    }
}
