using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Tick {
	public class Tick : Form {
		private PictureBox[] digits = new PictureBox[4];
		public Tick() {
			BackgroundImageLayout = ImageLayout.Stretch;
			Cursor.Hide();
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)new ResourceManager("Tick", Assembly.GetExecutingAssembly()).GetObject("Tick.ico");;
			Text = "Tick";
			WindowState = FormWindowState.Maximized;
			
			try {
				BackgroundImage = Image.FromFile("skin\\background.png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			PictureBox separator = new PictureBox();
			separator.BackgroundImageLayout = ImageLayout.Stretch;
			separator.Dock = DockStyle.Fill;
			try {
				separator.BackgroundImage = Image.FromFile("skin\\separator.png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		
			for (int i = 0; i < 4; i++) {
				digits[i] = new PictureBox();
				digits[i].BackgroundImageLayout = ImageLayout.Stretch;
				digits[i].Dock = DockStyle.Fill;
			}
			
			TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
			tableLayoutPanel.Dock = DockStyle.Fill;
			tableLayoutPanel.BackColor = Color.Transparent;
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.Controls.Add(digits[0], 0, 0);
			tableLayoutPanel.Controls.Add(digits[1], 1, 0);
			tableLayoutPanel.Controls.Add(separator, 2, 0);
			tableLayoutPanel.Controls.Add(digits[2], 3, 0);
			tableLayoutPanel.Controls.Add(digits[3], 4, 0);
			Controls.Add(tableLayoutPanel);

			Timer timer = new Timer();
			timer.Interval = 60000;
			timer.Enabled = true;
			
			timer.Tick += new EventHandler(Timer);
			KeyPress += new KeyPressEventHandler(KeyPressHandler);
			
			UpdateClock();
		}
		
		private void UpdateClock() {
			string time = DateTime.Now.ToString("hhmm");
			for (int i = 0; i < 4; i++)
				try {
					digits[i].BackgroundImage = Image.FromFile("skin\\" + time[i] + ".png");
				}
				catch (FileNotFoundException ex) {
					MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
		}

		private void Timer(object sender, EventArgs e) {
			UpdateClock();
		}

		private void KeyPressHandler(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)27)
				Close();
		}
		
		private static void Main() {
			Application.Run(new Tick());
		}
	}
}