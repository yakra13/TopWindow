

namespace TopWindow
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_close = new Button();
            lstOutput = new ListBox();
            SuspendLayout();
            // 
            // btn_close
            // 
            btn_close.Location = new Point(355, 320);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(75, 23);
            btn_close.TabIndex = 0;
            btn_close.Text = "Cancel";
            btn_close.UseVisualStyleBackColor = true;
            // 
            // lstOutput
            // 
            lstOutput.FormattingEnabled = true;
            lstOutput.Location = new Point(16, 10);
            lstOutput.Name = "lstOutput";
            lstOutput.Size = new Size(772, 304);
            lstOutput.TabIndex = 1;
            lstOutput.MouseMove += lstOutput_MouseMove;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_close);
            Controls.Add(lstOutput);
            Name = "Form1";
            Text = "Form1";
            Shown += Form1_Shown;
            MouseMove += Form1_MouseMove;
            ResumeLayout(false);
        }

        #endregion

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND &&
                (m.WParam.ToInt64() & 0xFFF0) == SC_CLOSE)
            {
                return; // Ignore Alt+F4 and system close commands
            }

            base.WndProc(ref m);
        }

        private Button btn_close;
        private ListBox lstOutput;
    }
}
