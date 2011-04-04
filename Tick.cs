using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Tick {
	public class Tick : Form {
		private PictureBox firstDigit = new PictureBox();
		private PictureBox secondDigit = new PictureBox();
		private PictureBox thirdDigit = new PictureBox();
		private PictureBox fourthDigit = new PictureBox();	
		private bool militaryTime = true;
		
		public Tick() {
			BackgroundImageLayout = ImageLayout.Stretch;
			try {
				BackgroundImage = Image.FromFile("skin\\background.png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			Cursor.Hide();
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)new ResourceManager("Tick", Assembly.GetExecutingAssembly()).GetObject("Tick.ico");;
			Text = "Tick";
			WindowState = FormWindowState.Maximized;

			firstDigit.BackgroundImageLayout = ImageLayout.Stretch;
			firstDigit.Dock = DockStyle.Fill;

			secondDigit.BackgroundImageLayout = ImageLayout.Stretch;
			secondDigit.Dock = DockStyle.Fill;

			PictureBox separator = new PictureBox();
			separator.BackgroundImageLayout = ImageLayout.Stretch;
			try {
				separator.BackgroundImage = Image.FromFile("skin\\separator.png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			separator.Dock = DockStyle.Fill;

			thirdDigit.BackgroundImageLayout = ImageLayout.Stretch;
			thirdDigit.Dock = DockStyle.Fill;

			fourthDigit.BackgroundImageLayout = ImageLayout.Stretch;
			fourthDigit.Dock = DockStyle.Fill;

			TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
			tableLayoutPanel.BackColor = Color.Transparent;
			tableLayoutPanel.ColumnCount = 5;
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
			tableLayoutPanel.Dock = DockStyle.Fill;
			tableLayoutPanel.Controls.Add(firstDigit, 0, 0);
			tableLayoutPanel.Controls.Add(secondDigit, 1, 0);
			tableLayoutPanel.Controls.Add(separator, 2, 0);
			tableLayoutPanel.Controls.Add(thirdDigit, 3, 0);
			tableLayoutPanel.Controls.Add(fourthDigit, 4, 0);
			Controls.Add(tableLayoutPanel);

			Timer timer = new Timer();
			timer.Interval = 60000;
			timer.Enabled = true;
			timer.Tick += new EventHandler(Timer);

			KeyPress += new KeyPressEventHandler(KeyPressHandler);
			UpdateClock();
		}
		
		private void UpdateClock() {
			string hour;
			if (!militaryTime && DateTime.Now.Hour > 12) hour = (DateTime.Now.Hour - 12).ToString();
			else if (!militaryTime && DateTime.Now.Hour == 0) hour = "12";
			else hour = DateTime.Now.Hour.ToString();
			
			if (hour.Length == 1) hour = "0" + hour;
			string minute = DateTime.Now.Minute.ToString();
			if (minute.Length == 1) minute = "0" + minute;
			
			try {
				firstDigit.BackgroundImage = Image.FromFile("skin\\" + hour[0] + ".png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			try {
				secondDigit.BackgroundImage = Image.FromFile("skin\\" + hour[1] + ".png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			try {
				thirdDigit.BackgroundImage = Image.FromFile("skin\\" + minute[0] + ".png");
			}
			catch (FileNotFoundException ex) {
				MessageBox.Show(ex.Message + " is missing", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			try {
				fourthDigit.BackgroundImage = Image.FromFile("skin\\" + minute[1] + ".png");
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
			else {
				militaryTime = !militaryTime;
				UpdateClock();
			}
		}
		
		private static void Main() {
			Application.Run(new Tick());
		}
	}
}